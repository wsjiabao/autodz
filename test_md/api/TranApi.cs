using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace MdTZ
{
    class TranApi
    {

        #region 初始化对象
        public static bool isTraning = false;
     
        public static List<string> buyCodes = new List<string>();     
        public static List<string> sellCodes = new List<string>();

        public static string tickCodes = "sh000001,sz399001,sz399006,";
        public static string tickSqlCodes = "'sh000001','sz399001','sz399006',";

        public static List<GpTranBean> buyedCodes = new List<GpTranBean>();
        public static List<GpTranBean> selledCodes = new List<GpTranBean>();

        //交易时间-交易次数
        public static Dictionary<string, int> tranBuyTimes = new Dictionary<string, int>{
             {"9.35-9.50",1},{"14.30-14.50",1},{"19.00-23.30",1}
        };

        public static Dictionary<string, int> tranSellTimes = new Dictionary<string, int>{
             {"9.40-9.50",1},{"13.30-13.50",1},{"19.00-23.30",1}
        };

        //public static Dictionary<string, int> tranTimes = new Dictionary<string, int>();

        #endregion


        #region 交易入口

        /// <summary>
        /// 发起买入处理
        /// </summary>
        /// <returns></returns>
        public static int do_buy_process(List<GuoPiao> dpgps, List<GuoPiao> buygps)
        {

            if (isTraning)
            {
                return 0;
            }

            isTraning = true;
            //double qsz = QuShi.qsz;                        

            string real_code = "";
            string log_str = "";
            int buyCnt = 0;
            int buyNums = 1;

            //不在时间计划内
            if (StaUtil.getTranTimeNow(tranBuyTimes) <= 0)
            {
                isTraning = false;
                return 0;
            }

            foreach (GuoPiao gp in buygps)
            {
                real_code = StaUtil.getTransCode(gp.code);

                //根据当前仓位获取股票数                
                GPUtil.write("开始买入:" + real_code);

                //发起交易
                //THSAPI.buyIn(real_code, THSAPI.PRICE_OPT_SELL_2, 0, THSAPI.NUM_OPT_INPUT, 1 * 100); 
                //ZXApi.buyIn(real_code, THSAPI.PRICE_OPT_SELL_2, 0, THSAPI.NUM_OPT_INPUT, buyNums * 100);  
                ZXApi.buyIn_cur(real_code, THSAPI.NUM_OPT_INPUT, buyNums * 100);

                //更新交易数据                 
                GPUtil.helper.ExecuteNonQuery("INSERT INTO gpsell (CODE,cbj,num,flag,buydate,DATE) values ('" + gp.code + "',"
                        + gp.dqj + "," + buyNums * 100 + ",0,'" + DateTime.Now + "','" + DateTime.Now + "')");

                log_str = "结束买入[" + real_code + "] 数量:" + buyNums * 100 + " 价格:" + gp.dqj;
                GPUtil.write(log_str);

                buyCnt++;

            }

            isTraning = false;
            return buyCnt;
        }

        /// <summary>
        /// 卖出
        /// </summary>
        /// <param name="dpgps"></param>
        /// <param name="sellgps"></param>
        /// <returns></returns>
        public static int do_sell_process(List<GuoPiao> dpgps, List<GuoPiao> sellgps)
        {
            if (isTraning)
            {
                return 0;
            }

            isTraning = true;
            int rtnSells = 0;

            /**
             * 卖出先决条件
             **/
            string real_code = "";
            string log_str = "";

            //不在时间计划内
            if (StaUtil.getTranTimeNow(tranSellTimes) <= 0)
            {
                isTraning = false;
                return 0;
            }

            foreach (GuoPiao gp in sellgps)
            {
                real_code = StaUtil.getTransCode(gp.code);
                log_str = "开始卖出[" + gp.code + "]";
                GPUtil.write(log_str);

                //方法测试
                //THSAPI.sellOut(real_code, THSAPI.PRICE_OPT_BUY_2, 0, THSAPI.NUM_OPT_INPUT, 100);  
                //ZXApi.sellOut(real_code, THSAPI.PRICE_OPT_BUY_2, 0, THSAPI.NUM_OPT_INPUT, 100,0); 
                ZXApi.sellOut_cur(real_code, THSAPI.NUM_OPT_INPUT, 100);

                //更新交易数据                 
                GPUtil.helper.ExecuteNonQuery("update gpsell set num = num-100 where code='" + gp.code + "' and num > 0");

                log_str = "结束卖出[" + real_code + "] 数量:" + 1 * 100;
                GPUtil.write(log_str);

                rtnSells++;

            }

            isTraning = false;

            return rtnSells;
        }

        #endregion




     


       
    }
}
