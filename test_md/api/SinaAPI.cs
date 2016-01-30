using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;
using System.Threading;
using System.Windows.Forms;
using System.Data;
using System.IO;
using System.Text.RegularExpressions;


namespace MdTZ
{
    public class SinaAPI
    {

        public static Dictionary<String, String> cwData = null;

        //private void fromYaoh_Click(object sender, EventArgs e)
        //{
        //    DateTime sdate = DateTime.Now.AddDays(-30);
        //    HisDataAPI.loadAllHisDataFromYaohu(sdate, DateTime.Now);
        //}

        /**
         * 转化URL code
         * */
        public static string getUrlCode(string code)
        {
            string rtnCode = code;
            if (code.IndexOf("sh") == -1 || code.IndexOf("sz") == -1)
            {
                if (code.IndexOf("6") == 0)
                {
                    rtnCode = "sh" + code;
                }
                else if (code.IndexOf("0") == 0 || code.IndexOf("3") == 0)
                {
                    rtnCode = "sz" + code;
                }
            }

            return rtnCode;
        }

        /**
         * 
         * 转化URL CODES
         * */
        private static string converToUrlCode(string code)
        {
            string[] codes = code.Split(",".ToCharArray());
            string rtnUrlCode = "";
            for (int i = 0; i < codes.Length; i++)
            {
                rtnUrlCode += getUrlCode(codes[i]) + ",";
            }

            return rtnUrlCode;
        }

        /**
        * 获取个股数据
        * */
        public static string getGPDataFromSina(string code
        )
        {
            //转化code
            if (String.IsNullOrEmpty(code))
            {
                return "";
            }

            code = converToUrlCode(code);

            //HttpClient client = new HttpClient("http://hq.sinajs.cn/list=" + code);
            //string html = client.GetString();
            string html = "";
            try
            {
                html = RealDataApi.getRequest("http://hq.sinajs.cn/list=" + code);
            }
            catch (Exception e)
            {
                GPUtil.write(e.Message);
            }           
            //Console.WriteLine("RealDataApi.getRequest.str:" + html);
            return html;
        }

        /**
     * 获取个股数据
         * 1, s_sh000001,s_sz399001,s_sz399006
         * 2, s_sz399001
         * 3, s_sz399006
     * */
        public static List<DaoPan> getDPList( string type
        )
        {
            string code = "s_sh000001";
            if ("0".Equals(type))
            {
                code = "s_sh000001";
            }
            else if ("1".Equals(type))
            {
                code = "s_sz399001";
            }
            else if ("2".Equals(type))
            {
                code = "s_sz399006";
            }
            else if ("nsdk".Equals(type))
            {
                code = "int_nasdaq";
            }
            else if ("dqs".Equals(type))
            {
                code = "int_dji";
            }
            else if ("hs".Equals(type))
            {
                code = "int_hangseng";
            }
            else
            {
                code = "s_sh000001,s_sz399001,s_sz399006,int_dji,int_nasdaq,int_hangseng";
            }
            //HttpClient client = new HttpClient("http://hq.sinajs.cn/list=" + code);
            //string data = client.GetString();
            string data = RealDataApi.getRequest("http://hq.sinajs.cn/list=" + code);
            if (String.IsNullOrEmpty(data))
            {
                return null;
            }
            else
            {
                data = data.Replace("\r\n", "").Replace("\"","");
            }

            List<DaoPan> dpList = new List<DaoPan>();
            string[] fieldList = data.Split(";".ToCharArray());
            string[] fields = null;
            string[] dpCodes = code.Split(",".ToCharArray());
            DaoPan dp = null;
            for (int i = 0; i < fieldList.Length; i++)
            {
                fields = fieldList[i].Split(",".ToCharArray());

                if (fields.Length <= 1)
                {
                    continue;
                }
                dp = new DaoPan();
                dp.code = dpCodes[i];
                dp.name = fields[0]; //指数名称
                dp.zs = Convert.ToDouble(fields[1]); //当前点数
                dp.zds = Convert.ToDouble(fields[2]); //当前价格
                dp.zdl = Convert.ToDouble(fields[3]); //涨跌率

                if (fields.Length > 4)
                {
                    dp.cjl = Convert.ToInt32(fields[4]);//成交量（手）
                    dp.cje = Convert.ToDouble(fields[5].Replace("\"", ""));//成交额（万元）
                }
               

                //本次涨幅
                double real_zf = GPUtil.getRealDpZf(dp);
                dp.real_zf = real_zf;
                
                dpList.Add(dp);
            }

            return dpList;
        }

        /**
    * 获取个股数据
    * */
        public static List<GuoPiao> getGPList(string code
        )
        {
            if (String.IsNullOrEmpty(code))
            {
                Console.WriteLine("code is null");
                return null;
            }
            string[] gpCodes = code.Split(",".ToCharArray());
            List<GuoPiao> gpList = new List<GuoPiao>();
            StringBuilder subCodes = new StringBuilder();
            String data = "";
            for (int i = 0; i < gpCodes.Length; i++)
            {
                subCodes.Append(gpCodes[i]).Append(",");

                if (i > 0 && i % 500 == 0)
                {
                    data = getGPDataFromSina(subCodes.ToString());
                    if (String.IsNullOrEmpty(data))
                    {
                        return null;
                    }
                    else
                    {
                        data = data.Replace("\r\n", "");
                    }

                    string[] fieldList = data.Split(";".ToCharArray());
                    gpList.AddRange(getSubGPList(fieldList, subCodes.ToString()));

                    subCodes.Clear();
                }

            }

            if (!String.IsNullOrEmpty(subCodes.ToString()))
            {
                data = getGPDataFromSina(subCodes.ToString());
                if (String.IsNullOrEmpty(data))
                {
                    return null;
                }
                else
                {
                    data = data.Replace("\r\n", "");
                }

                string[] fieldList = data.Split(";".ToCharArray());
                gpList.AddRange(getSubGPList(fieldList, subCodes.ToString()));
            }            
           
            Console.WriteLine("gpList.size:" + gpList.Count.ToString());
            return gpList;
        }

        /**
         * 
         * 获取子集合
         * */
        private static List<GuoPiao> getSubGPList(string[] fieldList, string codes)
        {
            string[] fields = null;
            string[] codeslist = codes.Split(",".ToCharArray());
            List<GuoPiao> gpList = new List<GuoPiao>();
            GuoPiao gp = null;
            for (int i = 0; i < fieldList.Length; i++)
            {
                fields = fieldList[i].Split(",".ToCharArray());
                if (fields.Length <= 1)
                {
                    continue;
                }
                gp = new GuoPiao();
                gp.code = codeslist[i];
                gp.name = fields[0];
                if (gp.name.IndexOf("道琼斯") != -1
                    || gp.name.IndexOf("纳斯达克") != -1
                    || gp.name.IndexOf("恒生指数") != -1)
                {
                    gp.zf = Convert.ToDouble(fields[3].Replace("\"",""));
                    continue;
                }
                gp.kpj = Convert.ToDouble(fields[1]); //1：”27.55″，今日开盘价；
                gp.zrspj = Convert.ToDouble(fields[2]); //2：”27.25″，昨日收盘价；
                gp.dqj = Convert.ToDouble(fields[3]); //3：”26.91″，当前价格；                
                gp.jrzgj = Convert.ToDouble(fields[4]);//4：”27.55″，今日最高价；
                gp.jrzdj = Convert.ToDouble(fields[5]);//5：”26.20″，今日最低价；
                gp.b_now = Convert.ToDouble(fields[6]);//6：”26.91″，竞买价，即“买一”报价；
                gp.s_now = Convert.ToDouble(fields[7]);//7：”26.92″，竞卖价，即“卖一”报价；
                gp.cj_num = Convert.ToDouble(fields[8]);//8：”22114263″，成交的股票数，由于股票交易以一百股为基本单位，所以在使用时，通常把该值除以一百；
                gp.cj_amt = Convert.ToDouble(fields[9]);//9：”589824680″，成交金额，单位为“元”，为了一目了然，通常以“万元”为成交金额的单位，所以通常把该值除以一万；
                gp.b1_num = Convert.ToDouble(fields[10]);//10：”4695″，“买一”申请4695股，即47手；
                gp.b1_price = Convert.ToDouble(fields[11]);//11：”26.91″，“买一”报价；
                gp.b2_num = Convert.ToDouble(fields[12]);//12：”57590″，“买二”
                gp.b2_price = Convert.ToDouble(fields[13]);//13：”26.90″，“买二”
                gp.b3_num = Convert.ToDouble(fields[14]);//14：”14700″，“买三”
                gp.b3_price = Convert.ToDouble(fields[15]);//15：”26.89″，“买三”
                gp.b4_num = Convert.ToDouble(fields[16]);//16：”14300″，“买四”
                gp.b4_price = Convert.ToDouble(fields[17]);//17：”26.88″，“买四”
                gp.b5_num = Convert.ToDouble(fields[18]); //18：”15100″，“买五”
                gp.b5_price = Convert.ToDouble(fields[19]); //19：”26.87″，“买五”
                gp.s1_num = Convert.ToDouble(fields[20]);//20：”3100″，“卖一”申报3100股，即31手；
                gp.s1_price = Convert.ToDouble(fields[21]);//21：”26.92″，“卖一”报价
                gp.s2_num = Convert.ToDouble(fields[22]);//20：”3100″，“卖一”申报3100股，即31手；
                gp.s2_price = Convert.ToDouble(fields[23]);//21：”26.92″，“卖一”报价
                gp.s3_num = Convert.ToDouble(fields[24]);//20：”3100″，“卖一”申报3100股，即31手；
                gp.s3_price = Convert.ToDouble(fields[25]);//21：”26.92″，“卖一”报价
                gp.s4_num = Convert.ToDouble(fields[26]);//20：”3100″，“卖一”申报3100股，即31手；
                gp.s4_price = Convert.ToDouble(fields[27]);//21：”26.92″，“卖一”报价
                gp.s5_num = Convert.ToDouble(fields[28]);//20：”3100″，“卖一”申报3100股，即31手；
                gp.s5_price = Convert.ToDouble(fields[29]);//21：”26.92″，“卖一”报价
                gp.date = fields[30];//30：”2008-01-11″，日期；
                gp.time = fields[31];//31：”15:05:32″，时间；

                //振幅
                gp.zhengf = Math.Round(((gp.jrzgj - gp.jrzdj) / gp.zrspj) * 100, 2);

                //分时均价
                if (gp.cj_num > 0)
                {
                    gp.jj = Math.Round(gp.cj_amt / gp.cj_num, 2);
                }

                //涨幅
                if (gp.zrspj > 0)
                {
                    gp.zf = Math.Round(((gp.dqj - gp.zrspj) / gp.zrspj) * 100,2); //涨幅
                }

                double real_zf = GPUtil.getRealGpZf(gp);
                gp.real_zf = real_zf;

                //tr 计算
                if (((gp.jrzgj - gp.jrzdj) - Math.Abs(gp.zrspj - gp.jrzgj) > 0) 
                    )
                {
                    if ((gp.jrzgj - gp.jrzdj) > Math.Abs(gp.zrspj - gp.jrzdj))
                    {
                        gp.tr = gp.jrzgj - gp.jrzdj;
                    }
                    else
                    {
                        gp.tr = Math.Abs(gp.zrspj - gp.jrzdj);
                    }
                }
                else
                {
                    if (Math.Abs(gp.zrspj - gp.jrzgj) > Math.Abs(gp.zrspj - gp.jrzdj))
                    {
                        gp.tr = Math.Abs(gp.zrspj - gp.jrzgj);
                    }
                    else
                    {
                        gp.tr = Math.Abs(gp.zrspj - gp.jrzdj);
                    }
                }

                gpList.Add(gp);
            }

            return gpList;
        }       

        /**
       *  获取雅虎 股票代码
       * */
        private static string getYaohUrlCode(string code)
        {
            string rtnCode = code;
            if (String.IsNullOrEmpty(code))
            {
                return rtnCode;
            }
            else if (code.IndexOf(".ss") != -1 || code.IndexOf(".sz") != -1 || code.IndexOf(".SZ") != -1)
            {
                return rtnCode;
            }

            if (code.IndexOf("6") == 0)
            {
                rtnCode = code + ".ss";
            }
            else if (code.IndexOf("0") == 0 || code.IndexOf("3") == 0)
            {
                rtnCode = code + ".sz";
            }

            return rtnCode;
        }

        /**
         * 获取股票，大盘历史数据
         * 
         * 直接在浏览器地址中数据网址即可
            http://table.finance.yahoo.com/table.csv?s=股票代码
            上证股票是股票代码后面加上.ss，深证股票是股票代码后面加上.sz
            深市数据链接：http://table.finance.yahoo.com/table.csv?s=000001.sz
            上市数据链接：http://table.finance.yahoo.com/table.csv?s=600000.ss
            另外，上证综指代码：000001.ss，深证成指代码：399001.SZ，沪深300代码：000300.ss
            例如查询中国石油的历史数据，直接在浏览器中输入：http://table.finance.yahoo.com/table.csv?s=601857.ss
         * */
        public static DataTable getGuoPiaoHisDataFromYaoh(DateTime bdate, DateTime edate, string code)
        {

            if (String.IsNullOrEmpty(code))
            {
                return null;
            }
            else
            {
                code = getYaohUrlCode(code);
            }

            //雅虎历史数据
            string url = "http://table.finance.yahoo.com/table.csv?" + "a=" + (bdate.Month - 1).ToString()
                + "&b=" + bdate.Day.ToString() + "&c=" + bdate.Year.ToString()
                + "&d=" + (edate.Month - 1).ToString() + "&e=" + edate.Day.ToString()
                + "&f=" + edate.Year.ToString() + "&s=" + code;
            Console.WriteLine(url);
            HttpClient client = null;
            DataTable datatable = null;
                   
            try
            {
                client = new HttpClient(url);
                datatable = CSVFileHelper.OpenCSVFromSteam(code, client.GetStream());
            }
            catch (Exception e)
            {
                client.Reset();
                throw e;
            }
            
            return datatable;
        }

        /**
           * 获取股票分时数据
           * 
           * http://market.finance.sina.com.cn/downxls.php?date=2011-07-08&symbol=sh600900
                获取代码为sh600900，在2011-07-08的成交明细，数据为xls格式。
                http://vip.stock.finance.sina.com.cn/quotes_service/view/cn_price.php?symbol=sh600900
                获得sh600900当日的分价表
                http://market.finance.sina.com.cn/pricehis.php?symbol=sh600900&startdate=2011-08-17&enddate=2011-08-19
                获得sh600900从2011-08-17到2011-08-19的分价表。
         * */
        public static DataTable getGuoPiaoTimeHisFromSina(string code,DateTime date)
        {

            if (String.IsNullOrEmpty(code))
            {
                return null;
            }

            //雅虎历史数据
            string url = "http://market.finance.sina.com.cn/downxls.php?date=" + date.Date.ToShortDateString() + "&symbol="+code;
            Console.WriteLine(url);
            HttpClient client = null;
            DataTable datatable = null;
            client = new HttpClient(url);
            Stream stream = null;
            try
            {
                stream = client.GetStream();
                datatable = CSVFileHelper.OpenCSVFromSteam(code, stream);
            }
            catch (Exception e)
            {
                client.Reset();
                Console.WriteLine("no data to get today");
            }

            return datatable;
        }
      
        /**
        * 财务数据获取

                    截止日期:2015-09-30
        每股净资产:3.5447元
        每股收益:0.2917元
        每股现金含量:-0.3729元
        每股资本公积金:1.1351元
        固定资产合计: 
        流动资产合计:21832500万元
        资产总计:32033600万元
        长期负债合计:2588090万元
        主营业务收入:14855500万元
        财务费用:75539.6万元
        净利润:796077万元
        **/
        public static List<GpCw> getGpCaiWuInfo(WebBrowser web)
        {          
            HtmlElementCollection ElementCollection = web.Document.GetElementsByTagName("Table");
            cwData = new Dictionary<String, String>();

            HtmlElementCollection subCols = null;
            GpCw cw = null;
            List<GpCw> cws = new List<GpCw>();
            int cnt = 0;
            foreach (HtmlElement item in ElementCollection)
            {                
                if ("FundHoldSharesTable".Equals(item.GetAttribute("id")))
                {
                   
                    foreach (HtmlElement subItem in item.GetElementsByTagName("Tr"))
                    {

                        cnt = subItem.GetElementsByTagName("td").Count;
                        if (cnt == 2)
                        {
                            subCols = subItem.GetElementsByTagName("td");

                            if (!String.IsNullOrEmpty(subCols[0].InnerText))
                            {
                                cwData.Add(subCols[0].InnerText.Trim(), subCols[1].InnerText.Trim());
                            }
                        }
                       
                        if (cwData.Keys.Contains("净利润"))
                        {
                            cw = convertToCw(cwData);
                            cws.Add(cw);

                            cwData.Clear();
                        }
                    }
                }               
            }         

            return cws;

        }

        private static string getGpCodeFromUrl(string url)
        {
            string rtnStr = url.Replace("http://vip.stock.finance.sina.com.cn/corp/go.php/vFD_FinanceSummary/stockid/", "")
                .Replace("/displaytype/4.phtml", "");
            return rtnStr;
        }

        /**
         WEB 获取内容回调
        **/    
        public static void webGetCwCallBack(WebBrowser gp_web, List<string> codeList, List<string> loadCodeList)
        {
            if (gp_web.Url.ToString().Equals("about:blank"))
            {
                return;
            }

            string code = "";
            if (codeList.Count > 0)
            {
                code = codeList[0].Replace("sh", "").Replace("sz", "");
            }

            string url = "";
            if (!String.IsNullOrEmpty(code))
            {
                url = "http://vip.stock.finance.sina.com.cn/corp/go.php/vFD_FinanceSummary/stockid/" + code
                     + "/displaytype/4.phtml";
                gp_web.Url = new Uri(url);

                if (!loadCodeList.Contains(code))
                {
                    Console.WriteLine("财务:" + url.ToString());
                  
                    List<GpCw> cws= SinaAPI.getGpCaiWuInfo(gp_web);

                    foreach (GpCw cw in cws)
                    {
                        if (cw != null)
                        {
                            cw.code = getGpCodeFromUrl(gp_web.Url.ToString());
                            cw.code = SinaAPI.getUrlCode(cw.code);
                            HisDataAPI.insertGpCw(cw);
                            Console.WriteLine(gp_web.Url.ToString());
                        }
                    }

                   
                    loadCodeList.Add(code);

                    codeList.RemoveAt(0);

                    gp_web.Navigate(url);

                }

            }
        }

        private static GpCw convertToCw(Dictionary<String, String> data)
        {

            GpCw cw = null;
            string mgxjhl_str = "";

            string mgjzc = "0";
            string mgsy = "0";
            string mgxjlh = "0";
            string zyywsr = "0";
            string jlr = "0";
            string jzrq = "";
            if (data != null && data.Count > 0)
            {
                cw = new GpCw();

                mgjzc = data["每股净资产"].Replace("元", "");
                mgjzc = string.IsNullOrEmpty(mgjzc) ? "0" : mgjzc;
                mgsy = data["每股收益"].Replace("元", "");
                mgsy = string.IsNullOrEmpty(mgsy) ? "0" : mgsy;
                mgxjlh = data["每股现金含量"].Replace("元", "");
                mgxjlh = string.IsNullOrEmpty(mgxjlh) ? "0" : mgxjlh;
                zyywsr = data["主营业务收入"].Replace("万元", "");
                zyywsr = string.IsNullOrEmpty(zyywsr) ? "0" : zyywsr;
                jlr = data["净利润"].Replace("万元", "");
                jlr = string.IsNullOrEmpty(jlr) ? "0" : jlr;
                jzrq = data["截止日期"];

                cw.mgjzc = Convert.ToDouble(mgjzc);
                cw.mgsy = Convert.ToDouble(mgsy);

                mgxjhl_str = mgxjlh;
                if (!data.Keys.Contains("每股现金含量"))
                {
                    mgxjhl_str = "0";
                }
                
                cw.mgxjhl = Convert.ToDouble(mgxjhl_str.Replace("元", ""));
                //cw.mgzbgjj = Convert.ToDouble(data["每股资本公积金"].Replace("元", ""));
                cw.zyywsr = Convert.ToDouble(zyywsr);
                cw.jll = Convert.ToDouble(jlr);
                cw.date = Convert.ToDateTime(jzrq);
            }

            return cw;
        }

        /**
         * 股票投资日历
         */       
        public static bool savetGpTZRL(WebBrowser web)
        {
            HtmlElement tableElement = web.Document.GetElementById("tzrlTb");

            if (tableElement == null)
            {
                return false;
            }

            //GPUtil.helper.ExecuteNonQuery("DELETE FROM dzrl WHERE rl < (SELECT MAX(DATE) FROM gpsinahis WHERE (CODE = 'sh000001'))");
            GPUtil.helper.ExecuteNonQuery("DELETE FROM dzrl;");

            Dzrl rec = null;
            string rlDate = "";
            foreach (HtmlElement item in tableElement.GetElementsByTagName("Tr"))
            {
                if (item == null)
                {
                    continue;
                }
              
                foreach (HtmlElement tditem in item.GetElementsByTagName("Td"))
                {                   
                    if (tditem != null && "first".Equals(tditem.GetAttribute("className")))
                    {
                        Console.WriteLine("aaa:"+tditem.InnerText);
                        rlDate = tditem.InnerText.Trim();                       
                    }                   
                }
              
                Console.WriteLine(item.InnerText);
                rec = new Dzrl();
                foreach (string key in KeyWordAPI.keyData.Keys)
                {
                    if (item.InnerText.IndexOf(key) != -1)
                    {
                        rec.keystr = rec.keystr + key + ",";
                        rec.gn = Convert.ToInt32(KeyWordAPI.keyData[key]);

                    }
                }

                foreach (string key in KeyWordAPI.gpNameList)
                {
                    if (item.InnerText.IndexOf(key) != -1)
                    {
                        rec.gpstr = key;

                        GPUtil.helper.ExecuteNonQuery("insert into Dzrl (gn,rl,keystr,gpstr,updtime) values ("+rec.gn+",'"
                        + rlDate + "','" + rec.keystr
                        + "','" + rec.gpstr + "','" + DateTime.Now + "')");
                    }
                }
            }            

            return true;
        }

        /**
     * 股票复盘
     **/
        public static bool savetGPFP(WebBrowser web)
        {
            //清除老数据
            GPUtil.helper.ExecuteNonQuery("DELETE FROM Gpfp");
            Console.WriteLine("DELETE FROM Gpfp");

            foreach (HtmlElement item in web.Document.GetElementsByTagName("Table"))
            {
                if (item == null)
                {
                    continue;
                }

                Dzfp fp = null;
                //表
                if ("mod_table".Equals(item.GetAttribute("className")))
                {
                    foreach (HtmlElement tritem in item.GetElementsByTagName("Tr"))
                    {

                        HtmlElementCollection items = tritem.GetElementsByTagName("Td");

                        if (items == null || items.Count == 0)
                        {
                            continue;
                        }

                        fp = new Dzfp();
                        fp.eventType = items[0].InnerText;
                        fp.gpname = items[2].InnerText;
                        fp.zf = Convert.ToDouble(items[4].InnerText);
                        fp.updtime = DateTime.Now;
                        fp.remark = items[6].InnerText;

                        GPUtil.helper.ExecuteNonQuery("insert into Gpfp (eventtype,gpname,zf,remark,updtime) values ('"
                           + fp.eventType + "','" + fp.gpname
                           + "','" + fp.zf + "','" + fp.remark + "','" + DateTime.Now + "')");

                    }

                }

            }

            return true;
        }

    }
}
