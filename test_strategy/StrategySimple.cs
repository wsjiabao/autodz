using System;
using GMSDK;

namespace test_strategy
{
    class Program
    {
        static void Main(string[] args)
        {
            //本例子演示如果用API编写一个基本的策略
            StrategySimple s = new StrategySimple();
            int ret = s.InitWithConfig("strategy.ini");
            System.Console.WriteLine("init: {0}", ret);

            ret = s.Run();

            if(ret != 0)
            {
                System.Console.WriteLine("run error: {0}", ret);
            }

            Console.Read();
        }
    }

    public class StrategySimple : Strategy
    {
        private bool flag = true;
        private int count = 0;

        /// <summary>
        /// 收到tick事件，在这里添加策略逻辑。我们简单的每10个tick开仓/平仓，以最新价下单。
        /// </summary>
        /// <param name="tick"></param>
        public override void OnTick( Tick tick)
        {
            Console.WriteLine(
                "tick {0}: time={1} symbol={2} last_price={3}", 
                this.count, 
                tick.utc_time, 
                tick.sec_id, 
                tick.last_price);

            if (this.count % 10 == 0)
            {
                if (this.flag)
                {
                    OpenLong(tick.exchange, tick.sec_id, tick.last_price, 1);  //最新价开仓一手
                }
                else
                {
                    CloseLong(tick.exchange, tick.sec_id, tick.last_price, 1); //最新价平仓一手
                }
            }

            this.count++;
            this.flag = !this.flag;
        }

        /// <summary>
        /// 收到bar事件。这里仅作演示输出，没策略逻辑。
        /// </summary>
        /// <param name="bar"></param>
        public override void OnBar(Bar bar)
        {
            Console.WriteLine(
                "bar: time={1} symbol={2} close_price={3}", 
                this.count, 
                bar.strtime, 
                bar.sec_id, 
                bar.close);
        }

        /// <summary>
        /// 委托执行回报，订单的任何执行回报都会触发本事件，通过rpt可访问回报信息。
        /// </summary>
        /// <param name="rpt"></param>
        public override void OnExecRpt(ExecRpt rpt)
        {
            Console.WriteLine(
                "rpt: cl_ord_id={0} price={1} amount={2} exec_type={3}", 
                rpt.cl_ord_id, 
                rpt.price, 
                rpt.amount, 
                rpt.exec_type);
        }

        /// <summary>
        /// 订单被拒绝时，触发本事件。order参数包含最新的order状态。
        /// </summary>
        /// <param name="order"></param>
        public override void OnOrderRejected(Order order)
        {
            Console.WriteLine("order rejected: {0} {1}", order.cl_ord_id, order.ord_rej_reason);
        }

        /// <summary>
        /// 当订单已被交易所接受时，触发本事件。order参数包含最新的order状态。
        /// </summary>
        /// <param name="order"></param>
        public override void OnOrderNew(Order order)
        {
            Console.WriteLine("order new: {0}", order.cl_ord_id);
        }

        /// <summary>
        /// 订单全部成交时，触发本事件。order参数包含最新的order状态。
        /// </summary>
        /// <param name="order"></param>
        public override void OnOrderFilled(Order order)
        {
            Console.WriteLine("order filled: {0}", order.cl_ord_id);
        }

        /// <summary>
        /// 订单部分成交时，触发本事件。order参数包含最新的order状态。
        /// </summary>
        /// <param name="order"></param>
        public override void OnOrderPartiallyFilled(Order order)
        {
            Console.WriteLine("order partially filled: {0}", order.cl_ord_id);
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

        /// <summary>
        /// 错误处理，收到错误事件时，可以根据error_code判断是什么错误，然后进行处理。
        /// 以下示例实时行情/交易断线后，重新连接的过程。
        /// </summary>
        /// <param name="error_code"></param>
        public override void OnError(int error_code, string error_msg)
        {
            Console.WriteLine("on_error: {0}, msg: {1}", error_code, error_msg);

        }
    }
}
