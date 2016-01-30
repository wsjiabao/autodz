using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GMSDK;
using System.Runtime.InteropServices;

namespace test_td
{
    class Program
    {
        private static void on_order_status(Order o)
        {
             System.Console.WriteLine("order status updated: {0}", o.cl_ord_id);
        }

        static void on_execrpt( ExecRpt e)
         {
             System.Console.WriteLine("execution: {0}", e.cl_ord_id);
         }

        static void on_order_new(Order o)
        {
             System.Console.WriteLine("order: {0}", o.cl_ord_id);
        }

        static void td_LoginEvent()
        {
            TdApi td = TdApi.Instance;

            Order order2;
            order2 = td.OpenLong("CFFEX", "IF1506", 0, 1);
            order2 = td.OpenLong("CFFEX","IF1506", 0, 1);
            order2 = td.OpenLong("CFFEX", "IF1506", 0, 1);
            System.Console.WriteLine("place order2: {0}", order2.cl_ord_id);

            Order order1;
            order1 = td.PlaceOrder(new Order());
            order1 = td.PlaceOrder(new Order());
            order1 = td.PlaceOrder(new Order());
            System.Console.WriteLine("place order1: {0}", order1.cl_ord_id);
        }

        static void Main(string[] args)
        {
            int ret;
            TdApi td = TdApi.Instance;
            td.ExecRptEvent +=on_execrpt;
            td.OrderStatusEvent += on_order_status;
            td.OrderNewEvent +=on_order_new;
            td.LoginEvent += td_LoginEvent;

            ret = td.Init(
                "demo@myquant.cn", 
                "123456", 
                "strategy_2",
                "localhost:8001"//连接到本地掘金终端, 此项为null或空字符串时，连接到掘金云服务
                ); 

            if (ret != 0)
            {
                //登录失败
                return;
            }

            Order order = td.GetOrder("df7c2bff713a47709959b90937c83be7");
            if (order == null)
            {
                System.Console.WriteLine("no order");
            }
            else
            {
                System.Console.WriteLine("order result: {0}", order.cl_ord_id);
            }

            Cash cash = td.GetCash();
            if (cash == null)
            {
                System.Console.WriteLine("no cash");
            }
            else
            {
                System.Console.WriteLine("cash result: {0}", cash.nav);
            }

            Position p = td.GetPosition("","", 0);
            if (p == null)
            {
                System.Console.WriteLine("no position");
            }
            else
            {
                System.Console.WriteLine("position result: {0}", p.volume);
            }

            List<Position> ps = td.GetPositions();
            if (ps.Count==0)
            {
                System.Console.WriteLine("no position");
            }
            else
            {
                System.Console.WriteLine("positions result: {0}", ps.Count);
            }

            Indicator indicator = td.GetIndicator();
            if (indicator == null)
            {
                System.Console.WriteLine("no indicator");
            }
            else
            {
                System.Console.WriteLine("indicator result: {0}", indicator.nav);
            }

            td.Run();

        }
    }
}
