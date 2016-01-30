using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;
using System.Threading;
using System.Windows.Forms;

namespace MdTZ
{
    public class THSAPI_MNP
    {

        /**
         * 买入策略
         * */
        public const string BUY_CR_1 = "1";     // 撤销第一个
        public const string BUY_CR_2 = "2";     // 撤销全部
        public const string BUY_CR_3 = "3";     // 撤销全部买单
        public const string BUY_CR_4 = "4";     // 撤销全部卖单
        public const string BUY_CR_5 = "5";     // 撤销全部卖单

        /**
         * 卖出策略
         * */
        public const string SELL_CR_1 = "1";     // 撤销全部卖单
        public const string SELL_CR_2= "2";     // 撤销全部卖单

        /**
         * 委托选项
         * */
        public const string CANCEL_OPT_FRIST = "0";     //撤销第一个
        public const string CANCEL_OPT_ALL = "a";     // 撤销全部
        public const string CANCEL_OPT_BUY_ALL = "ba";     // 撤销全部买单
        public const string CANCEL_OPT_SELL_ALL = "sa";     // 撤销全部卖单

        /**
         * 价格选项
         * */
        public const string PRICE_OPT_INPUT = "0";     //默认输入
        public const string PRICE_OPT_NOW = "n";     // 最新价格
        public const string PRICE_OPT_ZT = "zt";     // 涨停价格
        public const string PRICE_OPT_DT = "dt";     // 跌停价格
        public const string PRICE_OPT_BUY_1 = "b1";     // 买1
        public const string PRICE_OPT_BUY_2 = "b2";     //买2
        public const string PRICE_OPT_BUY_3 = "b3";     // 买3
        public const string PRICE_OPT_BUY_4 = "b4";     // 买4
        public const string PRICE_OPT_BUY_5 = "b5";     //买5

        public const string PRICE_OPT_SELL_1 = "s1";     // 卖1
        public const string PRICE_OPT_SELL_2 = "s2";     //卖2
        public const string PRICE_OPT_SELL_3 = "s3";     // 卖3
        public const string PRICE_OPT_SELL_4 = "s4";     //卖4
        public const string PRICE_OPT_SELL_5 = "s5";     //卖5


        /**
        * 数量选项
        * */
        public const string NUM_OPT_ALL = "a";     //全量
        public const string NUM_OPT_INPUT = "i";     //输入


        /**
         * 输入股票代码
         * */
        private static void inputCode(
            string code
        )
        {
            //输入股票代码
            int kn = 0;
            for (int i = 0; i < code.Length; i++)
            {
                kn = int.Parse(code.ToArray().GetValue(i).ToString());
                switch (kn)
                {
                    default:
                        break;
                    case 0:
                        WinAPI.keybd_event((byte)Keys.D0, 0, 0, 0); ;
                        break;
                    case 1:
                        WinAPI.keybd_event((byte)Keys.D1, 0, 0, 0); ;
                        break;
                    case 2:
                        WinAPI.keybd_event((byte)Keys.D2, 0, 0, 0); ;
                        break;
                    case 3:
                        WinAPI.keybd_event((byte)Keys.D3, 0, 0, 0); ;
                        break;
                    case 4:
                        WinAPI.keybd_event((byte)Keys.D4, 0, 0, 0); ;
                        break;
                    case 5:
                        WinAPI.keybd_event((byte)Keys.D5, 0, 0, 0); ;
                        break;
                    case 6:
                        WinAPI.keybd_event((byte)Keys.D6, 0, 0, 0); ;
                        break;
                    case 7:
                        WinAPI.keybd_event((byte)Keys.D7, 0, 0, 0); ;
                        break;
                    case 8:
                        WinAPI.keybd_event((byte)Keys.D8, 0, 0, 0); ;
                        break;
                    case 9:
                        WinAPI.keybd_event((byte)Keys.D9, 0, 0, 0); ;
                        break;
                }
            }
        }




        /**
         * 输入数量
         * */
        private static void inputNum(
            string num
        )
        {
            //输入股票代码
            char kn = '0';
            for (int i = 0; i < num.Length; i++)
            {
                kn = (char)num.ToArray().GetValue(i);
                switch (kn)
                {
                    default:
                        break;
                    case '0':
                        WinAPI.keybd_event((byte)Keys.D0, 0, 0, 0); ;
                        break;
                    case '.':
                        WinAPI.keybd_event((byte)Keys.Decimal, 0, 0, 0); ;
                        break;
                    case '1':
                        WinAPI.keybd_event((byte)Keys.D1, 0, 0, 0); ;
                        break;
                    case '2':
                        WinAPI.keybd_event((byte)Keys.D2, 0, 0, 0); ;
                        break;
                    case '3':
                        WinAPI.keybd_event((byte)Keys.D3, 0, 0, 0); ;
                        break;
                    case '4':
                        WinAPI.keybd_event((byte)Keys.D4, 0, 0, 0); ;
                        break;
                    case '5':
                        WinAPI.keybd_event((byte)Keys.D5, 0, 0, 0); ;
                        break;
                    case '6':
                        WinAPI.keybd_event((byte)Keys.D6, 0, 0, 0); ;
                        break;
                    case '7':
                        WinAPI.keybd_event((byte)Keys.D7, 0, 0, 0); ;
                        break;
                    case '8':
                        WinAPI.keybd_event((byte)Keys.D8, 0, 0, 0); ;
                        break;
                    case '9':
                        WinAPI.keybd_event((byte)Keys.D9, 0, 0, 0); ;
                        break;
                }
            }
        }


         /**
         * 买入重置
         * */
        public static void buyReset()
        {
            //先重置
            WinAPI.mouse_event(WinAPI.MOUSEEVENTF_ABSOLUTE | WinAPI.MOUSEEVENTF_MOVE, 12500, 18000, 0, 0);
            WinAPI.mouse_event(WinAPI.MOUSEEVENTF_LEFTDOWN | WinAPI.MOUSEEVENTF_LEFTUP, 1, 1, 0, 0);
            Thread.Sleep(300);
        }

        /**
       * 买入重置
       * */
        public static void buyReset1()
        {
            //先重置
            WinAPI.mouse_event(WinAPI.MOUSEEVENTF_ABSOLUTE | WinAPI.MOUSEEVENTF_MOVE, 11000, 19700, 0, 0);
            WinAPI.mouse_event(WinAPI.MOUSEEVENTF_LEFTDOWN | WinAPI.MOUSEEVENTF_LEFTUP, 1, 1, 0, 0);
            Thread.Sleep(300);
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
        ){
            //买入按钮
            WinAPI.mouse_event(WinAPI.MOUSEEVENTF_ABSOLUTE | WinAPI.MOUSEEVENTF_MOVE, 1200, 6000, 0, 0);
            WinAPI.mouse_event(WinAPI.MOUSEEVENTF_LEFTDOWN | WinAPI.MOUSEEVENTF_LEFTUP, 1, 1, 0, 0);
            Thread.Sleep(300);

            //先重置
            buyReset();

            //买入代码
            WinAPI.mouse_event(WinAPI.MOUSEEVENTF_ABSOLUTE | WinAPI.MOUSEEVENTF_MOVE, 15000, 10000, 0, 0);
            WinAPI.mouse_event(WinAPI.MOUSEEVENTF_LEFTDOWN | WinAPI.MOUSEEVENTF_LEFTUP, 1, 1, 0, 0);
            Thread.Sleep(300);

            //输入股票代码
            inputCode(code);
            WinAPI.keybd_event((byte)Keys.Enter, 0, 0, 0);
            Thread.Sleep(300);

            //股票价格
            if (priceOpt.Equals(PRICE_OPT_INPUT))
            {
                //输入价格
                inputNum(price.ToString());                
                Thread.Sleep(300);
            }
            else if (priceOpt.Equals(PRICE_OPT_BUY_1))
            {
                WinAPI.mouse_event(WinAPI.MOUSEEVENTF_ABSOLUTE | WinAPI.MOUSEEVENTF_MOVE, 21200, 16000, 0, 0);
                WinAPI.mouse_event(WinAPI.MOUSEEVENTF_LEFTDOWN | WinAPI.MOUSEEVENTF_LEFTUP, 1, 1, 0, 0);
                Thread.Sleep(300);
            }
            else if (priceOpt.Equals(PRICE_OPT_BUY_2))
            {
                WinAPI.mouse_event(WinAPI.MOUSEEVENTF_ABSOLUTE | WinAPI.MOUSEEVENTF_MOVE, 21200, 17000, 0, 0);
                WinAPI.mouse_event(WinAPI.MOUSEEVENTF_LEFTDOWN | WinAPI.MOUSEEVENTF_LEFTUP, 1, 1, 0, 0);
                Thread.Sleep(300);
            }
            else if (priceOpt.Equals(PRICE_OPT_BUY_3))
            {
                WinAPI.mouse_event(WinAPI.MOUSEEVENTF_ABSOLUTE | WinAPI.MOUSEEVENTF_MOVE, 21200, 18500, 0, 0);
                WinAPI.mouse_event(WinAPI.MOUSEEVENTF_LEFTDOWN | WinAPI.MOUSEEVENTF_LEFTUP, 1, 1, 0, 0);
                Thread.Sleep(300);
            }
            else if (priceOpt.Equals(PRICE_OPT_BUY_4))
            {
                WinAPI.mouse_event(WinAPI.MOUSEEVENTF_ABSOLUTE | WinAPI.MOUSEEVENTF_MOVE, 21200, 19500, 0, 0);
                WinAPI.mouse_event(WinAPI.MOUSEEVENTF_LEFTDOWN | WinAPI.MOUSEEVENTF_LEFTUP, 1, 1, 0, 0);
                Thread.Sleep(300);
            }
            else if (priceOpt.Equals(PRICE_OPT_BUY_5))
            {
                WinAPI.mouse_event(WinAPI.MOUSEEVENTF_ABSOLUTE | WinAPI.MOUSEEVENTF_MOVE, 21200, 20500, 0, 0);
                WinAPI.mouse_event(WinAPI.MOUSEEVENTF_LEFTDOWN | WinAPI.MOUSEEVENTF_LEFTUP, 1, 1, 0, 0);
                Thread.Sleep(300);
            }
            else if (priceOpt.Equals(PRICE_OPT_ZT))
            {
                WinAPI.mouse_event(WinAPI.MOUSEEVENTF_ABSOLUTE | WinAPI.MOUSEEVENTF_MOVE, 21200, 22500, 0, 0);
                WinAPI.mouse_event(WinAPI.MOUSEEVENTF_LEFTDOWN | WinAPI.MOUSEEVENTF_LEFTUP, 1, 1, 0, 0);
                Thread.Sleep(300);
            }
            else if (priceOpt.Equals(PRICE_OPT_DT))
            {
                WinAPI.mouse_event(WinAPI.MOUSEEVENTF_ABSOLUTE | WinAPI.MOUSEEVENTF_MOVE, 26200, 22500, 0, 0);
                WinAPI.mouse_event(WinAPI.MOUSEEVENTF_LEFTDOWN | WinAPI.MOUSEEVENTF_LEFTUP, 1, 1, 0, 0);
                Thread.Sleep(300);
            }
            else if (priceOpt.Equals(CANCEL_OPT_ALL))
            {
                WinAPI.mouse_event(WinAPI.MOUSEEVENTF_ABSOLUTE | WinAPI.MOUSEEVENTF_MOVE, 21200, 15000, 0, 0);
                WinAPI.mouse_event(WinAPI.MOUSEEVENTF_LEFTDOWN | WinAPI.MOUSEEVENTF_LEFTUP, 1, 1, 0, 0);
                Thread.Sleep(300);
            }
            else if (priceOpt.Equals(PRICE_OPT_SELL_1))
            {
                WinAPI.mouse_event(WinAPI.MOUSEEVENTF_ABSOLUTE | WinAPI.MOUSEEVENTF_MOVE, 21200, 13000, 0, 0);
                WinAPI.mouse_event(WinAPI.MOUSEEVENTF_LEFTDOWN | WinAPI.MOUSEEVENTF_LEFTUP, 1, 1, 0, 0);
                Thread.Sleep(300);
            }
            else if (priceOpt.Equals(PRICE_OPT_SELL_2))
            {
                WinAPI.mouse_event(WinAPI.MOUSEEVENTF_ABSOLUTE | WinAPI.MOUSEEVENTF_MOVE, 21200, 12000, 0, 0);
                WinAPI.mouse_event(WinAPI.MOUSEEVENTF_LEFTDOWN | WinAPI.MOUSEEVENTF_LEFTUP, 1, 1, 0, 0);
                Thread.Sleep(300);
            }
            else if (priceOpt.Equals(PRICE_OPT_SELL_3))
            {
                WinAPI.mouse_event(WinAPI.MOUSEEVENTF_ABSOLUTE | WinAPI.MOUSEEVENTF_MOVE, 21200, 10500, 0, 0);
                WinAPI.mouse_event(WinAPI.MOUSEEVENTF_LEFTDOWN | WinAPI.MOUSEEVENTF_LEFTUP, 1, 1, 0, 0);
                Thread.Sleep(300);
            }
            else if (priceOpt.Equals(PRICE_OPT_SELL_4))
            {
                WinAPI.mouse_event(WinAPI.MOUSEEVENTF_ABSOLUTE | WinAPI.MOUSEEVENTF_MOVE, 21200, 9500, 0, 0);
                WinAPI.mouse_event(WinAPI.MOUSEEVENTF_LEFTDOWN | WinAPI.MOUSEEVENTF_LEFTUP, 1, 1, 0, 0);
                Thread.Sleep(300);
            }
            else if (priceOpt.Equals(PRICE_OPT_SELL_5))
            {
                WinAPI.mouse_event(WinAPI.MOUSEEVENTF_ABSOLUTE | WinAPI.MOUSEEVENTF_MOVE, 21200, 8500, 0, 0);
                WinAPI.mouse_event(WinAPI.MOUSEEVENTF_LEFTDOWN | WinAPI.MOUSEEVENTF_LEFTUP, 1, 1, 0, 0);
                Thread.Sleep(300);
            }
            WinAPI.keybd_event((byte)Keys.Enter, 0, 0, 0);

             //数量-全部
            if (cntOpt.Equals(NUM_OPT_ALL))
            {              
                WinAPI.mouse_event(WinAPI.MOUSEEVENTF_ABSOLUTE | WinAPI.MOUSEEVENTF_MOVE, 15000, 14500, 0, 0);
                WinAPI.mouse_event(WinAPI.MOUSEEVENTF_LEFTDOWN | WinAPI.MOUSEEVENTF_LEFTUP, 1, 1, 0, 0);
                Thread.Sleep(300);
            } else if (cntOpt.Equals(NUM_OPT_INPUT)) {

                //数量
                //WinAPI.mouse_event(WinAPI.MOUSEEVENTF_ABSOLUTE | WinAPI.MOUSEEVENTF_MOVE, 14000, 16200, 0, 0);
                //WinAPI.mouse_event(WinAPI.MOUSEEVENTF_LEFTDOWN | WinAPI.MOUSEEVENTF_LEFTUP, 1, 1, 0, 0);
                //Thread.Sleep(300);

                inputNum(cnt.ToString());
                WinAPI.keybd_event((byte)Keys.Enter, 0, 0, 0);
                Thread.Sleep(300);
            }

            //WinAPI.keybd_event((byte)Keys.Enter, 0, 0, 0);

            //买入下单
            //WinAPI.mouse_event(WinAPI.MOUSEEVENTF_ABSOLUTE | WinAPI.MOUSEEVENTF_MOVE, 15500, 18000, 0, 0);
            //WinAPI.mouse_event(WinAPI.MOUSEEVENTF_LEFTDOWN | WinAPI.MOUSEEVENTF_LEFTUP, 1, 1, 0, 0);
            //Thread.Sleep(300);

            //交易确认
            WinAPI.keybd_event((byte)Keys.Enter, 0, 0, 0);
            Thread.Sleep(300);
            WinAPI.keybd_event((byte)Keys.Enter, 0, 0, 0);
            Thread.Sleep(300);
            WinAPI.keybd_event((byte)Keys.Enter, 0, 0, 0);

            return 0;
        }

        /**
        * 市价买入
        **/
        public static int strategyBuyIn(
            string code,
            string strategy,
            string priceOpt,
            double price,
            string cntOpt,
            int cnt
        )
        {
            //市价委托
            WinAPI.mouse_event(WinAPI.MOUSEEVENTF_ABSOLUTE | WinAPI.MOUSEEVENTF_MOVE, 1200, 13000, 0, 0);
            WinAPI.mouse_event(WinAPI.MOUSEEVENTF_LEFTDOWN | WinAPI.MOUSEEVENTF_LEFTUP, 1, 1, 0, 0);
            Thread.Sleep(300);

            //市价买入
            WinAPI.mouse_event(WinAPI.MOUSEEVENTF_ABSOLUTE | WinAPI.MOUSEEVENTF_MOVE, 2100, 15000, 0, 0);
            WinAPI.mouse_event(WinAPI.MOUSEEVENTF_LEFTDOWN | WinAPI.MOUSEEVENTF_LEFTUP, 1, 1, 0, 0);
            Thread.Sleep(300);

            //先重置
            buyReset1();

            //买入代码
            WinAPI.mouse_event(WinAPI.MOUSEEVENTF_ABSOLUTE | WinAPI.MOUSEEVENTF_MOVE, 15000, 10000, 0, 0);
            WinAPI.mouse_event(WinAPI.MOUSEEVENTF_LEFTDOWN | WinAPI.MOUSEEVENTF_LEFTUP, 1, 1, 0, 0);
            Thread.Sleep(300);

            //输入股票代码
            inputCode(code);
            WinAPI.keybd_event((byte)Keys.Enter, 0, 0, 0);
            Thread.Sleep(300);

            //策略
            WinAPI.mouse_event(WinAPI.MOUSEEVENTF_ABSOLUTE | WinAPI.MOUSEEVENTF_MOVE, 17000, 13200, 0, 0);
            WinAPI.mouse_event(WinAPI.MOUSEEVENTF_LEFTDOWN | WinAPI.MOUSEEVENTF_LEFTUP, 1, 1, 0, 0);
            Thread.Sleep(300);

            //策略1
            if (strategy.Equals(BUY_CR_1))
            {
                WinAPI.mouse_event(WinAPI.MOUSEEVENTF_ABSOLUTE | WinAPI.MOUSEEVENTF_MOVE, 17000, 14100, 0, 0);
                WinAPI.mouse_event(WinAPI.MOUSEEVENTF_LEFTDOWN | WinAPI.MOUSEEVENTF_LEFTUP, 1, 1, 0, 0);
                Thread.Sleep(300);
            }
            else if (strategy.Equals(BUY_CR_2))
            {
                WinAPI.mouse_event(WinAPI.MOUSEEVENTF_ABSOLUTE | WinAPI.MOUSEEVENTF_MOVE, 17000, 15100, 0, 0);
                WinAPI.mouse_event(WinAPI.MOUSEEVENTF_LEFTDOWN | WinAPI.MOUSEEVENTF_LEFTUP, 1, 1, 0, 0);
                Thread.Sleep(300);
            }
            else if (strategy.Equals(BUY_CR_3))
            {
                WinAPI.mouse_event(WinAPI.MOUSEEVENTF_ABSOLUTE | WinAPI.MOUSEEVENTF_MOVE, 17000, 16100, 0, 0);
                WinAPI.mouse_event(WinAPI.MOUSEEVENTF_LEFTDOWN | WinAPI.MOUSEEVENTF_LEFTUP, 1, 1, 0, 0);
                Thread.Sleep(300);
            }
            else if (strategy.Equals(BUY_CR_4))
            {
                WinAPI.mouse_event(WinAPI.MOUSEEVENTF_ABSOLUTE | WinAPI.MOUSEEVENTF_MOVE, 17000, 17100, 0, 0);
                WinAPI.mouse_event(WinAPI.MOUSEEVENTF_LEFTDOWN | WinAPI.MOUSEEVENTF_LEFTUP, 1, 1, 0, 0);
                Thread.Sleep(300);
            }
            else if (strategy.Equals(BUY_CR_5))
            {
                WinAPI.mouse_event(WinAPI.MOUSEEVENTF_ABSOLUTE | WinAPI.MOUSEEVENTF_MOVE, 17000, 18100, 0, 0);
                WinAPI.mouse_event(WinAPI.MOUSEEVENTF_LEFTDOWN | WinAPI.MOUSEEVENTF_LEFTUP, 1, 1, 0, 0);
                Thread.Sleep(300);
            }

            //股票价格
            if (priceOpt.Equals(PRICE_OPT_INPUT))
            {
                //输入价格
                inputNum(price.ToString());
                Thread.Sleep(300);
            }
            else if (priceOpt.Equals(PRICE_OPT_BUY_1))
            {
                WinAPI.mouse_event(WinAPI.MOUSEEVENTF_ABSOLUTE | WinAPI.MOUSEEVENTF_MOVE, 21200, 16000, 0, 0);
                WinAPI.mouse_event(WinAPI.MOUSEEVENTF_LEFTDOWN | WinAPI.MOUSEEVENTF_LEFTUP, 1, 1, 0, 0);
                Thread.Sleep(300);
            }
            else if (priceOpt.Equals(PRICE_OPT_BUY_2))
            {
                WinAPI.mouse_event(WinAPI.MOUSEEVENTF_ABSOLUTE | WinAPI.MOUSEEVENTF_MOVE, 21200, 17000, 0, 0);
                WinAPI.mouse_event(WinAPI.MOUSEEVENTF_LEFTDOWN | WinAPI.MOUSEEVENTF_LEFTUP, 1, 1, 0, 0);
                Thread.Sleep(300);
            }
            else if (priceOpt.Equals(PRICE_OPT_BUY_3))
            {
                WinAPI.mouse_event(WinAPI.MOUSEEVENTF_ABSOLUTE | WinAPI.MOUSEEVENTF_MOVE, 21200, 18500, 0, 0);
                WinAPI.mouse_event(WinAPI.MOUSEEVENTF_LEFTDOWN | WinAPI.MOUSEEVENTF_LEFTUP, 1, 1, 0, 0);
                Thread.Sleep(300);
            }
            else if (priceOpt.Equals(PRICE_OPT_BUY_4))
            {
                WinAPI.mouse_event(WinAPI.MOUSEEVENTF_ABSOLUTE | WinAPI.MOUSEEVENTF_MOVE, 21200, 19500, 0, 0);
                WinAPI.mouse_event(WinAPI.MOUSEEVENTF_LEFTDOWN | WinAPI.MOUSEEVENTF_LEFTUP, 1, 1, 0, 0);
                Thread.Sleep(300);
            }
            else if (priceOpt.Equals(PRICE_OPT_BUY_5))
            {
                WinAPI.mouse_event(WinAPI.MOUSEEVENTF_ABSOLUTE | WinAPI.MOUSEEVENTF_MOVE, 21200, 20500, 0, 0);
                WinAPI.mouse_event(WinAPI.MOUSEEVENTF_LEFTDOWN | WinAPI.MOUSEEVENTF_LEFTUP, 1, 1, 0, 0);
                Thread.Sleep(300);
            }
            else if (priceOpt.Equals(PRICE_OPT_ZT))
            {
                WinAPI.mouse_event(WinAPI.MOUSEEVENTF_ABSOLUTE | WinAPI.MOUSEEVENTF_MOVE, 21200, 22500, 0, 0);
                WinAPI.mouse_event(WinAPI.MOUSEEVENTF_LEFTDOWN | WinAPI.MOUSEEVENTF_LEFTUP, 1, 1, 0, 0);
                Thread.Sleep(300);
            }
            else if (priceOpt.Equals(PRICE_OPT_DT))
            {
                WinAPI.mouse_event(WinAPI.MOUSEEVENTF_ABSOLUTE | WinAPI.MOUSEEVENTF_MOVE, 26200, 22500, 0, 0);
                WinAPI.mouse_event(WinAPI.MOUSEEVENTF_LEFTDOWN | WinAPI.MOUSEEVENTF_LEFTUP, 1, 1, 0, 0);
                Thread.Sleep(300);
            }
            else if (priceOpt.Equals(CANCEL_OPT_ALL))
            {
                WinAPI.mouse_event(WinAPI.MOUSEEVENTF_ABSOLUTE | WinAPI.MOUSEEVENTF_MOVE, 21200, 15000, 0, 0);
                WinAPI.mouse_event(WinAPI.MOUSEEVENTF_LEFTDOWN | WinAPI.MOUSEEVENTF_LEFTUP, 1, 1, 0, 0);
                Thread.Sleep(300);
            }
            else if (priceOpt.Equals(PRICE_OPT_SELL_1))
            {
                WinAPI.mouse_event(WinAPI.MOUSEEVENTF_ABSOLUTE | WinAPI.MOUSEEVENTF_MOVE, 21200, 13000, 0, 0);
                WinAPI.mouse_event(WinAPI.MOUSEEVENTF_LEFTDOWN | WinAPI.MOUSEEVENTF_LEFTUP, 1, 1, 0, 0);
                Thread.Sleep(300);
            }
            else if (priceOpt.Equals(PRICE_OPT_SELL_2))
            {
                WinAPI.mouse_event(WinAPI.MOUSEEVENTF_ABSOLUTE | WinAPI.MOUSEEVENTF_MOVE, 21200, 12000, 0, 0);
                WinAPI.mouse_event(WinAPI.MOUSEEVENTF_LEFTDOWN | WinAPI.MOUSEEVENTF_LEFTUP, 1, 1, 0, 0);
                Thread.Sleep(300);
            }
            else if (priceOpt.Equals(PRICE_OPT_SELL_3))
            {
                WinAPI.mouse_event(WinAPI.MOUSEEVENTF_ABSOLUTE | WinAPI.MOUSEEVENTF_MOVE, 21200, 10500, 0, 0);
                WinAPI.mouse_event(WinAPI.MOUSEEVENTF_LEFTDOWN | WinAPI.MOUSEEVENTF_LEFTUP, 1, 1, 0, 0);
                Thread.Sleep(300);
            }
            else if (priceOpt.Equals(PRICE_OPT_SELL_4))
            {
                WinAPI.mouse_event(WinAPI.MOUSEEVENTF_ABSOLUTE | WinAPI.MOUSEEVENTF_MOVE, 21200, 9500, 0, 0);
                WinAPI.mouse_event(WinAPI.MOUSEEVENTF_LEFTDOWN | WinAPI.MOUSEEVENTF_LEFTUP, 1, 1, 0, 0);
                Thread.Sleep(300);
            }
            else if (priceOpt.Equals(PRICE_OPT_SELL_5))
            {
                WinAPI.mouse_event(WinAPI.MOUSEEVENTF_ABSOLUTE | WinAPI.MOUSEEVENTF_MOVE, 21200, 8500, 0, 0);
                WinAPI.mouse_event(WinAPI.MOUSEEVENTF_LEFTDOWN | WinAPI.MOUSEEVENTF_LEFTUP, 1, 1, 0, 0);
                Thread.Sleep(300);
            }
            WinAPI.keybd_event((byte)Keys.Enter, 0, 0, 0);

            //数量-全部
            if (cntOpt.Equals(NUM_OPT_ALL))
            {
                WinAPI.mouse_event(WinAPI.MOUSEEVENTF_ABSOLUTE | WinAPI.MOUSEEVENTF_MOVE, 17000, 15700, 0, 0);
                WinAPI.mouse_event(WinAPI.MOUSEEVENTF_LEFTDOWN | WinAPI.MOUSEEVENTF_LEFTUP, 1, 1, 0, 0);
                Thread.Sleep(300);
            }
            else if (cntOpt.Equals(NUM_OPT_INPUT))
            {

                //数量
                WinAPI.mouse_event(WinAPI.MOUSEEVENTF_ABSOLUTE | WinAPI.MOUSEEVENTF_MOVE, 17000, 17700, 0, 0);
                WinAPI.mouse_event(WinAPI.MOUSEEVENTF_LEFTDOWN | WinAPI.MOUSEEVENTF_LEFTUP, 1, 1, 0, 0);
                Thread.Sleep(300);

                inputNum(cnt.ToString());
                WinAPI.keybd_event((byte)Keys.Enter, 0, 0, 0);
                Thread.Sleep(300);
            }

            //WinAPI.keybd_event((byte)Keys.Enter, 0, 0, 0);

            //买入下单
            //WinAPI.mouse_event(WinAPI.MOUSEEVENTF_ABSOLUTE | WinAPI.MOUSEEVENTF_MOVE, 15500, 18000, 0, 0);
            //WinAPI.mouse_event(WinAPI.MOUSEEVENTF_LEFTDOWN | WinAPI.MOUSEEVENTF_LEFTUP, 1, 1, 0, 0);
            //Thread.Sleep(300);

            //交易确认
            WinAPI.keybd_event((byte)Keys.Enter, 0, 0, 0);
            Thread.Sleep(300);
            WinAPI.keybd_event((byte)Keys.Enter, 0, 0, 0);
            Thread.Sleep(300);
            WinAPI.keybd_event((byte)Keys.Enter, 0, 0, 0);

            return 0;
        }

        /**
         * 市价卖出
         **/
        public static int strategySell(
            string code,
            string strategy,
            string priceOpt,
            double price,
            string cntOpt,
            int cnt
        )
        {
            //市价委托
            WinAPI.mouse_event(WinAPI.MOUSEEVENTF_ABSOLUTE | WinAPI.MOUSEEVENTF_MOVE, 1200, 13000, 0, 0);
            WinAPI.mouse_event(WinAPI.MOUSEEVENTF_LEFTDOWN | WinAPI.MOUSEEVENTF_LEFTUP, 1, 1, 0, 0);
            Thread.Sleep(300);

            //市价买入
            WinAPI.mouse_event(WinAPI.MOUSEEVENTF_ABSOLUTE | WinAPI.MOUSEEVENTF_MOVE, 2500, 16800, 0, 0);
            WinAPI.mouse_event(WinAPI.MOUSEEVENTF_LEFTDOWN | WinAPI.MOUSEEVENTF_LEFTUP, 1, 1, 0, 0);
            Thread.Sleep(300);

            //先重置
            buyReset1();

            //买入代码
            WinAPI.mouse_event(WinAPI.MOUSEEVENTF_ABSOLUTE | WinAPI.MOUSEEVENTF_MOVE, 15000, 10000, 0, 0);
            WinAPI.mouse_event(WinAPI.MOUSEEVENTF_LEFTDOWN | WinAPI.MOUSEEVENTF_LEFTUP, 1, 1, 0, 0);
            Thread.Sleep(300);

            //输入股票代码
            inputCode(code);
            WinAPI.keybd_event((byte)Keys.Enter, 0, 0, 0);
            Thread.Sleep(300);

            //策略
            WinAPI.mouse_event(WinAPI.MOUSEEVENTF_ABSOLUTE | WinAPI.MOUSEEVENTF_MOVE, 17000, 13200, 0, 0);
            WinAPI.mouse_event(WinAPI.MOUSEEVENTF_LEFTDOWN | WinAPI.MOUSEEVENTF_LEFTUP, 1, 1, 0, 0);
            Thread.Sleep(300);

            //策略1
            if (strategy.Equals(SELL_CR_1))
            {
                WinAPI.mouse_event(WinAPI.MOUSEEVENTF_ABSOLUTE | WinAPI.MOUSEEVENTF_MOVE, 17000, 14100, 0, 0);
                WinAPI.mouse_event(WinAPI.MOUSEEVENTF_LEFTDOWN | WinAPI.MOUSEEVENTF_LEFTUP, 1, 1, 0, 0);
                Thread.Sleep(300);
            }
            else if (strategy.Equals(SELL_CR_2))
            {
                WinAPI.mouse_event(WinAPI.MOUSEEVENTF_ABSOLUTE | WinAPI.MOUSEEVENTF_MOVE, 17000, 15100, 0, 0);
                WinAPI.mouse_event(WinAPI.MOUSEEVENTF_LEFTDOWN | WinAPI.MOUSEEVENTF_LEFTUP, 1, 1, 0, 0);
                Thread.Sleep(300);
            }
            

            //股票价格
            if (priceOpt.Equals(PRICE_OPT_INPUT))
            {
                //输入价格
                inputNum(price.ToString());
                Thread.Sleep(300);
            }
            else if (priceOpt.Equals(PRICE_OPT_BUY_1))
            {
                WinAPI.mouse_event(WinAPI.MOUSEEVENTF_ABSOLUTE | WinAPI.MOUSEEVENTF_MOVE, 21200, 16000, 0, 0);
                WinAPI.mouse_event(WinAPI.MOUSEEVENTF_LEFTDOWN | WinAPI.MOUSEEVENTF_LEFTUP, 1, 1, 0, 0);
                Thread.Sleep(300);
            }
            else if (priceOpt.Equals(PRICE_OPT_BUY_2))
            {
                WinAPI.mouse_event(WinAPI.MOUSEEVENTF_ABSOLUTE | WinAPI.MOUSEEVENTF_MOVE, 21200, 17000, 0, 0);
                WinAPI.mouse_event(WinAPI.MOUSEEVENTF_LEFTDOWN | WinAPI.MOUSEEVENTF_LEFTUP, 1, 1, 0, 0);
                Thread.Sleep(300);
            }
            else if (priceOpt.Equals(PRICE_OPT_BUY_3))
            {
                WinAPI.mouse_event(WinAPI.MOUSEEVENTF_ABSOLUTE | WinAPI.MOUSEEVENTF_MOVE, 21200, 18500, 0, 0);
                WinAPI.mouse_event(WinAPI.MOUSEEVENTF_LEFTDOWN | WinAPI.MOUSEEVENTF_LEFTUP, 1, 1, 0, 0);
                Thread.Sleep(300);
            }
            else if (priceOpt.Equals(PRICE_OPT_BUY_4))
            {
                WinAPI.mouse_event(WinAPI.MOUSEEVENTF_ABSOLUTE | WinAPI.MOUSEEVENTF_MOVE, 21200, 19500, 0, 0);
                WinAPI.mouse_event(WinAPI.MOUSEEVENTF_LEFTDOWN | WinAPI.MOUSEEVENTF_LEFTUP, 1, 1, 0, 0);
                Thread.Sleep(300);
            }
            else if (priceOpt.Equals(PRICE_OPT_BUY_5))
            {
                WinAPI.mouse_event(WinAPI.MOUSEEVENTF_ABSOLUTE | WinAPI.MOUSEEVENTF_MOVE, 21200, 20500, 0, 0);
                WinAPI.mouse_event(WinAPI.MOUSEEVENTF_LEFTDOWN | WinAPI.MOUSEEVENTF_LEFTUP, 1, 1, 0, 0);
                Thread.Sleep(300);
            }
            else if (priceOpt.Equals(PRICE_OPT_ZT))
            {
                WinAPI.mouse_event(WinAPI.MOUSEEVENTF_ABSOLUTE | WinAPI.MOUSEEVENTF_MOVE, 21200, 22500, 0, 0);
                WinAPI.mouse_event(WinAPI.MOUSEEVENTF_LEFTDOWN | WinAPI.MOUSEEVENTF_LEFTUP, 1, 1, 0, 0);
                Thread.Sleep(300);
            }
            else if (priceOpt.Equals(PRICE_OPT_DT))
            {
                WinAPI.mouse_event(WinAPI.MOUSEEVENTF_ABSOLUTE | WinAPI.MOUSEEVENTF_MOVE, 26200, 22500, 0, 0);
                WinAPI.mouse_event(WinAPI.MOUSEEVENTF_LEFTDOWN | WinAPI.MOUSEEVENTF_LEFTUP, 1, 1, 0, 0);
                Thread.Sleep(300);
            }
            else if (priceOpt.Equals(CANCEL_OPT_ALL))
            {
                WinAPI.mouse_event(WinAPI.MOUSEEVENTF_ABSOLUTE | WinAPI.MOUSEEVENTF_MOVE, 21200, 15000, 0, 0);
                WinAPI.mouse_event(WinAPI.MOUSEEVENTF_LEFTDOWN | WinAPI.MOUSEEVENTF_LEFTUP, 1, 1, 0, 0);
                Thread.Sleep(300);
            }
            else if (priceOpt.Equals(PRICE_OPT_SELL_1))
            {
                WinAPI.mouse_event(WinAPI.MOUSEEVENTF_ABSOLUTE | WinAPI.MOUSEEVENTF_MOVE, 21200, 13000, 0, 0);
                WinAPI.mouse_event(WinAPI.MOUSEEVENTF_LEFTDOWN | WinAPI.MOUSEEVENTF_LEFTUP, 1, 1, 0, 0);
                Thread.Sleep(300);
            }
            else if (priceOpt.Equals(PRICE_OPT_SELL_2))
            {
                WinAPI.mouse_event(WinAPI.MOUSEEVENTF_ABSOLUTE | WinAPI.MOUSEEVENTF_MOVE, 21200, 12000, 0, 0);
                WinAPI.mouse_event(WinAPI.MOUSEEVENTF_LEFTDOWN | WinAPI.MOUSEEVENTF_LEFTUP, 1, 1, 0, 0);
                Thread.Sleep(300);
            }
            else if (priceOpt.Equals(PRICE_OPT_SELL_3))
            {
                WinAPI.mouse_event(WinAPI.MOUSEEVENTF_ABSOLUTE | WinAPI.MOUSEEVENTF_MOVE, 21200, 10500, 0, 0);
                WinAPI.mouse_event(WinAPI.MOUSEEVENTF_LEFTDOWN | WinAPI.MOUSEEVENTF_LEFTUP, 1, 1, 0, 0);
                Thread.Sleep(300);
            }
            else if (priceOpt.Equals(PRICE_OPT_SELL_4))
            {
                WinAPI.mouse_event(WinAPI.MOUSEEVENTF_ABSOLUTE | WinAPI.MOUSEEVENTF_MOVE, 21200, 9500, 0, 0);
                WinAPI.mouse_event(WinAPI.MOUSEEVENTF_LEFTDOWN | WinAPI.MOUSEEVENTF_LEFTUP, 1, 1, 0, 0);
                Thread.Sleep(300);
            }
            else if (priceOpt.Equals(PRICE_OPT_SELL_5))
            {
                WinAPI.mouse_event(WinAPI.MOUSEEVENTF_ABSOLUTE | WinAPI.MOUSEEVENTF_MOVE, 21200, 8500, 0, 0);
                WinAPI.mouse_event(WinAPI.MOUSEEVENTF_LEFTDOWN | WinAPI.MOUSEEVENTF_LEFTUP, 1, 1, 0, 0);
                Thread.Sleep(300);
            }
            WinAPI.keybd_event((byte)Keys.Enter, 0, 0, 0);

            //数量-全部
            if (cntOpt.Equals(NUM_OPT_ALL))
            {
                WinAPI.mouse_event(WinAPI.MOUSEEVENTF_ABSOLUTE | WinAPI.MOUSEEVENTF_MOVE, 17000, 15700, 0, 0);
                WinAPI.mouse_event(WinAPI.MOUSEEVENTF_LEFTDOWN | WinAPI.MOUSEEVENTF_LEFTUP, 1, 1, 0, 0);
                Thread.Sleep(300);
            }
            else if (cntOpt.Equals(NUM_OPT_INPUT))
            {

                //数量
                WinAPI.mouse_event(WinAPI.MOUSEEVENTF_ABSOLUTE | WinAPI.MOUSEEVENTF_MOVE, 17000, 17700, 0, 0);
                WinAPI.mouse_event(WinAPI.MOUSEEVENTF_LEFTDOWN | WinAPI.MOUSEEVENTF_LEFTUP, 1, 1, 0, 0);
                Thread.Sleep(300);

                inputNum(cnt.ToString());
                WinAPI.keybd_event((byte)Keys.Enter, 0, 0, 0);
                Thread.Sleep(300);
            }

            //WinAPI.keybd_event((byte)Keys.Enter, 0, 0, 0);

            //买入下单
            //WinAPI.mouse_event(WinAPI.MOUSEEVENTF_ABSOLUTE | WinAPI.MOUSEEVENTF_MOVE, 15500, 18000, 0, 0);
            //WinAPI.mouse_event(WinAPI.MOUSEEVENTF_LEFTDOWN | WinAPI.MOUSEEVENTF_LEFTUP, 1, 1, 0, 0);
            //Thread.Sleep(300);

            //交易确认
            WinAPI.keybd_event((byte)Keys.Enter, 0, 0, 0);
            Thread.Sleep(300);
            WinAPI.keybd_event((byte)Keys.Enter, 0, 0, 0);
            Thread.Sleep(300);
            WinAPI.keybd_event((byte)Keys.Enter, 0, 0, 0);

            return 0;
        }


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
            //卖出按钮
            WinAPI.mouse_event(WinAPI.MOUSEEVENTF_ABSOLUTE | WinAPI.MOUSEEVENTF_MOVE, 1200, 8000, 0, 0);
            WinAPI.mouse_event(WinAPI.MOUSEEVENTF_LEFTDOWN | WinAPI.MOUSEEVENTF_LEFTUP, 1, 1, 0, 0);
            Thread.Sleep(300);

            //先重置
            buyReset();

            //卖出代码
            WinAPI.mouse_event(WinAPI.MOUSEEVENTF_ABSOLUTE | WinAPI.MOUSEEVENTF_MOVE, 15000, 10000, 0, 0);
            WinAPI.mouse_event(WinAPI.MOUSEEVENTF_LEFTDOWN | WinAPI.MOUSEEVENTF_LEFTUP, 1, 1, 0, 0);
            Thread.Sleep(300);

            //输入股票代码
            if (!String.IsNullOrEmpty(code))
            {
                inputCode(code);
                WinAPI.keybd_event((byte)Keys.Enter, 0, 0, 0);
                Thread.Sleep(300);
            }

            //股票价格
            if (priceOpt.Equals(PRICE_OPT_INPUT))
            {
                //输入价格
                inputNum(price.ToString());
                Thread.Sleep(300);
            }
            else if (priceOpt.Equals(PRICE_OPT_BUY_1))
            {
                WinAPI.mouse_event(WinAPI.MOUSEEVENTF_ABSOLUTE | WinAPI.MOUSEEVENTF_MOVE, 21200, 16000, 0, 0);
                WinAPI.mouse_event(WinAPI.MOUSEEVENTF_LEFTDOWN | WinAPI.MOUSEEVENTF_LEFTUP, 1, 1, 0, 0);
                Thread.Sleep(300);
            }
            else if (priceOpt.Equals(PRICE_OPT_BUY_2))
            {
                WinAPI.mouse_event(WinAPI.MOUSEEVENTF_ABSOLUTE | WinAPI.MOUSEEVENTF_MOVE, 21200, 17000, 0, 0);
                WinAPI.mouse_event(WinAPI.MOUSEEVENTF_LEFTDOWN | WinAPI.MOUSEEVENTF_LEFTUP, 1, 1, 0, 0);
                Thread.Sleep(300);
            }
            else if (priceOpt.Equals(PRICE_OPT_BUY_3))
            {
                WinAPI.mouse_event(WinAPI.MOUSEEVENTF_ABSOLUTE | WinAPI.MOUSEEVENTF_MOVE, 21200, 18500, 0, 0);
                WinAPI.mouse_event(WinAPI.MOUSEEVENTF_LEFTDOWN | WinAPI.MOUSEEVENTF_LEFTUP, 1, 1, 0, 0);
                Thread.Sleep(300);
            }
            else if (priceOpt.Equals(PRICE_OPT_BUY_4))
            {
                WinAPI.mouse_event(WinAPI.MOUSEEVENTF_ABSOLUTE | WinAPI.MOUSEEVENTF_MOVE, 21200, 19500, 0, 0);
                WinAPI.mouse_event(WinAPI.MOUSEEVENTF_LEFTDOWN | WinAPI.MOUSEEVENTF_LEFTUP, 1, 1, 0, 0);
                Thread.Sleep(300);
            }
            else if (priceOpt.Equals(PRICE_OPT_BUY_5))
            {
                WinAPI.mouse_event(WinAPI.MOUSEEVENTF_ABSOLUTE | WinAPI.MOUSEEVENTF_MOVE, 21200, 20500, 0, 0);
                WinAPI.mouse_event(WinAPI.MOUSEEVENTF_LEFTDOWN | WinAPI.MOUSEEVENTF_LEFTUP, 1, 1, 0, 0);
                Thread.Sleep(300);
            }
            else if (priceOpt.Equals(PRICE_OPT_ZT))
            {
                WinAPI.mouse_event(WinAPI.MOUSEEVENTF_ABSOLUTE | WinAPI.MOUSEEVENTF_MOVE, 21200, 22500, 0, 0);
                WinAPI.mouse_event(WinAPI.MOUSEEVENTF_LEFTDOWN | WinAPI.MOUSEEVENTF_LEFTUP, 1, 1, 0, 0);
                Thread.Sleep(300);
            }
            else if (priceOpt.Equals(PRICE_OPT_DT))
            {
                WinAPI.mouse_event(WinAPI.MOUSEEVENTF_ABSOLUTE | WinAPI.MOUSEEVENTF_MOVE, 26200, 22500, 0, 0);
                WinAPI.mouse_event(WinAPI.MOUSEEVENTF_LEFTDOWN | WinAPI.MOUSEEVENTF_LEFTUP, 1, 1, 0, 0);
                Thread.Sleep(300);
            }
            else if (priceOpt.Equals(CANCEL_OPT_ALL))
            {
                WinAPI.mouse_event(WinAPI.MOUSEEVENTF_ABSOLUTE | WinAPI.MOUSEEVENTF_MOVE, 21200, 15000, 0, 0);
                WinAPI.mouse_event(WinAPI.MOUSEEVENTF_LEFTDOWN | WinAPI.MOUSEEVENTF_LEFTUP, 1, 1, 0, 0);
                Thread.Sleep(300);
            }
            else if (priceOpt.Equals(PRICE_OPT_SELL_1))
            {
                WinAPI.mouse_event(WinAPI.MOUSEEVENTF_ABSOLUTE | WinAPI.MOUSEEVENTF_MOVE, 21200, 13000, 0, 0);
                WinAPI.mouse_event(WinAPI.MOUSEEVENTF_LEFTDOWN | WinAPI.MOUSEEVENTF_LEFTUP, 1, 1, 0, 0);
                Thread.Sleep(300);
            }
            else if (priceOpt.Equals(PRICE_OPT_SELL_2))
            {
                WinAPI.mouse_event(WinAPI.MOUSEEVENTF_ABSOLUTE | WinAPI.MOUSEEVENTF_MOVE, 21200, 12000, 0, 0);
                WinAPI.mouse_event(WinAPI.MOUSEEVENTF_LEFTDOWN | WinAPI.MOUSEEVENTF_LEFTUP, 1, 1, 0, 0);
                Thread.Sleep(300);
            }
            else if (priceOpt.Equals(PRICE_OPT_SELL_3))
            {
                WinAPI.mouse_event(WinAPI.MOUSEEVENTF_ABSOLUTE | WinAPI.MOUSEEVENTF_MOVE, 21200, 10500, 0, 0);
                WinAPI.mouse_event(WinAPI.MOUSEEVENTF_LEFTDOWN | WinAPI.MOUSEEVENTF_LEFTUP, 1, 1, 0, 0);
                Thread.Sleep(300);
            }
            else if (priceOpt.Equals(PRICE_OPT_SELL_4))
            {
                WinAPI.mouse_event(WinAPI.MOUSEEVENTF_ABSOLUTE | WinAPI.MOUSEEVENTF_MOVE, 21200, 9500, 0, 0);
                WinAPI.mouse_event(WinAPI.MOUSEEVENTF_LEFTDOWN | WinAPI.MOUSEEVENTF_LEFTUP, 1, 1, 0, 0);
                Thread.Sleep(300);
            }
            else if (priceOpt.Equals(PRICE_OPT_SELL_5))
            {
                WinAPI.mouse_event(WinAPI.MOUSEEVENTF_ABSOLUTE | WinAPI.MOUSEEVENTF_MOVE, 21200, 8500, 0, 0);
                WinAPI.mouse_event(WinAPI.MOUSEEVENTF_LEFTDOWN | WinAPI.MOUSEEVENTF_LEFTUP, 1, 1, 0, 0);
                Thread.Sleep(300);
            }
            WinAPI.keybd_event((byte)Keys.Enter, 0, 0, 0);

            //数量-全部
            if (cntOpt.Equals(NUM_OPT_ALL))
            {
                WinAPI.mouse_event(WinAPI.MOUSEEVENTF_ABSOLUTE | WinAPI.MOUSEEVENTF_MOVE, 15000, 14500, 0, 0);
                WinAPI.mouse_event(WinAPI.MOUSEEVENTF_LEFTDOWN | WinAPI.MOUSEEVENTF_LEFTUP, 1, 1, 0, 0);
                Thread.Sleep(300);
            }
            else if (cntOpt.Equals(NUM_OPT_INPUT))
            {
                //数量
                //WinAPI.mouse_event(WinAPI.MOUSEEVENTF_ABSOLUTE | WinAPI.MOUSEEVENTF_MOVE, 14000, 16200, 0, 0);
                //WinAPI.mouse_event(WinAPI.MOUSEEVENTF_LEFTDOWN | WinAPI.MOUSEEVENTF_LEFTUP, 1, 1, 0, 0);
                //Thread.Sleep(300);

                inputNum(cnt.ToString());
                WinAPI.keybd_event((byte)Keys.Enter, 0, 0, 0);
                Thread.Sleep(300);
            }

            //WinAPI.keybd_event((byte)Keys.Enter, 0, 0, 0);

            //卖出下单
            //WinAPI.mouse_event(WinAPI.MOUSEEVENTF_ABSOLUTE | WinAPI.MOUSEEVENTF_MOVE, 15500, 18000, 0, 0);
            //WinAPI.mouse_event(WinAPI.MOUSEEVENTF_LEFTDOWN | WinAPI.MOUSEEVENTF_LEFTUP, 1, 1, 0, 0);
            //Thread.Sleep(300);

            //交易确认
            WinAPI.keybd_event((byte)Keys.Enter, 0, 0, 0);
            Thread.Sleep(300);
            WinAPI.keybd_event((byte)Keys.Enter, 0, 0, 0);
            Thread.Sleep(300);
            WinAPI.keybd_event((byte)Keys.Enter, 0, 0, 0);

            return 0;
        }



        /**
      * 自选股
      * */
        public static int selfSel(
            string code,string flag
        )
        {
            WinAPI.mouse_event(WinAPI.MOUSEEVENTF_ABSOLUTE | WinAPI.MOUSEEVENTF_MOVE, 300, 27800, 0, 0);
            WinAPI.mouse_event(WinAPI.MOUSEEVENTF_LEFTDOWN | WinAPI.MOUSEEVENTF_LEFTUP, 1, 1, 0, 0);
            Thread.Sleep(300);           

            //卖出按钮
            //WinAPI.mouse_event(WinAPI.MOUSEEVENTF_ABSOLUTE | WinAPI.MOUSEEVENTF_MOVE, 10400, 10800, 0, 0);
            //WinAPI.mouse_event(WinAPI.MOUSEEVENTF_LEFTDOWN | WinAPI.MOUSEEVENTF_LEFTUP, 1, 1, 0, 0);
            //Thread.Sleep(300);           

            //输入股票代码
            if (!String.IsNullOrEmpty(code))
            {
                inputCode(code);               
                WinAPI.keybd_event((byte)Keys.Enter, 0, 0, 0);
                Thread.Sleep(300);

                if ("add".Equals(flag))
                {
                    WinAPI.keybd_event((byte)Keys.Insert, 0, 0, 0);
                }
                else if ("del".Equals(flag))
                {
                    WinAPI.keybd_event((byte)Keys.Delete, 0, 0, 0);
                }
               
            }            

            return 0;
        }





        /**
        * 持仓卖出
        * */
        public static int storeSell(string priceOpt,
            double price,
            string cntOpt,
            int cnt)
        {
            //仓内卖出
            WinAPI.mouse_event(WinAPI.MOUSEEVENTF_ABSOLUTE | WinAPI.MOUSEEVENTF_MOVE, 1200, 6000, 0, 0);
            WinAPI.mouse_event(WinAPI.MOUSEEVENTF_LEFTDOWN | WinAPI.MOUSEEVENTF_LEFTUP, 1, 1, 0, 0);

            Thread.Sleep(300);

            //列表记录坐标
            WinAPI.mouse_event(WinAPI.MOUSEEVENTF_ABSOLUTE | WinAPI.MOUSEEVENTF_MOVE, 17000, 29510, 0, 0);
            WinAPI.mouse_event(WinAPI.MOUSEEVENTF_LEFTDOWN | WinAPI.MOUSEEVENTF_LEFTUP, 1, 1, 0, 0);
            WinAPI.mouse_event(WinAPI.MOUSEEVENTF_LEFTDOWN | WinAPI.MOUSEEVENTF_LEFTUP, 1, 1, 0, 0);
            Thread.Sleep(300);

            //卖出
            sellOut("", priceOpt,price,cntOpt,cnt);

            return 0;
        }

        /**
         * 委托撤销
         * */
        public static int agentCancel(string code , string cancelOpt)
        {
            //撤单
            WinAPI.mouse_event(WinAPI.MOUSEEVENTF_ABSOLUTE | WinAPI.MOUSEEVENTF_MOVE, 1200, 10000, 0, 0);
            WinAPI.mouse_event(WinAPI.MOUSEEVENTF_LEFTDOWN | WinAPI.MOUSEEVENTF_LEFTUP, 1, 1, 0, 0);

            Thread.Sleep(300);

            if (!String.IsNullOrEmpty(code))
            {
                WinAPI.mouse_event(WinAPI.MOUSEEVENTF_ABSOLUTE | WinAPI.MOUSEEVENTF_MOVE, 24200, 7000, 0, 0);
                WinAPI.mouse_event(WinAPI.MOUSEEVENTF_LEFTDOWN | WinAPI.MOUSEEVENTF_LEFTUP, 1, 1, 0, 0);
                WinAPI.mouse_event(WinAPI.MOUSEEVENTF_LEFTDOWN | WinAPI.MOUSEEVENTF_LEFTUP, 1, 1, 0, 0);
                WinAPI.keybd_event((byte)Keys.Delete, 0, 0, 0);
                Thread.Sleep(300);

                inputCode(code);
                WinAPI.keybd_event((byte)Keys.Enter, 0, 0, 0);
                Thread.Sleep(300);

                WinAPI.mouse_event(WinAPI.MOUSEEVENTF_ABSOLUTE | WinAPI.MOUSEEVENTF_MOVE, 12500, 7000, 0, 0);
                WinAPI.mouse_event(WinAPI.MOUSEEVENTF_LEFTDOWN | WinAPI.MOUSEEVENTF_LEFTUP, 1, 1, 0, 0);
                WinAPI.keybd_event((byte)Keys.Enter, 0, 0, 0);
                WinAPI.keybd_event((byte)Keys.Enter, 0, 0, 0);

                return 0;
            }

            //列表记录坐标
            if (cancelOpt.Equals(CANCEL_OPT_FRIST))
            {
                WinAPI.mouse_event(WinAPI.MOUSEEVENTF_ABSOLUTE | WinAPI.MOUSEEVENTF_MOVE, 11200, 11100, 0, 0);
                WinAPI.mouse_event(WinAPI.MOUSEEVENTF_LEFTDOWN | WinAPI.MOUSEEVENTF_LEFTUP, 1, 1, 0, 0);
                WinAPI.mouse_event(WinAPI.MOUSEEVENTF_LEFTDOWN | WinAPI.MOUSEEVENTF_LEFTUP, 1, 1, 0, 0);
                Thread.Sleep(300);
            }
            else if (cancelOpt.Equals(CANCEL_OPT_ALL))
            {
                //全撤
                WinAPI.mouse_event(WinAPI.MOUSEEVENTF_ABSOLUTE | WinAPI.MOUSEEVENTF_MOVE, 16000, 7000, 0, 0);
                WinAPI.mouse_event(WinAPI.MOUSEEVENTF_LEFTDOWN | WinAPI.MOUSEEVENTF_LEFTUP, 1, 1, 0, 0);
                Thread.Sleep(300);
            }
            else if (cancelOpt.Equals(CANCEL_OPT_SELL_ALL))
            {
                //全撤卖
                WinAPI.mouse_event(WinAPI.MOUSEEVENTF_ABSOLUTE | WinAPI.MOUSEEVENTF_MOVE, 22000, 7000, 0, 0);
                WinAPI.mouse_event(WinAPI.MOUSEEVENTF_LEFTDOWN | WinAPI.MOUSEEVENTF_LEFTUP, 1, 1, 0, 0);
                Thread.Sleep(300);
            }
            else if (cancelOpt.Equals(CANCEL_OPT_BUY_ALL))
            {
                //全撤买
                WinAPI.mouse_event(WinAPI.MOUSEEVENTF_ABSOLUTE | WinAPI.MOUSEEVENTF_MOVE, 19000, 7000, 0, 0);
                WinAPI.mouse_event(WinAPI.MOUSEEVENTF_LEFTDOWN | WinAPI.MOUSEEVENTF_LEFTUP, 1, 1, 0, 0);
                Thread.Sleep(300);
            }
           
            WinAPI.keybd_event((byte)Keys.Enter, 0, 0, 0);
            Thread.Sleep(300);
            WinAPI.keybd_event((byte)Keys.Enter, 0, 0, 0);
            return 0;
        }

    }
}
