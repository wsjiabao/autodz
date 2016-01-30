using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GMSDK;

namespace test_strategy_backtest
{
    class Program
    {
        static void Main(string[] args)
        {
            //本例子演示如果用API编写一个基本的策略回测
            StrategySimple s = new StrategySimple();
            int ret = s.InitWithConfig("strategy.ini");
            if(ret != 0)
            {
                System.Console.WriteLine("Init error: {0}", ret);
            }

            ret = s.Run();

            if (ret != 0)
            {
                System.Console.WriteLine("run error: {0}", ret);
            }
        }
    }

    public class StrategySimple : Strategy
    {
        private bool flag = true;

        /// <summary>
        /// 收到tick事件，在这里添加策略逻辑。我们简单的每10个tick开仓/平仓，以最新价下单。
        /// </summary>
        /// <param name="tick"></param>
        public override void OnTick(Tick tick)
        {

        }

        /// <summary>
        /// 收到bar事件。这里仅作演示输出，没策略逻辑。
        /// </summary>
        /// <param name="bar"></param>
        public override void OnBar(Bar bar)
        {
            if (flag)
                OpenLong(bar.exchange, bar.sec_id, 0, 100);
            else
                CloseLong(bar.exchange, bar.sec_id, 0, 100);

            flag = !flag;
        }
    }
}
