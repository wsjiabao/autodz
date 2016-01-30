using GMSDK;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MdTZ
{
    class StrategyTest : Strategy
    {
        private bool flag = true;
        private int count = 0;

        /// <summary>
        /// 收到tick事件，在这里添加策略逻辑。我们简单的每10个tick开仓/平仓，以最新价下单。
        /// </summary>
        /// <param name="tick"></param>
        public override void OnTick(Tick tick)
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
