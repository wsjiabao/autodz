using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace MdTZ
{
    class StaUtil
    {
        /// <summary>
        /// 获取交易BEAN
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public static List<GuoPiao> getTransBean(List<GuoPiao> gps, string type)
        {
            if (gps == null)
            {
                return null;
            }
            List<GuoPiao> rtnBeans = new List<GuoPiao>();

            if ("dp".Equals(type))
            {
                for (int i = 0; i < 2; i++)
                {
                    rtnBeans.Add(gps[i]);
                }
            }
            else if ("buy".Equals(type))
            {
                foreach(GuoPiao gp in gps) {
                    if (TranApi.buyCodes.Contains(gp.code))
                    {
                        rtnBeans.Add(gp);
                    }
                }
            }
            else if ("sell".Equals(type))
            {
                foreach (GuoPiao gp in gps)
                {
                    if (TranApi.sellCodes.Contains(gp.code))
                    {
                        rtnBeans.Add(gp);
                    }
                }
            }

            return rtnBeans;
        }

        /// <summary>
        /// 获取可用金额
        /// </summary>
        /// <returns></returns>
        public static double getCanBuyAmt()
        {
            DataRow row = GPUtil.helper.ExecuteDataRow("SELECT IFNULL(s.zjye,0) * (d.cw/100) je FROM dpqs d,gpparam p,store s WHERE d.type=p.dpqs AND p.code=s.code");
            double je = 0;
            if (row == null)
            {
                return 0;
            }
            if (row["je"] != null && !string.IsNullOrEmpty(row["je"].ToString()))
            {
                je = Convert.ToDouble(row["je"]);
            }
            return je;
        }

        /// <summary>
        /// 
        /// 检查是否已经买入
        /// </summary>
        /// <returns></returns>
        public static bool check_is_buyed(string code)
        {
            DataRow row = GPUtil.helper.ExecuteDataRow("select count(*) cnt from gpsell where code='" + code + "'");
            if (row != null && Convert.ToInt16(row["cnt"]) > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// 更新买入，卖出后状态
        /// </summary>
        public static void synBuyOrSellStatus()
        {

            foreach (GpTranBean tran in TranApi.buyedCodes)
            {
                //更新状态
                GPUtil.helper.ExecuteNonQuery("update gpbuy set flag=1 where code='" + tran.code + "'");
                GPUtil.helper.ExecuteNonQuery("INSERT INTO gpsell (CODE,cbj,num,flag,buydate,DATE) values ('" + tran.code + "',"
                    + tran.price + "," + tran.nums + ",0,'" + tran.date + "','" + GPUtil.nowTranDate + "')");
            }

            foreach (GpTranBean tran in TranApi.selledCodes)
            {
                //更新状态
                GPUtil.helper.ExecuteNonQuery("update gpsell set flag=1 where code='" + tran.code + "'");
            }
        }

        public static void add_buys_codes()
        {

            TranApi.buyCodes.Clear();

            DataTable codes = GPUtil.helper.ExecuteDataTable("select distinct b.code from gpbuy b where b.flag = 0 order by date desc");
            string code;
            foreach (DataRow r in codes.Rows)
            {
                code = r["code"].ToString();

                if (TranApi.tickCodes.IndexOf(code) == -1)
                {
                    TranApi.tickCodes += code + ",";
                    TranApi.tickSqlCodes += "'" + code + "',";
                }

                TranApi.buyCodes.Add(code);
            }

        }

        public static void add_sells_codes()
        {
            TranApi.sellCodes.Clear();

            DataTable codes = GPUtil.helper.ExecuteDataTable("select distinct code from gpsell where flag = 0");
            string code = "";
            foreach (DataRow r in codes.Rows)
            {
                code = r["code"].ToString();
                if (TranApi.tickCodes.IndexOf(code) == -1)
                {
                    TranApi.tickCodes += code + ",";
                    TranApi.tickSqlCodes += "'" + code + "',";
                }
                TranApi.sellCodes.Add(code);
            }

        }

        public static string tranPlan(string time)
        {
            string a = time.Replace(":", ".");
            return a.Substring(0, a.LastIndexOf('.'));
        }

        /// <summary>
        /// 获取交易计划
        /// </summary>
        public static void loadTranPlan()
        {
            string type = GPUtil.helper.ExecuteDataRow("select qsnow from gpparam")["qsnow"].ToString();
            DataTable ts = GPUtil.helper.ExecuteDataTable("select btime,etime,cnt from tranplan where type='" + type + "' order by btime");
            foreach (DataRow r in ts.Rows)
            {
                TranApi.tranBuyTimes.Add(tranPlan(r["btime"].ToString()) + "-" + tranPlan(r["etime"].ToString()), Convert.ToInt16(r["cnt"]));
            }
        }

        public static string getTransCode(string code)
        {
            return code.Replace("sh", "").Replace("sz", "");
        }

        /// <summary>
        /// 
        /// 交易时间控制
        /// </summary>
        /// <returns></returns>
        public static int getTranTimeNow(Dictionary<string, int> tranTimes)
        {
            double nowTime = Convert.ToDouble(DateTime.Now.Hour.ToString() + "." + DateTime.Now.Minute.ToString());
            double b_time = 0;
            double e_time = 0;
            int rtn = 0;
            foreach (string t_ran in tranTimes.Keys)
            {
                b_time = Convert.ToDouble(t_ran.Split("-".ToCharArray())[0]);
                e_time = Convert.ToDouble(t_ran.Split("-".ToCharArray())[1]);

                if (nowTime >= b_time && nowTime <= e_time)
                {
                    rtn = tranTimes[t_ran];
                    tranTimes[t_ran] = rtn - 1;

                    return rtn;
                }

            }
            return -1;
        }



    }
}
