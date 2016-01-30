using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using MySql.Data.MySqlClient;

namespace MdTZ
{
    class HisDataAPI
    {


        /**
       * 从sina 获取当天数据更新到 gp 表
       * */
        public static void synGpInfoFromSina()
        {
            ///**
            // * 检查是否已经更新
            // * */
            MySqlHelper helper = new MySqlHelper();
            MySqlParameter[] parms = new MySqlParameter[] { };

            string codes = GPUtil.codes;
            List<GuoPiao> gps = SinaAPI.getGPList(codes);

            Console.WriteLine("gps.cnt:" + gps.Count.ToString());

            StringBuilder sql = new StringBuilder();
            List<string> sqlList = new List<string>();
            bool result = false;
            foreach (GuoPiao gp in gps)
            {
                sql.Clear();

                sql.Append("UPDATE gp SET NAME='"+gp.name+"' WHERE CODE = '"+gp.code+"';");

                sqlList.Add(sql.ToString());

            }

            //批量插入
            result = MysqlHelper1.ExecuteNoQueryTran(sqlList);
            sqlList.Clear();

            Console.WriteLine("dsynGpInfoFromSinaresult:" + result);
        }

        /**
        * 从sina 获取当天数据
        getGpsForLoadYaohHis
        * */
        private static string getGpsForLoadYaohHis()
        {
            MySqlHelper helper = new MySqlHelper();
            MySqlParameter[] parms = new MySqlParameter[] { };
            DataTable dataTable = helper.ExecuteDataTable("SELECT code FROM gp where updhis='0'", parms);
            StringBuilder codes = new StringBuilder();
            if (dataTable != null)
            {
                foreach (DataRow row in dataTable.Rows)
                {
                    codes.Append(row["code"]).Append(",");
                }
            }
            //Console.Write("codes:" + codes.ToString());
            return codes.ToString();
        }


        public static string getGpCodesFromDb()
        {
            
            DataTable dataTable = GPUtil.helper.ExecuteDataTable("SELECT code FROM gp order by code", GPUtil.parms);
            StringBuilder codes = new StringBuilder();
            if (dataTable != null)
            {
                foreach (DataRow row in dataTable.Rows)
                {
                    codes.Append(row["code"]).Append(",");
                }
            }
            //Console.Write("codes:" + codes.ToString());
            return codes.ToString();
        }

        public static List<string> getLoadCwInfoCodes()
        {

            DataTable dataTable = GPUtil.helper.ExecuteDataTable("SELECT CODE FROM gp WHERE CODE NOT IN (SELECT DISTINCT CODE FROM gpcw)", GPUtil.parms);
            List<string> codes = new List<string>();
            if (dataTable != null)
            {
                foreach (DataRow row in dataTable.Rows)
                {
                    codes.Add((string)row["code"]);
                }
            }
            //Console.Write("codes:" + codes.ToString());
            return codes;
        }

        /**
         * 从雅虎获取股票历史数据存款数据库
         * */
        public static void loadHisDataFromYaohu(string code, DateTime bdate, DateTime edate)
        {
            //历史数据
            DataTable hisData = SinaAPI.getGuoPiaoHisDataFromYaoh(bdate, edate, code);

            if (hisData == null || hisData.Rows.Count <= 0)
            {
                return;
            }

            //保存到数据库
            hisData.TableName = "gpyaohhis";

            MySqlHelper help = new MySqlHelper();
            MySqlHelper.BulkInsert(help.ConnectionString, hisData);

        }

        /**
         * 获取所有股票实时历史
         **/
        public static void loadGpTimeHis(String codes)
        {
            //清空表                   
            char[] ch = new char[] { ',' };
            string[] gpcodeList = codes.Split(ch);
          
            foreach (string code in gpcodeList)
            {                                  
                loadTimeHisDataFromSina(code);
            }
        }

        /**
         * 从雅虎获取股票历史数据存款数据库
         * */
        public static void loadTimeHisDataFromSina(string code)
        {

            //历史数据
            Console.WriteLine("clear data:" + GPUtil.helper.ExecuteNonQuery("delete from gptimehis where code='" + code + "'"));

            DataTable hisData = SinaAPI.getGuoPiaoTimeHisFromSina(code,GPUtil.nowTranDate);
            if (hisData == null || hisData.Rows.Count <= 0)
            {
                return;
            }
           
            Console.WriteLine("hisData.rows.count:" + hisData.Rows.Count.ToString());

            //保存到数据库
            hisData.TableName = "gptimehis";
          
            MySqlHelper.BulkInsert(GPUtil.helper.ConnectionString, hisData);

        }

        /**
         * 从雅虎获取股票历史数据存款数据库
         * */
        public static void loadAllHisDataFromYaohu(DateTime bdate, DateTime edate)
        {
            string gpcodes = getGpsForLoadYaohHis();

            char[] ch = new char[] { ',' };
            string[] gpcodeList = gpcodes.Split(ch);
            string code = "";
            DataTable hisData = null;
            MySqlHelper help = new MySqlHelper();

            //已经保存的历史数据
            MySqlParameter[] parms = new MySqlParameter[] { };
            DataTable codeDatas = help.ExecuteDataTable("SELECT DISTINCT(g.Code) FROM gpyaohhis g where g.AdjClose IS NOT NULL", parms);
            StringBuilder sb = new StringBuilder();
            foreach (DataRow row in codeDatas.Rows)
            {
                code = row["code"].ToString();
                if (code.IndexOf("sh") != -1)
                {
                    code = code.Replace("sh", "");
                    code = code + ".ss";
                }
                else
                {
                    code = code.Replace("sz", "");
                    code = code + ".sz";
                }
                sb.Append(code).Append(",");
            }
            string saveCodes = sb.ToString();
            if (!string.IsNullOrEmpty(saveCodes))
            {
                saveCodes = saveCodes.Substring(0, saveCodes.LastIndexOf(","));
            }
            //Console.WriteLine("####saveCodes:[" + saveCodes + "]");

            //个股http://table.finance.yahoo.com/table.csv?a=9&b=24&c=2015&d=10&e=23&f=2015&s=603883.ss
            string tmpCode = "";
            string oldCode = "";
            string excodes = "399006,603021";
            for (int i = 0; i < gpcodeList.Length; i++)
            {
                code = gpcodeList[i];
                oldCode = code;
                tmpCode = code.Replace("sh", "").Replace("sz", "");
                if (String.IsNullOrEmpty(code))
                {
                    continue;
                }               

                if (code.IndexOf("sh") != -1)
                {
                    code = code.Replace("sh", "");
                    code = code + ".ss";
                }
                else
                {
                    code = code.Replace("sz", "");
                    code = code + ".sz";
                }

                if (saveCodes.IndexOf(code) != -1 || excodes.IndexOf(tmpCode) != -1)
                {
                    continue;
                }

                Console.WriteLine("开始[" + code + "]");

                //历史数据
                try
                {
                    hisData = SinaAPI.getGuoPiaoHisDataFromYaoh(bdate, edate, code);
                }
                catch (Exception e)
                {
                    Console.Write(e.Message.ToString());
                    Console.Write("update cnt:"+help.ExecuteNonQuery("update gp set updhis='1' where code='" + oldCode + "'"));
                    continue;
                }
                if (hisData == null || hisData.Rows.Count <= 0)
                {
                    return;
                }
                Console.Write(hisData.Rows.Count.ToString());
                //保存到数据库
                hisData.TableName = "gpyaohhis";
                MySqlHelper.BulkInsert(help.ConnectionString, hisData);

                Console.WriteLine("结束[" + code + "]");
            }

        }

        /**
          * 从sina 获取当天数据
          * */
        public static void sinaDataInsertToYaoh()
        {
            /**
             * 检查是否已经更新
             * */
            string checkCode = "sh600009";
            //List<GuoPiao> c_gp = SinaAPI.getGPList(checkCode);          
            string checkSql = "SELECT * FROM gpyaohhis WHERE CODE = '" + checkCode + "' and Date(date)='" + GPUtil.nowTranDate + "'";
            DataRow row = GPUtil.helper.ExecuteDataRow(checkSql, GPUtil.parms);

            if (row != null)
            {
                Console.WriteLine("check update:has Updated");
                return;
            }

            Console.WriteLine("check update:no Update");

            string codes = GPUtil.codes;
            List<GuoPiao> gps = SinaAPI.getGPList(codes);

            Console.WriteLine("gps.cnt:" + gps.Count.ToString());

            StringBuilder sql = new StringBuilder();
            List<string> sqlList = new List<string>();
            bool result = false;
           
            foreach(GuoPiao gp in gps)
            {
                sql.Clear();
               
              
                   sql.Append("insert into gpyaohhis(Code,Open,Date,Close,Volume,High,Low) values ('")
                   .Append(gp.code).Append("',")
                   .Append(gp.kpj).Append(",'")
                   .Append(gp.date).Append("',")
                   .Append(gp.dqj).Append(",")
                   .Append(gp.cj_num).Append(",").Append(gp.jrzgj).Append(",").Append(gp.jrzdj)
                   .Append(");");

                   sqlList.Add(sql.ToString());
               
            }

            //批量插入
            result = MysqlHelper1.ExecuteNoQueryTran(sqlList);
            sqlList.Clear();

            Console.WriteLine("dp end result:" + result);

        }

        /**
         * 从sina 获取当天数据
         * */
        public static void saveTodayHisDataFromSina()
        {

            Console.WriteLine("clear today data:" + 
                GPUtil.helper.ExecuteNonQuery("delete from gptoday", GPUtil.parms));

            string codes = GPUtil.codes;

            List<GuoPiao> gps = null;
            List<DaoPan> dpList = null;
            StringBuilder sql = new StringBuilder();

            try
            {
                gps = SinaAPI.getGPList(codes);
                dpList = SinaAPI.getDPList("dqs");
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return;
            }

            if (dpList != null && dpList.Count > 0)
            {
                DaoPan nsdk = dpList[0];
              
                sql.Append("insert into gptoday(code,dqj,cj_num,zf,date) values ('")
                    .Append(nsdk.code).Append("',")
                    .Append(nsdk.zs).Append(",")
                    .Append(nsdk.cjl).Append(",")
                    .Append(nsdk.zdl).Append(",'")
                    .Append(GPUtil.nowTranDate).Append("')");

                GPUtil.helper.ExecuteNonQuery(sql.ToString(), GPUtil.parms);
                Console.WriteLine("sql:" + sql.ToString());
                Console.WriteLine("gps.cnt:" + gps.Count.ToString());
            }

            if (gps == null || gps.Count == 0)
            {
                return;
            }
                                          
            List<string> sqlList = new List<string>();
            bool result = false;
            foreach (GuoPiao gp in gps)
            {
                sql.Clear();

                sql.Append("insert into gptoday(code,dqj,zrspj,jrzgj,jrzdj,cj_num,cj_amt,zf,date,zhengf,jj,tr) values ('")
                .Append(gp.code).Append("',")
                .Append(gp.dqj).Append(",")
                .Append(gp.zrspj).Append(",")
                .Append(gp.jrzgj).Append(",")
                .Append(gp.jrzdj).Append(",")
                .Append(gp.cj_num).Append(",")
                .Append(gp.cj_amt/100000000).Append(",")
                .Append(gp.zf).Append(",'")
                .Append(gp.date).Append("',")
                .Append(gp.zhengf)
                .Append(",").Append(gp.jj).Append(",").Append(gp.tr).Append(");");

                sqlList.Add(sql.ToString());
                
            }

            //批量插入
            if (sqlList.Count > 0)
            {
                result = MysqlHelper1.ExecuteNoQueryTran(sqlList);
            }          
            sqlList.Clear();

            if (gps.Count > 1)
            {
                DataRow row = GPUtil.helper.ExecuteDataRow("select max(id) m_id from gptoday");
                long m_id = Convert.ToInt64(row["m_id"]);
                GPUtil.helper.ExecuteNonQuery("delete from gptoday where id=" + m_id);
                Console.WriteLine("dp end result:" + result);
            }           

        }

        public static void refreshRMB()
        {
            //外汇
            double rmbRate = 0;
            try
            {
                rmbRate = JsonApi.getRMBRate();
            }
            catch (Exception e)
            {
                Console.WriteLine("JsonApi.getRMBRate().error:" + e.Message);
                GPUtil.write(e.Message);
            }

            if (rmbRate != null && rmbRate > 0)
            {
                Console.WriteLine("######refreshRMB:" + rmbRate);
                GPUtil.helper.ExecuteNonQuery("update currency set lastcur="
                    + rmbRate + " where curcode='CNY'", GPUtil.parms);
                GPUtil.helper.ExecuteNonQuery("update currency set zf=(lastcur-befcur)/befcur*100 where curcode='CNY'", GPUtil.parms);
            }
           
        }

        public static void refreshRMBLast()
        {
           
            GPUtil.helper.ExecuteNonQuery("update currency set befcur=lastcur where curcode='CNY'", GPUtil.parms);
        }

        public static void insertGpCw(GpCw cw)
        {
            if (cw != null && !String.IsNullOrEmpty(cw.code))
            {
               if (!checkLoadCwFlag(cw.code,cw.date)) { 
                    GPUtil.helper.ExecuteNonQuery("insert into gpcw (code,mgjzc,mgsy,mgxjhl,zyywsr,jll,date) values ('"
                        + cw.code + "'," + cw.mgjzc + ","+cw.mgsy+","+cw.mgxjhl+","+cw.zyywsr+","+cw.jll+",'"+cw.date+"')");
               }           
            }           
        }

        public static bool checkLoadCwFlag(string code,DateTime date)
        {
            if (!String.IsNullOrEmpty(code))
            {

                DataRow row = null;
                if (date != null)
                {
                    row = GPUtil.helper.ExecuteDataRow("select * from gpcw h where h.code='" + code + "' and h.date='" + date + "'", GPUtil.parms);
                } else
                {
                    row = GPUtil.helper.ExecuteDataRow("select * from gpcw h where h.code='" + code + "'", GPUtil.parms);
                }
               
                if (row == null)
                {
                    return false;
                }
            }

            return true;
        }

    }
}
