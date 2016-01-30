using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;

namespace MdTZ
{
    class RealDataApi
    {
        public static List<GnZJ> gnZjSorts = new List<GnZJ>();
        public static List<GnZJ> hyZjSorts = new List<GnZJ>();


        public static string getRequest(string url)
        {
            string strURL = url;
            System.Net.HttpWebRequest request;
            request = (System.Net.HttpWebRequest)WebRequest.Create(strURL);
            request.Timeout = 2000;
            request.Method = "GET";           
            System.Net.HttpWebResponse response;
            response = (System.Net.HttpWebResponse)request.GetResponse();
            System.IO.Stream s;
            s = response.GetResponseStream();
            string StrDate = "";
            string strValue = "";
            StreamReader Reader = new StreamReader(s, Encoding.Default);
            while ((StrDate = Reader.ReadLine()) != null)
            {
                strValue += StrDate;
            }
            return strValue;
        }

        /// <summary>
        /// 更新概念资金排序列表
        /// </summary>
        public static void updGnZjSort()
        {
            DataTable result  = GPUtil.helper.ExecuteDataTable("select keystr,remark from 概念资金");
            GnZJ gn = null;
            foreach(DataRow r in result.Rows) {
                gn = new GnZJ();
                gn.gnname = r["keystr"].ToString();
                gn.zb = Convert.ToDouble(r["remark"].ToString().Replace("%", ""));
                gnZjSorts.Add(gn);
            }
        }


        /// <summary>
        /// 更新行业资金排序列表
        /// </summary>
        public static void updHyZjSort()
        {
            DataTable result = GPUtil.helper.ExecuteDataTable("select keystr,remark from 行业资金");
            GnZJ gn = null;
            foreach (DataRow r in result.Rows)
            {
                gn = new GnZJ();
                gn.gnname = r["keystr"].ToString();
                gn.zb = Convert.ToDouble(r["remark"].ToString().Replace("%", ""));
                hyZjSorts.Add(gn);
            }
        }


        /// <summary>
        /// 
        /// 优选实时股票获取 7最高 (1-7) 档
        /// </summary>
        /// <returns></returns>
        public static List<GuoPiao> getYXRealGuoPiao(int lev)
        {
            if (lev < 0 || lev > 7)
            {
                return null;
            }
            List<GuoPiao> rntGps = new List<GuoPiao>();           

            //初始化, 
            HisDataAPI.saveTodayHisDataFromSina();          

            MySqlParameter lev_param = new MySqlParameter("?lev", MySqlDbType.VarChar, 1);
            lev_param.Value = lev;
            MySqlParameter[] parms = new MySqlParameter[] { lev_param };
            DataSet ds = GPUtil.helper.ExecuteProc("gp_sort", parms);
            DataTable result = ds.Tables[0];
            GuoPiao gp = null;
            foreach (DataRow r in result.Rows)
            {
                gp = new GuoPiao();
                gp.code = r["code"].ToString();
                gp.name = r["name"].ToString();
                gp.zf = Convert.ToDouble(r["zf"]);
                gp.jrzgj = Convert.ToDouble(r["jrzgj"]);
                gp.jrzdj = Convert.ToDouble(r["jrzdj"]);
                gp.dqj = Convert.ToDouble(r["dqj"]);
                gp.jj = Convert.ToDouble(r["jj"]);
                gp.hsl = Convert.ToDouble(r["hsl"]);
                gp.zhengf = Convert.ToDouble(r["zhengf"]);
                gp.ltsz = Convert.ToDouble(r["ltsz"]);
                gp.syl = Convert.ToDouble(r["syl"]);
                rntGps.Add(gp);
            }

            return rntGps;
        }


        public static void loadTimeHis()
        {
            DataTable dataTable = GPUtil.helper.
               ExecuteDataTable("SELECT s.code FROM gpsel s where flag = -1 limit 0,8"
               , GPUtil.parms);
           
            string code = "";
         
            if (dataTable != null)
            {
                foreach (DataRow row in dataTable.Rows)
                {
                    code = row["code"].ToString().Trim();
                    int b_sec = DateTime.Now.Second;
                    HisDataAPI.loadTimeHisDataFromSina(code);
                    Console.WriteLine("cost sec:" + (DateTime.Now.Second - b_sec));

                    GPUtil.helper.ExecuteNonQuery("update gpsel set flag=-2 where code='" + code + "'");
                }
            }
           
        }




    }
}
