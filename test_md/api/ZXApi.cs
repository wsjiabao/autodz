using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace MdTZ
{
    class ZXApi
    {

        /**
     * 卖出
     * */
        public static int sellOut(
            string code,
            string priceOpt,
            double price,
            string cntOpt,
            int cnt
        )
        {
            //买出
            WinAPI.mouse_event(WinAPI.MOUSEEVENTF_ABSOLUTE | WinAPI.MOUSEEVENTF_MOVE, 1745, 38800, 0, 0);
            WinAPI.mouse_event(WinAPI.MOUSEEVENTF_LEFTDOWN | WinAPI.MOUSEEVENTF_LEFTUP, 1, 1, 0, 0);
            Thread.Sleep(300);           

            //输入股票代码         
            THSAPI.inputCode(code);
            WinAPI.keybd_event((byte)Keys.Enter, 0, 0, 0);
            Thread.Sleep(300);

            //股票价格
            if (priceOpt.Equals(THSAPI.PRICE_OPT_SELL_2))
            {
                //股票价格上档
                WinAPI.mouse_event(WinAPI.MOUSEEVENTF_ABSOLUTE | WinAPI.MOUSEEVENTF_MOVE, 18700, 41080, 0, 0);
                WinAPI.mouse_event(WinAPI.MOUSEEVENTF_LEFTDOWN | WinAPI.MOUSEEVENTF_LEFTUP, 1, 1, 0, 0);
                WinAPI.keybd_event((byte)Keys.Enter, 0, 0, 0);
                Thread.Sleep(300);
            }
            else if (priceOpt.Equals(THSAPI.PRICE_OPT_BUY_2))
            {
                //股票价格下档
                WinAPI.mouse_event(WinAPI.MOUSEEVENTF_ABSOLUTE | WinAPI.MOUSEEVENTF_MOVE, 18700, 42080, 0, 0);
                WinAPI.mouse_event(WinAPI.MOUSEEVENTF_LEFTDOWN | WinAPI.MOUSEEVENTF_LEFTUP, 1, 1, 0, 0);
                WinAPI.keybd_event((byte)Keys.Enter, 0, 0, 0);
                Thread.Sleep(300);
            }

            //数量卖出-全部
            if (cntOpt.Equals(THSAPI.NUM_OPT_ALL))
            {
                //全部
                WinAPI.mouse_event(WinAPI.MOUSEEVENTF_ABSOLUTE | WinAPI.MOUSEEVENTF_MOVE, 18045, 43000, 0, 0);
                WinAPI.mouse_event(WinAPI.MOUSEEVENTF_LEFTDOWN | WinAPI.MOUSEEVENTF_LEFTUP, 1, 1, 0, 0);
                Thread.Sleep(300);
            }
            else if (cntOpt.Equals(THSAPI.NUM_OPT_INPUT))
            {
                THSAPI.inputNum(cnt.ToString());
                WinAPI.keybd_event((byte)Keys.Enter, 0, 0, 0);
                Thread.Sleep(300);
            }
            else if (cntOpt.Equals(THSAPI.NUM_OPT_50))
            {
                //1/2
                WinAPI.mouse_event(WinAPI.MOUSEEVENTF_ABSOLUTE | WinAPI.MOUSEEVENTF_MOVE, 11500, 44500, 0, 0);
                WinAPI.mouse_event(WinAPI.MOUSEEVENTF_LEFTDOWN | WinAPI.MOUSEEVENTF_LEFTUP, 1, 1, 0, 0);
                Thread.Sleep(300);
            }
            else if (cntOpt.Equals(THSAPI.NUM_OPT_30))
            {
                //1/3
                WinAPI.mouse_event(WinAPI.MOUSEEVENTF_ABSOLUTE | WinAPI.MOUSEEVENTF_MOVE, 13200, 44600, 0, 0);
                WinAPI.mouse_event(WinAPI.MOUSEEVENTF_LEFTDOWN | WinAPI.MOUSEEVENTF_LEFTUP, 1, 1, 0, 0);
                Thread.Sleep(300);
            }
            else if (cntOpt.Equals(THSAPI.NUM_OPT_25))
            {
                //1/4
                WinAPI.mouse_event(WinAPI.MOUSEEVENTF_ABSOLUTE | WinAPI.MOUSEEVENTF_MOVE, 15500, 44500, 0, 0);
                WinAPI.mouse_event(WinAPI.MOUSEEVENTF_LEFTDOWN | WinAPI.MOUSEEVENTF_LEFTUP, 1, 1, 0, 0);
                Thread.Sleep(300);
            }
            else if (cntOpt.Equals(THSAPI.NUM_OPT_20))
            {
                //1/5
                WinAPI.mouse_event(WinAPI.MOUSEEVENTF_ABSOLUTE | WinAPI.MOUSEEVENTF_MOVE, 17800, 44500, 0, 0);
                WinAPI.mouse_event(WinAPI.MOUSEEVENTF_LEFTDOWN | WinAPI.MOUSEEVENTF_LEFTUP, 1, 1, 0, 0);
                Thread.Sleep(300);
            }                             

            //交易确认
            WinAPI.keybd_event((byte)Keys.Enter, 0, 0, 0);
            Thread.Sleep(300);
            WinAPI.keybd_event((byte)Keys.Enter, 0, 0, 0);

            return 0;
        }



        /**
          * 卖出
          * */
        public static int sellOut_cur(
            string code,         
            string cntOpt,
            int cnt
        )
        {
            //买出
            WinAPI.mouse_event(WinAPI.MOUSEEVENTF_ABSOLUTE | WinAPI.MOUSEEVENTF_MOVE, 1745, 42600, 0, 0);
            WinAPI.mouse_event(WinAPI.MOUSEEVENTF_LEFTDOWN | WinAPI.MOUSEEVENTF_LEFTUP, 1, 1, 0, 0);
            Thread.Sleep(300);

            //输入股票代码         
            THSAPI.inputCode(code);           
            Thread.Sleep(300);
           

            //数量卖出-全部
            if (cntOpt.Equals(THSAPI.NUM_OPT_ALL))
            {
                //全部
                WinAPI.mouse_event(WinAPI.MOUSEEVENTF_ABSOLUTE | WinAPI.MOUSEEVENTF_MOVE, 18045, 43000, 0, 0);
                WinAPI.mouse_event(WinAPI.MOUSEEVENTF_LEFTDOWN | WinAPI.MOUSEEVENTF_LEFTUP, 1, 1, 0, 0);
                Thread.Sleep(300);
            }
            else if (cntOpt.Equals(THSAPI.NUM_OPT_INPUT))
            {
                THSAPI.inputNum(cnt.ToString());
                WinAPI.keybd_event((byte)Keys.Enter, 0, 0, 0);
                Thread.Sleep(300);
            }
            else if (cntOpt.Equals(THSAPI.NUM_OPT_50))
            {
                //1/2
                WinAPI.mouse_event(WinAPI.MOUSEEVENTF_ABSOLUTE | WinAPI.MOUSEEVENTF_MOVE, 11500, 44500, 0, 0);
                WinAPI.mouse_event(WinAPI.MOUSEEVENTF_LEFTDOWN | WinAPI.MOUSEEVENTF_LEFTUP, 1, 1, 0, 0);
                Thread.Sleep(300);
            }
            else if (cntOpt.Equals(THSAPI.NUM_OPT_30))
            {
                //1/3
                WinAPI.mouse_event(WinAPI.MOUSEEVENTF_ABSOLUTE | WinAPI.MOUSEEVENTF_MOVE, 13200, 44600, 0, 0);
                WinAPI.mouse_event(WinAPI.MOUSEEVENTF_LEFTDOWN | WinAPI.MOUSEEVENTF_LEFTUP, 1, 1, 0, 0);
                Thread.Sleep(300);
            }
            else if (cntOpt.Equals(THSAPI.NUM_OPT_25))
            {
                //1/4
                WinAPI.mouse_event(WinAPI.MOUSEEVENTF_ABSOLUTE | WinAPI.MOUSEEVENTF_MOVE, 15500, 44500, 0, 0);
                WinAPI.mouse_event(WinAPI.MOUSEEVENTF_LEFTDOWN | WinAPI.MOUSEEVENTF_LEFTUP, 1, 1, 0, 0);
                Thread.Sleep(300);
            }
            else if (cntOpt.Equals(THSAPI.NUM_OPT_20))
            {
                //1/5
                WinAPI.mouse_event(WinAPI.MOUSEEVENTF_ABSOLUTE | WinAPI.MOUSEEVENTF_MOVE, 17800, 44500, 0, 0);
                WinAPI.mouse_event(WinAPI.MOUSEEVENTF_LEFTDOWN | WinAPI.MOUSEEVENTF_LEFTUP, 1, 1, 0, 0);
                Thread.Sleep(300);
            }

            //交易确认
            WinAPI.keybd_event((byte)Keys.Enter, 0, 0, 0);
            Thread.Sleep(300);
            WinAPI.keybd_event((byte)Keys.Enter, 0, 0, 0);

            return 0;
        }




        /**
      * 买入
      * */
        public static int buyIn(
            string code,
            string priceOpt,
            double price,
            string cntOpt,
            int cnt
        )
        {
            //买入
            WinAPI.mouse_event(WinAPI.MOUSEEVENTF_ABSOLUTE | WinAPI.MOUSEEVENTF_MOVE, 1745, 36800, 0, 0);
            WinAPI.mouse_event(WinAPI.MOUSEEVENTF_LEFTDOWN | WinAPI.MOUSEEVENTF_LEFTUP, 1, 1, 0, 0);

            Thread.Sleep(300);

            //输入股票代码
            THSAPI.inputCode(code);
            WinAPI.keybd_event((byte)Keys.Enter, 0, 0, 0);
            Thread.Sleep(300);

            if (priceOpt.Equals(THSAPI.PRICE_OPT_SELL_2))
            {
                //股票价格上档
                WinAPI.mouse_event(WinAPI.MOUSEEVENTF_ABSOLUTE | WinAPI.MOUSEEVENTF_MOVE, 18700, 41100, 0, 0);
                WinAPI.mouse_event(WinAPI.MOUSEEVENTF_LEFTDOWN | WinAPI.MOUSEEVENTF_LEFTUP, 1, 1, 0, 0);
                WinAPI.keybd_event((byte)Keys.Enter, 0, 0, 0);
                Thread.Sleep(300);
            }
            else if (priceOpt.Equals(THSAPI.PRICE_OPT_BUY_2))
            {
                //股票价格下档
                WinAPI.mouse_event(WinAPI.MOUSEEVENTF_ABSOLUTE | WinAPI.MOUSEEVENTF_MOVE, 18700, 42080, 0, 0);
                WinAPI.mouse_event(WinAPI.MOUSEEVENTF_LEFTDOWN | WinAPI.MOUSEEVENTF_LEFTUP, 1, 1, 0, 0);
                WinAPI.keybd_event((byte)Keys.Enter, 0, 0, 0);
                Thread.Sleep(300);
            }                      
           
            //数量-全部
            if (cntOpt.Equals(THSAPI.NUM_OPT_ALL))
            {
                //全部
                WinAPI.mouse_event(WinAPI.MOUSEEVENTF_ABSOLUTE | WinAPI.MOUSEEVENTF_MOVE, 18045, 44800, 0, 0);
                WinAPI.mouse_event(WinAPI.MOUSEEVENTF_LEFTDOWN | WinAPI.MOUSEEVENTF_LEFTUP, 1, 1, 0, 0);
                Thread.Sleep(300);
            }
            else if (cntOpt.Equals(THSAPI.NUM_OPT_INPUT))
            {
                THSAPI.inputNum(cnt.ToString());
                WinAPI.keybd_event((byte)Keys.Enter, 0, 0, 0);
                Thread.Sleep(300);
            }
            else if (cntOpt.Equals(THSAPI.NUM_OPT_50))
            {
                //1/2
                WinAPI.mouse_event(WinAPI.MOUSEEVENTF_ABSOLUTE | WinAPI.MOUSEEVENTF_MOVE, 11000, 46300, 0, 0);
                WinAPI.mouse_event(WinAPI.MOUSEEVENTF_LEFTDOWN | WinAPI.MOUSEEVENTF_LEFTUP, 1, 1, 0, 0);
                Thread.Sleep(300);
            }
            else if (cntOpt.Equals(THSAPI.NUM_OPT_30))
            {
                //1/3
                WinAPI.mouse_event(WinAPI.MOUSEEVENTF_ABSOLUTE | WinAPI.MOUSEEVENTF_MOVE, 13200, 46300, 0, 0);
                WinAPI.mouse_event(WinAPI.MOUSEEVENTF_LEFTDOWN | WinAPI.MOUSEEVENTF_LEFTUP, 1, 1, 0, 0);
                Thread.Sleep(300);
            }
            else if (cntOpt.Equals(THSAPI.NUM_OPT_25))
            {
                //1/4
                WinAPI.mouse_event(WinAPI.MOUSEEVENTF_ABSOLUTE | WinAPI.MOUSEEVENTF_MOVE, 15200, 46300, 0, 0);
                WinAPI.mouse_event(WinAPI.MOUSEEVENTF_LEFTDOWN | WinAPI.MOUSEEVENTF_LEFTUP, 1, 1, 0, 0);
                Thread.Sleep(300);
            }
            else if (cntOpt.Equals(THSAPI.NUM_OPT_20))
            {
                //1/5
                WinAPI.mouse_event(WinAPI.MOUSEEVENTF_ABSOLUTE | WinAPI.MOUSEEVENTF_MOVE, 17600, 46300, 0, 0);
                WinAPI.mouse_event(WinAPI.MOUSEEVENTF_LEFTDOWN | WinAPI.MOUSEEVENTF_LEFTUP, 1, 1, 0, 0);  
                Thread.Sleep(300);
            }                                    

            //交易确认
            WinAPI.keybd_event((byte)Keys.Enter, 0, 0, 0);
            Thread.Sleep(300);
            WinAPI.keybd_event((byte)Keys.Enter, 0, 0, 0);

            return 0;
        }


        /**
          * 市交买入
          * */
        public static int buyIn_cur(
            string code,           
            string cntOpt,
            int cnt
        )
        {
            //买入
            WinAPI.mouse_event(WinAPI.MOUSEEVENTF_ABSOLUTE | WinAPI.MOUSEEVENTF_MOVE, 1745, 40400, 0, 0);
            WinAPI.mouse_event(WinAPI.MOUSEEVENTF_LEFTDOWN | WinAPI.MOUSEEVENTF_LEFTUP, 1, 1, 0, 0);

            Thread.Sleep(300);

            //输入股票代码
            THSAPI.inputCode(code);
            WinAPI.keybd_event((byte)Keys.Enter, 0, 0, 0);
            Thread.Sleep(300);           

            //数量-全部
            if (cntOpt.Equals(THSAPI.NUM_OPT_ALL))
            {
                //全部
                WinAPI.mouse_event(WinAPI.MOUSEEVENTF_ABSOLUTE | WinAPI.MOUSEEVENTF_MOVE, 18045, 44800, 0, 0);
                WinAPI.mouse_event(WinAPI.MOUSEEVENTF_LEFTDOWN | WinAPI.MOUSEEVENTF_LEFTUP, 1, 1, 0, 0);
                Thread.Sleep(300);
            }
            else if (cntOpt.Equals(THSAPI.NUM_OPT_INPUT))
            {
                THSAPI.inputNum(cnt.ToString());
                WinAPI.keybd_event((byte)Keys.Enter, 0, 0, 0);
                Thread.Sleep(300);
            }
            else if (cntOpt.Equals(THSAPI.NUM_OPT_50))
            {
                //1/2
                WinAPI.mouse_event(WinAPI.MOUSEEVENTF_ABSOLUTE | WinAPI.MOUSEEVENTF_MOVE, 11000, 46300, 0, 0);
                WinAPI.mouse_event(WinAPI.MOUSEEVENTF_LEFTDOWN | WinAPI.MOUSEEVENTF_LEFTUP, 1, 1, 0, 0);
                Thread.Sleep(300);
            }
            else if (cntOpt.Equals(THSAPI.NUM_OPT_30))
            {
                //1/3
                WinAPI.mouse_event(WinAPI.MOUSEEVENTF_ABSOLUTE | WinAPI.MOUSEEVENTF_MOVE, 13200, 46300, 0, 0);
                WinAPI.mouse_event(WinAPI.MOUSEEVENTF_LEFTDOWN | WinAPI.MOUSEEVENTF_LEFTUP, 1, 1, 0, 0);
                Thread.Sleep(300);
            }
            else if (cntOpt.Equals(THSAPI.NUM_OPT_25))
            {
                //1/4
                WinAPI.mouse_event(WinAPI.MOUSEEVENTF_ABSOLUTE | WinAPI.MOUSEEVENTF_MOVE, 15200, 46300, 0, 0);
                WinAPI.mouse_event(WinAPI.MOUSEEVENTF_LEFTDOWN | WinAPI.MOUSEEVENTF_LEFTUP, 1, 1, 0, 0);
                Thread.Sleep(300);
            }
            else if (cntOpt.Equals(THSAPI.NUM_OPT_20))
            {
                //1/5
                WinAPI.mouse_event(WinAPI.MOUSEEVENTF_ABSOLUTE | WinAPI.MOUSEEVENTF_MOVE, 17600, 46300, 0, 0);
                WinAPI.mouse_event(WinAPI.MOUSEEVENTF_LEFTDOWN | WinAPI.MOUSEEVENTF_LEFTUP, 1, 1, 0, 0);
                Thread.Sleep(300);
            }

            //交易确认
            WinAPI.keybd_event((byte)Keys.Enter, 0, 0, 0);
            Thread.Sleep(300);
            WinAPI.keybd_event((byte)Keys.Enter, 0, 0, 0);

            return 0;
        }


        /**
        * 委托撤销
        * */
        public static int agentCancel(string code, string cancelOpt)
        {
            //撤单
            WinAPI.mouse_event(WinAPI.MOUSEEVENTF_ABSOLUTE | WinAPI.MOUSEEVENTF_MOVE, 1745, 45800, 0, 0);
            WinAPI.mouse_event(WinAPI.MOUSEEVENTF_LEFTDOWN | WinAPI.MOUSEEVENTF_LEFTUP, 1, 1, 0, 0);

            Thread.Sleep(300);

            WinAPI.mouse_event(WinAPI.MOUSEEVENTF_ABSOLUTE | WinAPI.MOUSEEVENTF_MOVE, 13745, 38000, 0, 0);
            WinAPI.mouse_event(WinAPI.MOUSEEVENTF_LEFTDOWN | WinAPI.MOUSEEVENTF_LEFTUP, 1, 1, 0, 0);

            Thread.Sleep(300);

            if (!String.IsNullOrEmpty(code))
            {
                //输入股票代码
                THSAPI.inputCode(code);
                WinAPI.keybd_event((byte)Keys.Enter, 0, 0, 0);
                Thread.Sleep(300);
            }
            else
            {
                return 0;
            }

            //移动到撤销按钮
            WinAPI.mouse_event(WinAPI.MOUSEEVENTF_ABSOLUTE | WinAPI.MOUSEEVENTF_MOVE, 53000, 37800, 0, 0);
            WinAPI.mouse_event(WinAPI.MOUSEEVENTF_LEFTDOWN | WinAPI.MOUSEEVENTF_LEFTUP, 1, 1, 0, 0);

            Thread.Sleep(300);
            WinAPI.keybd_event((byte)Keys.Enter, 0, 0, 0);
            Thread.Sleep(300);
            WinAPI.keybd_event((byte)Keys.Enter, 0, 0, 0);

            return 0;
        }



        /**
        * 刷新窗口
        * */
        public static int refreshWin()
        {
            //撤单
            WinAPI.mouse_event(WinAPI.MOUSEEVENTF_ABSOLUTE | WinAPI.MOUSEEVENTF_MOVE, 3000, 49000, 0, 0);
            WinAPI.mouse_event(WinAPI.MOUSEEVENTF_LEFTDOWN | WinAPI.MOUSEEVENTF_LEFTUP, 1, 1, 0, 0);
            return 0;
        }
    }
}
