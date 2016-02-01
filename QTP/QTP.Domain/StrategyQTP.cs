using System;
using System.Diagnostics;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

using QTP.Infra;
using QTP.DBAccess;
using GMSDK;

namespace QTP.Domain
{
    public class StrategyQTP : Strategy
    {
        #region Members
        private NLog log;
        private Dictionary<string, Monitor> monitors;

        protected TStrategy strategyT;
        protected RiskM risk;

        // pusle
        private System.Timers.Timer timer;
        private int countPusle;
        private int messageInterval = 40;       // 30s
        #endregion

        #region Public methods

        public StrategyQTP(TStrategy s, RiskM t, NLog log)
        {
            strategyT = s;

            t.SetStrategy(this);
            risk = t;
            
            this.log = log;

            monitors = new Dictionary<string, Monitor>();
        }

        public void Start()
        {
            WriteInfo(string.Format("策略({0}, {1})初始化开始", strategyT.GMID, strategyT.Id));

            int ret = base.Init(strategyT.UserName, strategyT.Password, strategyT.GMID, "", (MDMode)strategyT.MDMode, "localhost:8001");
            if (ret != 0)
            {
                WriteInfo("策略初始化错误");
                return; 
            }

            // Run Initialize Task
            Task.Run(new Action(InitAction));

            // Subscrible Instruments
            foreach (TInstrument ins in strategyT.Instruments)
            {
                base.Subscribe(ins.Symbol + ".tick");
                base.Subscribe(ins.Symbol + ".bar.60");
            }

            // Run strategy
            base.Run();
        }

        public new void Stop()
        {
            if (timer != null) timer.Close();
            base.Stop();
        }

        public Monitor GetMonitor(string symbol)
        {
            if (monitors.ContainsKey(symbol))
                return monitors[symbol];
            return null;
        }
        public Monitor GetMonitor(string exchange, string sec_id)
        {
            string symbol = string.Format("{0}.{1}", exchange, sec_id);
            return GetMonitor(symbol);
        }

        public double GetVolumn(string exchange, string sec_id)
        {
            return risk.GetVolume(exchange, sec_id);
        }
        #endregion

        #region utils

        public void WriteDebug(string msg)
        {
            log.WriteDebug(msg);
        }
        public void WriteError(string msg)
        {
            Console.WriteLine("[{0}.{1}] {2}", DateTime.Now.ToLongTimeString(), DateTime.Now.Millisecond, msg);
            log.WriteError(msg);
        }
        public void WriteWarning(string msg)
        {
            Console.WriteLine("[{0}.{1}] {2}", DateTime.Now.ToLongTimeString(), DateTime.Now.Millisecond, msg);
            log.WriteWarning(msg);
        }
        public void WriteInfo(string msg)
        {
            Console.WriteLine("[{0}.{1}] {2}", DateTime.Now.ToLongTimeString(), DateTime.Now.Millisecond, msg);
            log.WriteInfo(msg);
        }

        private void InitAction()
        {
            // monitors
            Assembly assembly = Assembly.LoadFrom(@"QTP.Domain.dll");
            foreach (TInstrument ins in strategyT.Instruments)
            {
                Type type = assembly.GetType(string.Format("QTP.Domain.{0}", ins.MonitorClass));

                Monitor monitor = (Monitor)Activator.CreateInstance(type);
                monitor.SetTInstrument(this, ins); 

                monitor.Initialize();
                monitors.Add(ins.Symbol, monitor);
            }

            // process buffer data of monitors
            foreach (KeyValuePair<string, Monitor> pair in monitors)
            {
                Monitor m = pair.Value;
                m.InitializeBufferData();
            }

            // Trader 资管 
            risk.Initialize();

            // pusle timer 
            PusleTimerHandler(null, null);

            timer = new System.Timers.Timer(1000);          // set 1s pusle timer 
            timer.Elapsed += PusleTimerHandler;
            timer.Start();
        }

        private void PusleTimerHandler(object sender, System.Timers.ElapsedEventArgs e)
        {
            // message Pusle
            countPusle++;
            if (countPusle % messageInterval == 0)
            {
                string msg = null;
                foreach (KeyValuePair<string, Monitor> pair in monitors)
                {
                    Monitor m = pair.Value;
                    msg += m.PulseHintMessage();
                }

                WriteInfo(msg);
            }

            // process OnPusle per monitor
            foreach (KeyValuePair<string, Monitor> pair in monitors)
            {
                Monitor m = pair.Value;
                m.OnPulse();
            }
        }

        #endregion

        #region MD events and Error override

        public override void OnTick(Tick tick)
        {
            Monitor monitor = GetMonitor(string.Format("{0}.{1}", tick.exchange, tick.sec_id));
            if (monitor != null)
            {
                monitor.OnTick(tick);
            }
        }

        public override void OnBar(Bar bar)
        {
            Monitor monitor = GetMonitor(string.Format("{0}.{1}", bar.exchange, bar.sec_id));
            if (monitor != null)
            {
                monitor.OnBar(bar);
            }
        }
        public override void OnMdEvent(MDEvent md_event)
        {
            WriteInfo(string.Format("重要行情事件({0})", md_event.event_type == 1 ? "开市":"收市"));
        }

        public override void OnError(int error_code, string error_msg)
        {
            WriteInfo(string.Format("{0}({1}))", error_msg, error_code));
        }

        #endregion

        #region Trader events overide
        /// <summary>
        /// 委托执行回报，订单的任何执行回报都会触发本事件，通过rpt可访问回报信息。
        /// </summary>
        /// <param name="rpt"></param>
        public override void OnExecRpt(ExecRpt rpt)
        {
            //Console.WriteLine(
            //    "rpt: cl_ord_id={0} price={1} amount={2} exec_type={3}",
            //    rpt.cl_ord_id,
            //    rpt.price,
            //    rpt.amount,
            //    rpt.exec_type);
        }

        /// <summary>
        /// 订单被拒绝时，触发本事件。order参数包含最新的order状态。
        /// </summary>
        /// <param name="order"></param>
        public override void OnOrderRejected(Order order)
        {
            WriteError(string.Format("{0} {1} 订单被拒绝[原因:{2}]", order.exchange, order.sec_id, order.ord_rej_reason));
        }

        /// <summary>
        /// 当订单已被交易所接受时，触发本事件。order参数包含最新的order状态。
        /// </summary>
        /// <param name="order"></param>
        public override void OnOrderNew(Order order)
        {
            WriteDebug(string.Format("{0}.{1} 新订单({2}]", order.exchange, order.sec_id, order.cl_ord_id));
        }

        /// <summary>
        /// 订单全部成交时，触发本事件。order参数包含最新的order状态。
        /// </summary>
        /// <param name="order"></param>
        public override void OnOrderFilled(Order order)
        {
            WriteInfo(string.Format("{0}.{1} 订单完成", order.exchange, order.sec_id));

            Monitor monitor = GetMonitor(order.exchange, order.sec_id);
            if (monitor != null)
                monitor.OnOrderFilled();
        }

        /// <summary>
        /// 订单部分成交时，触发本事件。order参数包含最新的order状态。
        /// </summary>
        /// <param name="order"></param>
        public override void OnOrderPartiallyFilled(Order order)
        {
            WriteInfo(string.Format("{0}.{1} 订单部分完成", order.exchange, order.sec_id));
        }

        /// <summary>
        /// 订单被停止执行时，触发本事件, 比如限价单到收市仍未成交，作订单过期处理。order参数包含最新的order状态。
        /// </summary>
        /// <param name="order"></param>
        public override void OnOrderStopExecuted(Order order)
        {
            Console.WriteLine("order stop executed: {0}", order.cl_ord_id);
        }

        /// <summary>
        /// 撤单成功时，触发本事件。order参数包含最新的order状态。
        /// </summary>
        /// <param name="order"></param>
        public override void OnOrderCancelled(Order order)
        {
            Console.WriteLine("order cancelled: {0}", order.cl_ord_id);
        }

        /// <summary>
        /// 撤单请求被拒绝时，触发本事件
        /// </summary>
        /// <param name="rpt"></param>
        public override void OnOrderCancelRejected(ExecRpt rpt)
        {
            Console.WriteLine("order cancel failed: {0}", rpt.cl_ord_id);
        }

        #endregion

    }
}
