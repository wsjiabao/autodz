using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using MySql.Data.MySqlClient;
using System.IO;
using System.Windows.Forms;

namespace MdTZ
{
    class GPUtil
    {
        /// <summary>
        /// 
        /// 所有股票代码
        /// </summary>
        /// 
        public static bool isTest = true;
        public static string codes = "";
        public static MySqlHelper helper = new MySqlHelper();
        public static MySqlParameter[] parms = new MySqlParameter[] { };

        /**
         * 当前交易日
         * */
        public static DateTime nowTranDate { get; set; }

        public static bool isTranTime()
        {
            if (isTest)
            {
                return true;
            }
            //后面不是
            if (Convert.ToInt16(DateTimeHelper.GetWeekNumberOfDay(DateTime.Now)) >= 6)
            {
                return false;
            }    

            //9 下午3点以后不做了
            if (DateTime.Now.Hour <= 9 || DateTime.Now.Hour >= 16)
            {
                return false;
            }

            return true;
        }

        public static void setTodayTranDay() {

             //最近交易日                      
            if (Convert.ToInt16(DateTimeHelper.GetWeekNumberOfDay(DateTime.Now)) >= 6)
            {
                GPUtil.nowTranDate = Convert.ToDateTime(SinaAPI.getGPList("sh600006")[0].date).Date;
            }
            else
            {
                GPUtil.nowTranDate = DateTime.Now.Date;
            }

            //GPUtil.nowTranDate = DateTime.Now.Date;

            GPUtil.updateTranDate();
        }

        public static void updateTranDate()
        {
            GPUtil.helper.ExecuteNonQuery("update gpparam set jydate='" + GPUtil.nowTranDate + "'");
        }


        public static void write(string msg)
        {

            Console.WriteLine(msg);
            //当前程序目录
            string logPath = Path.GetDirectoryName(Application.ExecutablePath);
            //新建文件
            System.IO.StreamWriter sw = System.IO.File.AppendText(logPath + "/日志.txt");
            sw.WriteLine(DateTime.Now+" :" + msg);

            sw.Close();
            sw.Dispose();

        }

        /**
         * 获取涨停价格
         * */
        public static double getUpPrice(String code)
        {
            string sql = "SELECT  h.`zrspj`,h.`zrspj`*(1-0.1) dtj,h.`zrspj`*(1+0.1) ztj FROM gpsinahis h WHERE h.`code` = '"
                +code+"' and Date(h.date)='"+GPUtil.nowTranDate+"'";
            DataRow row = helper.ExecuteDataRow(sql, parms);

            if (row != null)
            {
                return Math.Round(Convert.ToDouble(row["ztj"]), 2)-0.05;
            }

            return 0;
        }


        /**
        * 大盘历史涨幅
        * */
        public static string getDPHis()
        {
            string his = "";
            string sql = "SELECT h.his FROM 大盘历史 h WHERE h.`code` = 'sh000001'";
            DataRow row = helper.ExecuteDataRow(sql, parms);

            if (row != null)
            {
                his = row["his"].ToString();
            }

            return his;
        }

        /**
        * 获取跌停价格
        * */
        public static double getLowPrice(String code)
        {
            string sql = "SELECT h.`zrspj`,h.`zrspj`*(1-0.1) dtj,h.`zrspj`*(1+0.1) ztj FROM gpsinahis h WHERE h.`code` = '" 
                + code + "' and Date(h.date)='" + GPUtil.nowTranDate + "'";
            DataRow row = helper.ExecuteDataRow(sql, parms);

            if (row != null)
            {
                return Math.Round(Convert.ToDouble(row["dtj"]), 2) + 0.05;
            }

            return 0;
        }

        /**
         * 
         * 大盘瞬时涨跌幅
         * */
        public static double getRealDpZf(DaoPan dp)
        {
            if (GPTotalAPI.dpMap.ContainsKey(dp.code))
            {
                DaoPan old_dp = GPTotalAPI.dpMap[dp.code];
                if (old_dp.zs > 0)
                {
                    return Math.Round(((dp.zs - old_dp.zs) / old_dp.zs) * 100, 2);
                }
            }
            return 0;
        }

        /**
        * 
        * 股票成本价涨幅
        * */
        public static void setGpCostPriceZf(GuoPiao gp)
        {
            if (GPTotalAPI.gpMap.ContainsKey(gp.code))
            {
                GpTotal total = GPTotalAPI.gpMap[gp.code];
                if (total != null)
                {
                    total.dqj = gp.dqj;
                    total.buyzf =  Math.Round(((gp.dqj - total.costPrice) / total.costPrice) * 100, 3);
                    gp.total = total;
                }
            }           
           
        }


    }
}
