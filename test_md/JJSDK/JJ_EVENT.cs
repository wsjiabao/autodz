using GMSDK;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MdTZ
{
    class JJ_EVENT
    {
        #region 行情数据事件：接收实时行情数据时触发，主要有Tick行情事件和Bar行情事件。

        public static void on_tick(Tick tick)
        {
            System.Console.WriteLine(string.Format("{0}  {1}.{2} {3}", tick.strtime, tick.exchange, tick.sec_id, tick.last_price));
        }
        public static void on_bar(Bar bar)
        {
            System.Console.WriteLine(string.Format("{0}  {1}.{2} {3}", bar.strtime, bar.exchange, bar.sec_id, bar.close));
        }


        #endregion


        #region 交易事件
        public static void on_order_status(Order o)
        {
            System.Console.WriteLine("order status updated: {0}", o.cl_ord_id);
        }

        public static void on_execrpt(ExecRpt e)
        {
            System.Console.WriteLine("execution: {0}", e.cl_ord_id);
        }

        public static void on_order_new(Order o)
        {
            System.Console.WriteLine("order: {0}", o.cl_ord_id);
        }

        public static void td_LoginEvent()
        {
           
        }


        #endregion


        #region 错误处理
        public static void OnError(int error_code, string error_msg) {
             System.Console.WriteLine(string.Format("{0}  {1}", error_code,error_msg));
        }


        #endregion
      


       

    }
}
