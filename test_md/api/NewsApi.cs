using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace MdTZ
{
    class NewsApi
    {

        public static void clearYwData()
        {
            GPUtil.helper.ExecuteNonQuery("delete from newsyw where newsfrom in ('今日要闻','概念资金','行业资金')", GPUtil.parms);
        }

        public static void clearData()
        {
            //GPUtil.helper.ExecuteNonQuery("DELETE FROM dzrl WHERE rl < (SELECT MAX(DATE) FROM gpsinahis WHERE (CODE = 'sh000001'))");
            GPUtil.helper.ExecuteNonQuery("delete from longhu",GPUtil.parms);
            GPUtil.helper.ExecuteNonQuery("delete from newsyw", GPUtil.parms);
        }

        public static void clearGGData()
        {

            GPUtil.helper.ExecuteNonQuery("delete from newsyw where newsfrom='个股公告'", GPUtil.parms);
        }

        public static void afterLoadProcess()
        {

            Console.WriteLine("##########afterLoadProcess doing");
            //交易后处理
            GPUtil.helper.ExecuteProcNoOut("call xw_process();");
            GPUtil.helper.ExecuteProcNoOut("call upd_qz();");

            //选择自选股           
            GPUtil.helper.ExecuteProcNoOut("CALL add_gpsel(); ");
            GPUtil.helper.ExecuteProcNoOut("CALL get_all_gn_codes();");

            //刷新趋势值
            QuShi.qsz = QuShi.getQuShiFS();

            //###行业资金##"
            RealDataApi.updGnZjSort();
            RealDataApi.updHyZjSort();

            Console.WriteLine("#############afterLoadProcess end");
          
        }

        public static bool isInsert(string key,string newsfrom)
        {
            DataRow row = null;
            if (String.IsNullOrEmpty(newsfrom))
            {
                row =  GPUtil.helper.ExecuteDataRow("select id from newsyw where keystr='" + key + "'", GPUtil.parms);
            } else
            {
                row = GPUtil.helper.ExecuteDataRow("select id from newsyw where keystr='" + key + "' and newsfrom='"+ newsfrom + "'"
                    , GPUtil.parms);
            }                
                
            if (row == null)
            {
                return false;
            } else
            {
                return true;
            }
        }

        /**
          *今日头条-同花顺
          * http://www.10jqka.com.cn/
        **/
        public static bool saveTHS_JRDT(WebBrowser web)
        {

            NewsYw yw = null;
            String htmlText = "";
            List<String> allKeys = new List<string>();
            allKeys.AddRange(KeyWordAPI.keyData.Keys);
            allKeys.AddRange(KeyWordAPI.gpNameList);

            int gn = 0;
            Object gnObj = null;
            foreach (HtmlElement item in web.Document.GetElementsByTagName("Div"))
            {
                if (item == null)
                {
                    continue;
                }
                //表
                if ("jrtt ta-parent-box".Equals(item.GetAttribute("className"))
                    || "item cjyw ta-parent-box".Equals(item.GetAttribute("className")))
                {
                    htmlText = item.InnerText.Trim().Replace("\r\n", "");                    
                    Console.WriteLine(htmlText);
                    foreach (string key in allKeys)
                    {
                        if (htmlText.IndexOf(key) != -1)
                        {
                            yw = new NewsYw();
                           
                            if ("jrtt ta-parent-box".Equals(item.GetAttribute("className")))
                            {
                                yw.from = "今日要闻";
                            }
                            else if ("item cjyw ta-parent-box".Equals(item.GetAttribute("className")))
                            {
                                yw.from = "今日要闻";
                            }
                            else if ("left yahei".Equals(item.GetAttribute("className"))
                                || "box module ta-parent-box".Equals(item.GetAttribute("className"))
                                || "box ta-parent-box".Equals(item.GetAttribute("className")))
                            {
                                yw.from = "今日要闻";
                            }

                            //yw.from = "今日要闻-同花顺";
                            yw.updtime = DateTime.Now;
                            yw.flag = 0;
                            yw.keystr = key;

                            if (KeyWordAPI.keyData.Keys.Contains(key))
                            {
                                gnObj = KeyWordAPI.keyData[key];
                                gn = Convert.ToInt32(gnObj);
                            }
                            else
                            {
                                gn = 0;
                            }
                            Console.WriteLine(yw.keystr);
                            if (!String.IsNullOrEmpty(yw.keystr))
                            {
                                if (!isInsert(yw.keystr, yw.from))
                                {
                                    GPUtil.helper.ExecuteNonQuery("insert into Newsyw (flag,gn,newsfrom,keystr,updtime) values ("
                                           + yw.flag + "," + gn + ",'" + yw.from + "','" + yw.keystr
                                           + "','" + DateTime.Now + "')");
                                }

                            }
                        }

                    }

                }

            }

            return true;
        }

        /**
        * 首页-同花顺
        * http://stock.10jqka.com.cn/
        **/
        public static bool saveTHS_SY(WebBrowser web)
        {           
            NewsYw yw = null;
            String htmlText = "";
            List<String> allKeys = new List<string>();
            allKeys.AddRange(KeyWordAPI.keyData.Keys);
            allKeys.AddRange(KeyWordAPI.gpNameList);

            int gn = 0;
            Object gnObj = null;
            foreach (HtmlElement item in web.Document.GetElementsByTagName("Div"))
            {
                if (item == null)
                {
                    continue;
                }
                //表
                if ("secnews-title".Equals(item.GetAttribute("className"))
                    || "ui-box noborder bondnews".Equals(item.GetAttribute("className"))                                     
                    || "ui-box noborder pb-m daytip".Equals(item.GetAttribute("className"))                   
                    || "ui-box noborder pb-m".Equals(item.GetAttribute("className"))
                    || "sub-box fl".Equals(item.GetAttribute("className"))
                    )
                {
                    htmlText = item.InnerText.Trim().Replace("\r\n", "");
                    Console.WriteLine(htmlText);

                    foreach (string key in allKeys)
                    {
                        if (htmlText.IndexOf(key) != -1)
                        {
                            yw = new NewsYw();
                            yw.from = "今日要闻";
                            yw.updtime = DateTime.Now;
                            yw.flag = 0;
                            yw.keystr = key;

                            if (KeyWordAPI.keyData.Keys.Contains(key))
                            {
                                gnObj = KeyWordAPI.keyData[key];
                                gn = Convert.ToInt32(gnObj);
                            }
                            else
                            {
                                gn = 0;
                            }


                            Console.WriteLine(yw.keystr);

                            if (!String.IsNullOrEmpty(yw.keystr))
                            {
                                if (!isInsert(yw.keystr, yw.from))
                                {
                                    GPUtil.helper.ExecuteNonQuery("insert into Newsyw (flag,gn,newsfrom,keystr,updtime) values ("
                                           + yw.flag + "," + gn + ",'" + yw.from + "','" + yw.keystr
                                           + "','" + DateTime.Now + "')");
                                }
                               
                            }
                        }

                    }

                }

            }

            return true;
        }

        /**
        * 早盘必读-同花顺
        * http://stock.10jqka.com.cn/zaopan/
        **/
        public static bool saveZPBD(WebBrowser web)
        {            
            NewsYw yw = null;
            String htmlText = "";
            List<String> allKeys = new List<string>();
            allKeys.AddRange(KeyWordAPI.keyData.Keys);
            allKeys.AddRange(KeyWordAPI.gpNameList);

            int gn = 0;
            Object gnObj = null;
            foreach (HtmlElement item in web.Document.GetElementsByTagName("Div"))
            {
                if (item == null)
                {
                    continue;
                }
                //表
                if ("content-main-fl fl".Equals(item.GetAttribute("className")))
                {
                    htmlText = item.InnerText.Trim().Replace("\r\n", "");
                    Console.WriteLine(htmlText);

                    foreach (string key in allKeys)
                    {
                        if (htmlText.IndexOf(key) != -1)
                        {
                            yw = new NewsYw();
                            yw.from = "早盘必读";
                            yw.updtime = DateTime.Now;
                            yw.flag = 0;
                            yw.keystr = key;

                            if (KeyWordAPI.keyData.Keys.Contains(key))
                            {
                                gnObj = KeyWordAPI.keyData[key];
                                gn = Convert.ToInt32(gnObj);
                            }
                            else
                            {
                                gn = 0;
                            }


                            Console.WriteLine(yw.keystr);

                            if (!String.IsNullOrEmpty(yw.keystr))
                            {
                                if (!isInsert(yw.keystr, yw.from))
                                {
                                    GPUtil.helper.ExecuteNonQuery("insert into Newsyw (flag,gn,newsfrom,keystr,updtime) values ("
                                   + yw.flag + "," + gn + ",'" + yw.from + "','" + yw.keystr
                                   + "','" + DateTime.Now + "')");
                                }
                                   
                            }
                        }

                       
                    }


                }



            }

            return true;
        }

        /**
       * 机会情报-同花顺
       * http://yuanchuang.10jqka.com.cn/qingbao/
       **/
        public static bool saveJHQB(WebBrowser web)
        {


            NewsYw yw = null;

            List<String> allKeys = new List<string>();
            allKeys.AddRange(KeyWordAPI.keyData.Keys);
            allKeys.AddRange(KeyWordAPI.gpNameList);

            int gn = 0;
            Object gnObj = null;
            DateTime qbdate = DateTime.Now;
            String htmlText = "";
            foreach (HtmlElement item in web.Document.GetElementsByTagName("Div"))
            {
                if (item == null)
                {
                    continue;
                }
                //表
                if ("fl date".Equals(item.GetAttribute("className")))
                {
                    htmlText = item.InnerText.Trim().Replace("\r\n", "");
                    string[] tmps = htmlText.Split("|".ToCharArray());
                    qbdate = Convert.ToDateTime(tmps[1]);

                    htmlText = item.Parent.Parent.InnerText.Trim().Replace("\r\n", "");

                    foreach (string key in allKeys)
                    {
                        if (htmlText.IndexOf(key) != -1)
                        {
                            yw = new NewsYw();
                            yw.from = "机会情报";
                            yw.updtime = qbdate;
                            yw.flag = 0;
                            yw.keystr = key;

                            if (KeyWordAPI.keyData.Keys.Contains(key))
                            {
                                gnObj = KeyWordAPI.keyData[key];
                                gn = Convert.ToInt32(gnObj);
                            }
                            else
                            {
                                gn = 0;
                            }

                            Console.WriteLine(yw.keystr);

                            if (!String.IsNullOrEmpty(yw.keystr))
                            {
                                if (!isInsert(yw.keystr, yw.from))
                                {
                                    GPUtil.helper.ExecuteNonQuery("insert into Newsyw (flag,gn,newsfrom,keystr,updtime) values ("
                                    + yw.flag + "," + gn + ",'" + yw.from + "','" + yw.keystr
                                    + "','" + yw.updtime + "')");
                                }
                                    
                            }
                        }

       
                    }


                }



            }

            return true;
        }

        /**
      * 异动观察-同花顺
      * http://yuanchuang.10jqka.com.cn/zhangting/
      **/
        public static bool saveYDGC(WebBrowser web)
        {

            NewsYw yw = null;

            List<String> allKeys = new List<string>();
            //allKeys.AddRange(KeyWordAPI.keyData.Keys);
            allKeys.AddRange(KeyWordAPI.gpNameList);

            int gn = 0;
            Object gnObj = null;
            DateTime qbdate = DateTime.Now;
            String htmlText = "";
            foreach (HtmlElement item in web.Document.GetElementsByTagName("Div"))
            {
                if (item == null)
                {
                    continue;
                }
                //表
                if ("fl date".Equals(item.GetAttribute("className")))
                {
                    htmlText = item.InnerText.Trim().Replace("\r\n", "");
                    string[] tmps = htmlText.Split("|".ToCharArray());
                    qbdate = Convert.ToDateTime(tmps[1]);

                    htmlText = item.Parent.Parent.InnerText.Trim().Replace("\r\n", "");

                    foreach (string key in allKeys)
                    {
                        if (htmlText.IndexOf(key) != -1)
                        {
                            yw = new NewsYw();
                            yw.from = "异动观察";
                            yw.updtime = qbdate;
                            yw.flag = 0;
                            yw.keystr = key;

                            if (KeyWordAPI.keyData.Keys.Contains(key))
                            {
                                gnObj = KeyWordAPI.keyData[key];
                                gn = Convert.ToInt32(gnObj);
                            }
                            else
                            {
                                gn = 0;
                            }


                            Console.WriteLine(yw.keystr);

                            if (!String.IsNullOrEmpty(yw.keystr))
                            {

                                if (!isInsert(yw.keystr, yw.from))
                                {
                                    GPUtil.helper.ExecuteNonQuery("insert into Newsyw (flag,gn,newsfrom,keystr,updtime) values ("
                                          + yw.flag + "," + gn + ",'" + yw.from + "','" + yw.keystr
                                          + "','" + yw.updtime + "')");
                                }
                                  
                            }
                        }

           
                    }


                }



            }

            return true;
        }
       
        /**
        * 今日要闻-同花顺
        * http://news.10jqka.com.cn/yaowen/
        **/
        public static bool saveJRYW(WebBrowser web)
        {


            NewsYw yw = null;

            List<String> allKeys = new List<string>();
            allKeys.AddRange(KeyWordAPI.keyData.Keys);
            allKeys.AddRange(KeyWordAPI.gpNameList);

            int gn = 0;
            Object gnObj = null;
            DateTime qbdate = DateTime.Now;
            String htmlText = "";
            foreach (HtmlElement item in web.Document.GetElementsByTagName("Div"))
            {
                if (item == null)
                {
                    continue;
                }
                //表
                if ("block-wrap auto-news".Equals(item.GetAttribute("className"))
                    || "box today-focus-text fl".Equals(item.GetAttribute("className")))
                {
                    htmlText = item.InnerText.Trim().Replace("\r\n", "");
                    Console.WriteLine(htmlText);
                    foreach (string key in allKeys)
                    {
                        if (htmlText.IndexOf(key) != -1)
                        {
                            yw = new NewsYw();
                            yw.from = "今日要闻";
                            yw.updtime = DateTime.Now;
                            yw.flag = 0;
                            yw.keystr = key;
                            if (KeyWordAPI.keyData.Keys.Contains(key))
                            {
                                gnObj = KeyWordAPI.keyData[key];
                                gn = Convert.ToInt32(gnObj);
                            }
                            else
                            {
                                gn = 0;
                            }


                            Console.WriteLine(yw.keystr);

                            if (!String.IsNullOrEmpty(yw.keystr))
                            {
                                if (!isInsert(yw.keystr, yw.from))
                                {
                                    GPUtil.helper.ExecuteNonQuery("insert into Newsyw (flag,gn,newsfrom,keystr,updtime) values ("
                                       + yw.flag + "," + gn + ",'" + yw.from + "','" + yw.keystr
                                       + "','" + yw.updtime + "')");
                                }
                                   
                            }
                        }

   
                    }


                }



            }

            return true;
        }

        /**
        * 财经新闻清单-同花顺
        * http://news.10jqka.com.cn/today_list/
        **/
        public static bool saveCJXW_LIST(WebBrowser web)
        {

            NewsYw yw = null;

            List<String> allKeys = new List<string>();
            allKeys.AddRange(KeyWordAPI.keyData.Keys);       

            int gn = 0;
            Object gnObj = null;
            DateTime qbdate = DateTime.Now;
            String htmlText = "";
            foreach (HtmlElement item in web.Document.GetElementsByTagName("Div"))
            {
                if (item == null)
                {
                    continue;
                }
                //表
                if ("list_area".Equals(item.GetAttribute("className")))
                {
                    htmlText = item.InnerText.Trim().Replace("\r\n", "");                                    

                    foreach (string key in allKeys)
                    {
                        if (htmlText.IndexOf(key) != -1)
                        {
                            yw = new NewsYw();
                            yw.from = "财经要闻";
                            yw.updtime = qbdate;
                            yw.flag = 0;
                            yw.keystr = key;

                            if (KeyWordAPI.keyData.Keys.Contains(key))
                            {
                                gnObj = KeyWordAPI.keyData[key];
                                gn = Convert.ToInt32(gnObj);
                            }
                            else
                            {
                                gn = 0;
                            }

                            Console.WriteLine(yw.keystr);

                            if (!String.IsNullOrEmpty(yw.keystr))
                            {
                                if (!isInsert(yw.keystr, yw.from))
                                {
                                    GPUtil.helper.ExecuteNonQuery("insert into Newsyw (flag,gn,newsfrom,keystr,updtime) values ("
                                       + yw.flag + "," + gn + ",'" + yw.from + "','" + yw.keystr
                                       + "','" + yw.updtime + "')");
                                }
                                   
                            }
                        }
                        
                    }


                }



            }

            return true;

        }

       /**
           * 业绩预增-同花顺
           * http://data.10jqka.com.cn/financial/yjyg/
       **/
        public static bool saveYJYZ_GG(WebBrowser web)
        {

            NewsYw yw = null;

            string type = "业绩预增";
            string url = web.Url.ToString();           

            int gn = 0;
            DateTime qbdate = DateTime.Now;
            String htmlText = "";
            HtmlElementCollection trEles = null;

            HtmlElementCollection tdEles1 = null;
            foreach (HtmlElement item in web.Document.GetElementsByTagName("Table"))
            {
                if (item == null)
                {
                    continue;
                }
                //表
                if ("m-table J-ajax-table".Equals(item.GetAttribute("className")))
                {
                    trEles = item.Document.GetElementsByTagName("Tr");
                    int i = 0;
                    foreach (HtmlElement tritem in trEles)
                    {
                        i++;
                        if (i == 1)
                        {
                            continue;
                        }
                        htmlText = tritem.InnerText.Trim().Replace("\r\n", "");

                        string yg_type = "";
                        tdEles1 = tritem.GetElementsByTagName("Td");

                        if (tdEles1 == null || tdEles1.Count <= 0)
                        {
                            continue;
                        }

                        qbdate = Convert.ToDateTime(tdEles1[7].InnerText.Trim());
                        if (tdEles1[3] != null)
                        {
                            yg_type = tdEles1[3].InnerText;
                        }

                        Console.WriteLine("yg_type:" + yg_type);

                        yw = new NewsYw();
                        yw.from = type;
                        yw.updtime = qbdate;
                        yw.flag = 0;
                        yw.remark = yg_type;
                        yw.keystr = tdEles1[2].InnerText;
                        Console.WriteLine(yw.keystr);

                        if (!String.IsNullOrEmpty(yw.keystr))
                        {
                            if (!isInsert(yw.keystr, yw.from))
                            {
                                GPUtil.helper.ExecuteNonQuery("insert into Newsyw (flag,gn,newsfrom,keystr,remark,updtime) values ("
                                  + yw.flag + "," + gn + ",'" + yw.from + "','" + yw.keystr
                                  + "','" + yw.remark + "','" + yw.updtime + "')");
                            }                               

                        }
                    }
                }


            }

            return true;            

        }

        /**
          * 大众交易-同花顺
          * http://data.10jqka.com.cn/market/dzjy/
        **/
        public static bool save_DZJY(WebBrowser web)
        {

            NewsYw yw = null;

            string type = "大众交易";
            string url = web.Url.ToString();

            int gn = 0;
            DateTime qbdate = DateTime.Now;
            String htmlText = "";
            HtmlElementCollection trEles = null;
            HtmlElementCollection tdEles1 = null;
            foreach (HtmlElement item in web.Document.GetElementsByTagName("Table"))
            {
                if (item == null)
                {
                    continue;
                }
                //表
                if ("m-table J-ajax-table".Equals(item.GetAttribute("className")))
                {
                    trEles = item.Document.GetElementsByTagName("Tr");
                    int i = 0;
                    foreach (HtmlElement tritem in trEles)
                    {
                        i++;
                        if (i == 1)
                        {
                            continue;
                        }
                        htmlText = tritem.InnerText.Trim().Replace("\r\n", "");

                        string yg_type = "";
                        tdEles1 = tritem.GetElementsByTagName("Td");

                        if (tdEles1 == null || tdEles1.Count <= 0)
                        {
                            continue;
                        }

                        qbdate = Convert.ToDateTime(tdEles1[1].InnerText.Trim());
                        if (tdEles1[6] != null)
                        {
                            yg_type = tdEles1[6].InnerText +"||" + tdEles1[7].InnerText;    
                        }

                        Console.WriteLine("yg_type:" + yg_type);

                        yw = new NewsYw();
                        yw.from = type;
                        yw.updtime = qbdate;
                        yw.flag = 0;
                        yw.remark = yg_type;
                        yw.keystr = tdEles1[3].InnerText;
                        Console.WriteLine(yw.keystr);

                        if (!String.IsNullOrEmpty(yw.keystr))
                        {
                            if (!isInsert(yw.keystr, yw.from))
                            {
                                GPUtil.helper.ExecuteNonQuery("insert into Newsyw (flag,gn,newsfrom,keystr,remark,updtime) values ("
                                  + yw.flag + "," + gn + ",'" + yw.from + "','" + yw.keystr
                                  + "','" + yw.remark + "','" + yw.updtime + "')");
                            }

                        }
                    }
                }


            }

            return true;

        }

        /**
           * 个股公告-同花顺
           * http://data.10jqka.com.cn/market/ggsd/
        **/
        public static bool save_GGSD(WebBrowser web)
        {

            NewsYw yw = null;

            string type = "个股公告";
            int gn = 0;
            string url = web.Url.ToString();
            if (url.IndexOf("利好公告") != -1)
            {
                gn = 1;
            }
            else if (url.IndexOf("利空公告") != -1)
            {
                gn = -1;
            }
            else if (url.IndexOf("最新公告") != -1)
            {
                gn = 0;
            }
           
            DateTime qbdate = DateTime.Now;
            String htmlText = "";
            HtmlElementCollection trEles = null;

            HtmlElementCollection tdEles1 = null;
            foreach (HtmlElement item in web.Document.GetElementsByTagName("Table"))
            {
                if (item == null)
                {
                    continue;
                }
                //表
                if ("m-table J-ajax-table".Equals(item.GetAttribute("className")))
                {
                    trEles = item.Document.GetElementsByTagName("Tr");
                    int i = 0;
                    foreach (HtmlElement tritem in trEles)
                    {
                        i++;
                        if (i == 1)
                        {
                            continue;
                        }
                        htmlText = tritem.InnerText.Trim().Replace("\r\n", "");

                        string yg_type = "";
                        tdEles1 = tritem.GetElementsByTagName("Td");

                        if (tdEles1 == null || tdEles1.Count <= 0)
                        {
                            continue;
                        }

                        qbdate = Convert.ToDateTime(tdEles1[1].InnerText.Trim());
                        if (tdEles1[8] != null)
                        {
                            yg_type = tdEles1[8].InnerText+"||"+ tdEles1[5].InnerText;
                        }

                        Console.WriteLine("yg_type:" + yg_type);

                        yw = new NewsYw();
                        yw.from = type;
                        yw.updtime = qbdate;
                        yw.flag = 0;                       
                        yw.remark = yg_type;
                        yw.remark1 = tdEles1[6].InnerText.Trim();
                        yw.keystr = tdEles1[3].InnerText;
                        Console.WriteLine(yw.keystr);

                        if (!String.IsNullOrEmpty(yw.keystr))
                        {
                            if (!isInsert(yw.keystr, yw.from))
                            {
                                GPUtil.helper.ExecuteNonQuery("insert into Newsyw (flag,gn,newsfrom,keystr,remark,remark1,updtime) values ("
                                  + yw.flag + "," + gn + ",'" + yw.from + "','" + yw.keystr
                                  + "','" + yw.remark + "','"+yw.remark1+"','" + yw.updtime + "')");
                            }

                        }
                    }
                }


            }

            return true;
        }

        /**
      * IPO-收益-同花顺
      * http://data.10jqka.com.cn/ipo/syg/
      **/
        public static bool saveIPO_SY_GG(WebBrowser web)
        {

            NewsYw yw = null;

            string type = "IPO收益";
            string url = web.Url.ToString();

            int gn = 0;
            DateTime qbdate = DateTime.Now;
            String htmlText = "";
            HtmlElementCollection trEles = null;

            HtmlElementCollection tdEles1 = null;
            foreach (HtmlElement item in web.Document.GetElementsByTagName("Table"))
            {
                if (item == null)
                {
                    continue;
                }
                //表
                if ("m-table J-ajax-table".Equals(item.GetAttribute("className")))
                {
                    trEles = item.Document.GetElementsByTagName("Tr");
                    int i = 0;
                    foreach (HtmlElement tritem in trEles)
                    {
                        i++;
                        if (i == 1)
                        {
                            continue;
                        }
                        htmlText = tritem.InnerText.Trim().Replace("\r\n", "");

                        string yg_type = "";
                        tdEles1 = tritem.GetElementsByTagName("Td");

                        if (tdEles1 == null || tdEles1.Count <= 0)
                        {
                            continue;
                        }

                      
                        if (tdEles1[7] != null)
                        {
                            yg_type = tdEles1[7].InnerText;
                        }

                        Console.WriteLine("yg_type:" + yg_type);

                        yw = new NewsYw();
                        yw.from = type;
                        yw.updtime = qbdate;
                        yw.flag = 0;
                        yw.remark = yg_type;
                        yw.keystr = tdEles1[2].InnerText;
                        Console.WriteLine(yw.keystr);

                        if (!String.IsNullOrEmpty(yw.keystr))
                        {
                            if (!isInsert(yw.keystr, yw.from))
                            {
                                GPUtil.helper.ExecuteNonQuery("insert into Newsyw (flag,gn,newsfrom,keystr,remark,updtime) values ("
                                  + yw.flag + "," + gn + ",'" + yw.from + "','" + yw.keystr
                                  + "','" + yw.remark + "','" + yw.updtime + "')");
                            }

                        }
                    }
                }


            }

            return true;

        }

        /**
       * 强势选股-同花顺
       * http://data.10jqka.com.cn/rank/cxfl/
       **/
        public static bool save_QSXG_THS(WebBrowser web)
        {

            NewsYw yw = null;      
            string type = "创新股";
            string url = web.Url.ToString();
            if (url.IndexOf("lxsz") != -1)
            {
                type = "连续上涨";
            }
            else if (url.IndexOf("cxfl") != -1)
            {
                type = "持续放量";
            }
            else if (url.IndexOf("xstp") != -1)
            {
                type = "向上突波";
            }
            else if (url.IndexOf("ljqs") != -1)
            {
                type = "量价齐升";
            }
            else if (url.IndexOf("cxg") != -1)
            {
                type = "创新高";
            }
            else if (url.IndexOf("cxd") != -1)
            {
                type = "创新低";
            }
            else if (url.IndexOf("lxxd") != -1)
            {
                type = "连续下跌";
            }
            else if (url.IndexOf("cxsl") != -1)
            {
                type = "持续缩量";
            }
            else if (url.IndexOf("xxtp") != -1)
            {
                type = "向下突波";
            }
            else if (url.IndexOf("ljqd") != -1)
            {
                type = "量较齐跌";
            }         

            int gn = 0;
            DateTime qbdate = DateTime.Now;
            String htmlText = "";
            HtmlElementCollection trEles = null;

            HtmlElementCollection tdEles1 = null;
            foreach (HtmlElement item in web.Document.GetElementsByTagName("Table"))
            {
                if (item == null)
                {
                    continue;
                }
                //表
                if ("m-table J-ajax-table".Equals(item.GetAttribute("className")))
                {
                    trEles = item.Document.GetElementsByTagName("Tr");
                    int i = 0;
                    foreach (HtmlElement tritem in trEles)
                    {
                        i++;
                        if (i == 1)
                        {
                            continue;
                        }
                        htmlText = tritem.InnerText.Trim().Replace("\r\n", "");

                        string yg_type = "";
                        tdEles1 = tritem.GetElementsByTagName("Td");

                        if (tdEles1 == null || tdEles1.Count <= 0)
                        {
                            continue;
                        }

                     
                     
                        if (url.IndexOf("lxsz") != -1)
                        {
                            yg_type = tdEles1[6].InnerText.Trim();
                        }
                        else if (url.IndexOf("cxfl") != -1)
                        {
                            yg_type = tdEles1[7].InnerText.Trim();
                        }
                        else if (url.IndexOf("xstp") != -1)
                        {
                            yg_type = tdEles1[7].InnerText.Trim();
                        }
                        else if (url.IndexOf("ljqs") != -1)
                        {
                            yg_type = tdEles1[4].InnerText.Trim();
                        }
                        else if (url.IndexOf("cxg") != -1)
                        {
                            yg_type = tdEles1[4].InnerText.Trim();
                        }
                        else if (url.IndexOf("cxd") != -1)
                        {
                            yg_type = tdEles1[4].InnerText.Trim();
                        }
                        else if (url.IndexOf("lxxd") != -1)
                        {
                            yg_type = tdEles1[6].InnerText.Trim();
                        }
                        else if (url.IndexOf("cxsl") != -1)
                        {
                            yg_type = tdEles1[7].InnerText.Trim();
                        }
                        else if (url.IndexOf("xxtp") != -1)
                        {
                            yg_type = tdEles1[7].InnerText.Trim();
                        }
                        else if (url.IndexOf("ljqd") != -1)
                        {
                            yg_type = tdEles1[4].InnerText.Trim();
                        }                      
                        Console.WriteLine("yg_type:" + yg_type);

                        yw = new NewsYw();
                        yw.from = type;
                        yw.updtime = qbdate;
                        yw.flag = 0;
                        yw.remark = yg_type;
                        yw.keystr = tdEles1[2].InnerText;
                        Console.WriteLine(yw.keystr);

                        if (!String.IsNullOrEmpty(yw.keystr))
                        {
                            if (!isInsert(yw.keystr, yw.from))
                            {
                                GPUtil.helper.ExecuteNonQuery("insert into Newsyw (flag,gn,newsfrom,keystr,remark,updtime) values ("
                                  + yw.flag + "," + gn + ",'" + yw.from + "','" + yw.keystr
                                  + "','" + yw.remark + "','" + yw.updtime + "')");
                            }

                        }
                    }
                }


            }

            return true;

           
        }

        /**
           * 送股派息-同花顺
           * http://data.10jqka.com.cn/financial/sgpx/
        **/
        public static bool saveSGPX_GG(WebBrowser web)
        {

            NewsYw yw = null;

            string type = "送股派息";
            string url = web.Url.ToString();

            int gn = 0;
            DateTime qbdate = DateTime.Now;
            String htmlText = "";
            HtmlElementCollection trEles = null;

            HtmlElementCollection tdEles1 = null;
            foreach (HtmlElement item in web.Document.GetElementsByTagName("Table"))
            {
                if (item == null)
                {
                    continue;
                }
                //表
                if ("m-table J-ajax-table".Equals(item.GetAttribute("className")))
                {
                    trEles = item.Document.GetElementsByTagName("Tr");
                    int i = 0;
                    foreach (HtmlElement tritem in trEles)
                    {
                        i++;
                        if (i == 1)
                        {
                            continue;
                        }
                        htmlText = tritem.InnerText.Trim().Replace("\r\n", "");

                        string yg_type = "";
                        tdEles1 = tritem.GetElementsByTagName("Td");

                        if (tdEles1 == null || tdEles1.Count <= 0)
                        {
                            continue;
                        }

                        qbdate = Convert.ToDateTime(tdEles1[10].InnerText.Trim());
                        if (tdEles1[9] != null)
                        {
                            yg_type = tdEles1[9].InnerText;
                        }

                        Console.WriteLine("yg_type:" + yg_type);

                        yw = new NewsYw();
                        yw.from = type;
                        yw.updtime = qbdate;
                        yw.flag = 0;
                        yw.remark = yg_type;
                        yw.keystr = tdEles1[2].InnerText;
                        Console.WriteLine(yw.keystr);

                        if (!String.IsNullOrEmpty(yw.keystr))
                        {
                            if (!isInsert(yw.keystr, yw.from))
                            {
                                GPUtil.helper.ExecuteNonQuery("insert into Newsyw (flag,gn,newsfrom,keystr,remark,updtime) values ("
                                  + yw.flag + "," + gn + ",'" + yw.from + "','" + yw.keystr
                                  + "','" + yw.remark + "','" + yw.updtime + "')");
                            }

                        }
                    }
                }


            }

            return true;

        }

        /**
        * 高管持股-同花顺
        *http://data.10jqka.com.cn/financial/ggjy/
        **/
        public static bool save_GGCG(WebBrowser web)
        {

            NewsYw yw = null;

            string type = "高管持股";
            string url = web.Url.ToString();

            int gn = 0;
            DateTime qbdate = DateTime.Now;
            String htmlText = "";
            HtmlElementCollection trEles = null;

            HtmlElementCollection tdEles1 = null;
            foreach (HtmlElement item in web.Document.GetElementsByTagName("Table"))
            {
                if (item == null)
                {
                    continue;
                }
                //表
                if ("m-table J-ajax-table".Equals(item.GetAttribute("className")))
                {
                    trEles = item.Document.GetElementsByTagName("Tr");
                    int i = 0;
                    foreach (HtmlElement tritem in trEles)
                    {
                        i++;
                        if (i == 1)
                        {
                            continue;
                        }
                        htmlText = tritem.InnerText.Trim().Replace("\r\n", "");

                        string yg_type = "";
                        tdEles1 = tritem.GetElementsByTagName("Td");

                        if (tdEles1 == null || tdEles1.Count <= 0)
                        {
                            continue;
                        }

                        qbdate = Convert.ToDateTime(tdEles1[4].InnerText.Trim());
                        if (tdEles1[8] != null)
                        {
                            yg_type = tdEles1[8].InnerText;
                        }

                        Console.WriteLine("yg_type:" + yg_type);

                        yw = new NewsYw();
                        yw.from = type;
                        yw.updtime = qbdate;
                        yw.flag = 0;
                        yw.remark = yg_type;
                        yw.keystr = tdEles1[2].InnerText;
                        Console.WriteLine(yw.keystr);

                        if (!String.IsNullOrEmpty(yw.keystr))
                        {
                            if (!isInsert(yw.keystr, yw.from))
                            {
                                GPUtil.helper.ExecuteNonQuery("insert into Newsyw (flag,gn,newsfrom,keystr,remark,updtime) values ("
                                  + yw.flag + "," + gn + ",'" + yw.from + "','" + yw.keystr
                                  + "','" + yw.remark + "','" + yw.updtime + "')");
                            }

                        }
                    }
                }


            }

            return true;

        }

        /**
          * 龙虎榜-同花顺
          *http://data.10jqka.com.cn/financial/longhu/
          **/
        public static bool save_LONGHU(WebBrowser web)
        {



            Longhu lh = null;

            string type = "龙虎榜";
            string url = web.Url.ToString();
        
            DateTime qbdate = DateTime.Now;
            String htmlText = "";
            HtmlElementCollection trEles = null;
            HtmlElementCollection tdEles1 = null;
            HtmlElement item = null;
            item = web.Document.GetElementById("maintable");
            trEles = item.Document.GetElementsByTagName("Tr");
            int i = 0;
            foreach (HtmlElement tritem in trEles)
            {
                i++;
                if (i == 1)
                {
                    continue;
                }
                htmlText = tritem.InnerText.Trim().Replace("\r\n", "");

               
                tdEles1 = tritem.GetElementsByTagName("Td");

                if (tdEles1 == null || tdEles1.Count <= 0)
                {
                    continue;
                }

                Console.WriteLine(htmlText);
                if (tdEles1.Count <= 10)
                {
                    continue;
                }
                
                lh = new Longhu();
                lh.code = tdEles1[1].InnerText;
                lh.date = qbdate;
                lh.name = tdEles1[2].InnerText;
                lh.zjjd = tdEles1[3].InnerText;
                lh.zdf = tdEles1[5].InnerText;
                lh.type = tdEles1[10].InnerText;
                Console.WriteLine(lh.name);

                GPUtil.helper.ExecuteNonQuery("insert into longhu (code,name,zjjd,zdf,jmre,type,date) values ('"
                      + lh.code + "','" + lh.name + "','" + lh.zjjd + "','" + lh.zdf
                      + "'," + lh.jmre + ",'" + lh.type + "','" + lh.date + "')");
            }

            return true;

        }

        /**
           * 保险资举牌-同花顺
           *http://data.10jqka.com.cn/financial/xzjp/
       **/
        public static bool save_XZJP(WebBrowser web)
        {

            NewsYw yw = null;

            string type = "险资举牌";
            string url = web.Url.ToString();

            int gn = 0;
            DateTime qbdate = DateTime.Now;
            String htmlText = "";
            HtmlElementCollection trEles = null;

            HtmlElementCollection tdEles1 = null;
            foreach (HtmlElement item in web.Document.GetElementsByTagName("Table"))
            {
                if (item == null)
                {
                    continue;
                }
                //表
                if ("m-table J-ajax-table".Equals(item.GetAttribute("className")))
                {
                    trEles = item.Document.GetElementsByTagName("Tr");
                    int i = 0;
                    foreach (HtmlElement tritem in trEles)
                    {
                        i++;
                        if (i == 1)
                        {
                            continue;
                        }
                        htmlText = tritem.InnerText.Trim().Replace("\r\n", "");

                        string yg_type = "";
                        tdEles1 = tritem.GetElementsByTagName("Td");

                        if (tdEles1 == null || tdEles1.Count <= 0)
                        {
                            continue;
                        }

                        qbdate = Convert.ToDateTime(tdEles1[1].InnerText.Trim());
                        if (tdEles1[7] != null)
                        {
                            yg_type = tdEles1[7].InnerText;
                        }

                        Console.WriteLine("yg_type:" + yg_type);

                        yw = new NewsYw();
                        yw.from = type;
                        yw.updtime = qbdate;
                        yw.flag = 0;
                        yw.remark = yg_type;
                        yw.keystr = tdEles1[3].InnerText;
                        Console.WriteLine(yw.keystr);

                        if (!String.IsNullOrEmpty(yw.keystr))
                        {
                            if (!isInsert(yw.keystr, yw.from))
                            {
                                GPUtil.helper.ExecuteNonQuery("insert into Newsyw (flag,gn,newsfrom,keystr,remark,updtime) values ("
                                  + yw.flag + "," + gn + ",'" + yw.from + "','" + yw.keystr
                                  + "','" + yw.remark + "','" + yw.updtime + "')");
                            }

                        }
                    }
                }


            }

            return true;

        }

        /**
           * 业绩快报-同花顺
           * http://data.10jqka.com.cn/financial/yjkb/
        **/
        public static bool save_YJKB_GG(WebBrowser web)
        {

            NewsYw yw = null;

            string type = "业绩快报";
            string url = web.Url.ToString();

            int gn = 0;
            DateTime qbdate = DateTime.Now;
            String htmlText = "";
            HtmlElementCollection trEles = null;

            HtmlElementCollection tdEles1 = null;
            foreach (HtmlElement item in web.Document.GetElementsByTagName("Table"))
            {
                if (item == null)
                {
                    continue;
                }
                //表
                if ("m-table J-ajax-table".Equals(item.GetAttribute("className")))
                {
                    trEles = item.Document.GetElementsByTagName("Tr");
                    int i = 0;
                    foreach (HtmlElement tritem in trEles)
                    {
                        i++;
                        if (i == 1)
                        {
                            continue;
                        }
                        htmlText = tritem.InnerText.Trim().Replace("\r\n", "");

                        string yg_type = "";
                        tdEles1 = tritem.GetElementsByTagName("Td");

                        if (tdEles1 == null || tdEles1.Count <= 0)
                        {
                            continue;
                        }

                        qbdate = Convert.ToDateTime(tdEles1[3].InnerText.Trim());
                        if (tdEles1[10] != null)
                        {
                            yg_type = tdEles1[10].InnerText;
                        }

                        Console.WriteLine("yg_type:" + yg_type);

                        yw = new NewsYw();
                        yw.from = type;
                        yw.updtime = qbdate;
                        yw.flag = 0;
                        yw.remark = yg_type;
                        yw.keystr = tdEles1[2].InnerText;
                        Console.WriteLine(yw.keystr);

                        if (!String.IsNullOrEmpty(yw.keystr))
                        {
                            if (!isInsert(yw.keystr, yw.from))
                            {
                                GPUtil.helper.ExecuteNonQuery("insert into Newsyw (flag,gn,newsfrom,keystr,remark,updtime) values ("
                                  + yw.flag + "," + gn + ",'" + yw.from + "','" + yw.keystr
                                  + "','" + yw.remark + "','" + yw.updtime + "')");
                            }

                        }
                    }
                }


            }

            return true;

        }

        /**
    * 业绩预披-同花顺
    * http://data.10jqka.com.cn/financial/yypl/
 **/
        public static bool save_YJYP(WebBrowser web)
        {

            NewsYw yw = null;

            string type = "业绩预披";
            string url = web.Url.ToString();

            int gn = 0;
            DateTime qbdate = DateTime.Now;
            String htmlText = "";
            HtmlElementCollection trEles = null;

            HtmlElementCollection tdEles1 = null;
            foreach (HtmlElement item in web.Document.GetElementsByTagName("Table"))
            {
                if (item == null)
                {
                    continue;
                }
                //表
                if ("m-table J-ajax-table".Equals(item.GetAttribute("className")))
                {
                    trEles = item.Document.GetElementsByTagName("Tr");
                    int i = 0;
                    foreach (HtmlElement tritem in trEles)
                    {
                        i++;
                        if (i == 1)
                        {
                            continue;
                        }
                        htmlText = tritem.InnerText.Trim().Replace("\r\n", "");

                        string yg_type = "";
                        tdEles1 = tritem.GetElementsByTagName("Td");

                        if (tdEles1 == null || tdEles1.Count <= 0)
                        {
                            continue;
                        }

                        qbdate = Convert.ToDateTime(tdEles1[3].InnerText.Trim());
                        //if (tdEles1[10] != null)
                        //{
                        //    yg_type = tdEles1[10].InnerText;
                        //}

                        //Console.WriteLine("yg_type:" + yg_type);

                        yw = new NewsYw();
                        yw.from = type;
                        yw.updtime = qbdate;
                        yw.flag = 0;
                        yw.remark = yg_type;
                        yw.keystr = tdEles1[2].InnerText;
                        Console.WriteLine(yw.keystr);

                        if (!String.IsNullOrEmpty(yw.keystr))
                        {
                            if (!isInsert(yw.keystr, yw.from))
                            {
                                GPUtil.helper.ExecuteNonQuery("insert into Newsyw (flag,gn,newsfrom,keystr,remark,updtime) values ("
                                  + yw.flag + "," + gn + ",'" + yw.from + "','" + yw.keystr
                                  + "','" + yw.remark + "','" + yw.updtime + "')");
                            }

                        }
                    }
                }


            }

            return true;

        }

        /**
       * 同花顺>数据中心>限售解禁
       * http://data.10jqka.com.cn/market/xsjj/
         **/
        public static bool saveXSJJ_GP(WebBrowser web)
        {
            NewsYw yw = null;

            string type = "限售解禁";
            string url = web.Url.ToString();

            int gn = 0;
            DateTime qbdate = DateTime.Now;
            String htmlText = "";
            HtmlElementCollection trEles = null;
            HtmlElementCollection tdEles1 = null;
            foreach (HtmlElement item in web.Document.GetElementsByTagName("Table"))
            {
                if (item == null)
                {
                    continue;
                }
                //表
                if ("m-table J-ajax-table".Equals(item.GetAttribute("className")))
                {
                    trEles = item.Document.GetElementsByTagName("Tr");
                    int i = 0;
                    foreach (HtmlElement tritem in trEles)
                    {
                        i++;
                        if (i == 1)
                        {
                            continue;
                        }
                        htmlText = tritem.InnerText.Trim().Replace("\r\n", "");

                        string yg_type = "";
                        tdEles1 = tritem.GetElementsByTagName("Td");
                        if (tdEles1 == null || tdEles1.Count <= 0)
                        {
                            continue;
                        }

                        qbdate = Convert.ToDateTime(tdEles1[3].InnerText.Trim());
                        if (tdEles1[7] != null)
                        {
                            yg_type = tdEles1[7].InnerText;
                        }

                        Console.WriteLine("yg_type:" + yg_type);

                        yw = new NewsYw();
                        yw.from = type;
                        yw.updtime = qbdate;
                        yw.flag = 0;
                        yw.remark = yg_type;
                        yw.keystr = tdEles1[2].InnerText;
                        Console.WriteLine(yw.keystr);

                        if (!String.IsNullOrEmpty(yw.keystr))
                        {
                            if (!isInsert(yw.keystr, yw.from))
                            {
                                GPUtil.helper.ExecuteNonQuery("insert into Newsyw (flag,gn,newsfrom,keystr,remark,updtime) values ("
                                  + yw.flag + "," + gn + ",'" + yw.from + "','" + yw.keystr
                                  + "','" + yw.remark + "','" + yw.updtime + "')");
                            }

                        }
                    }
                }


            }

            return true;

        }

        /**
         * 同花顺>数据中心>新股申购
         *http://data.10jqka.com.cn/ipo/xgsgyzq/
       **/
        public static bool saveXGSG_GP(WebBrowser web)
        {
            NewsYw yw = null;

            string type = "新股申购";
            string url = web.Url.ToString();

            int gn = 0;
            DateTime qbdate = DateTime.Now;
            String htmlText = "";
            HtmlElementCollection trEles = null;
            HtmlElementCollection tdEles1 = null;
            HtmlElement item = web.Document.GetElementById("maintable");
          
            if (item == null)
            {
                return false;
            }

            trEles = item.Document.GetElementsByTagName("Tr");
            int i = 0;
            foreach (HtmlElement tritem in trEles)
            {
                i++;
                if (i == 1)
                {
                    continue;
                }
                htmlText = tritem.InnerText.Trim().Replace("\r\n", "");

                string yg_type = "";
                tdEles1 = tritem.GetElementsByTagName("Td");
                if (tdEles1 == null || tdEles1.Count <= 0)
                {
                    continue;
                }

                if (tdEles1[9] != null)
                {
                    yg_type = tdEles1[9].InnerText;
                }

                Console.WriteLine("yg_type:" + yg_type);

                yw = new NewsYw();
                yw.from = type;
                yw.updtime = qbdate;
                yw.flag = 0;
                yw.remark = yg_type;
                yw.keystr = tdEles1[1].InnerText;
                Console.WriteLine(yw.keystr);

                if (!String.IsNullOrEmpty(yw.keystr))
                {
                    if (!isInsert(yw.keystr, yw.from))
                    {
                        GPUtil.helper.ExecuteNonQuery("insert into Newsyw (flag,gn,newsfrom,keystr,remark,updtime) values ("
                            + yw.flag + "," + gn + ",'" + yw.from + "','" + yw.keystr
                            + "','" + yw.remark + "','" + yw.updtime + "')");
                    }

                }
            }

            return true;

        }

        /**
           * 同花顺>数据中心>概念资金
           * http://data.10jqka.com.cn/funds/gnzjl/
         **/
        public static bool saveGNZJ(WebBrowser web)
        {
            NewsYw yw = null;

            string type = "概念资金";
            string url = web.Url.ToString();
            if (url.IndexOf("hyzjl") != -1)
            {
                type = "行业资金";
            }

            int gn = 0;
            DateTime qbdate = DateTime.Now;
            String htmlText = "";
            HtmlElementCollection trEles = null;
            HtmlElementCollection tdEles1 = null;
            foreach (HtmlElement item in web.Document.GetElementsByTagName("Table"))
            {
                if (item == null)
                {
                    continue;
                }
                //表
                if ("m-table J-ajax-table".Equals(item.GetAttribute("className")))
                {
                    trEles = item.Document.GetElementsByTagName("Tr");
                    int i = 0;
                    foreach (HtmlElement tritem in trEles)
                    {
                        i++;
                        if (i == 1)
                        {
                            continue;
                        }
                        htmlText = tritem.InnerText.Trim().Replace("\r\n", "");

                        string yg_type = "";
                        tdEles1 = tritem.GetElementsByTagName("Td");
                        if (tdEles1 == null || tdEles1.Count <= 0)
                        {
                            continue;
                        }
                      
                        if (tdEles1[3] != null)
                        {
                            yg_type = tdEles1[3].InnerText;
                        }

                        Console.WriteLine("yg_type:" + yg_type);

                        yw = new NewsYw();
                        yw.from = type;
                        yw.updtime = qbdate;
                        yw.flag = 0;
                        yw.remark = yg_type;
                        yw.keystr = tdEles1[1].InnerText;
                        Console.WriteLine(yw.keystr);

                        if (!String.IsNullOrEmpty(yw.keystr))
                        {
                            if (!isInsert(yw.keystr, yw.from))
                            {
                                GPUtil.helper.ExecuteNonQuery("insert into Newsyw (flag,gn,newsfrom,keystr,remark,updtime) values ("
                                  + yw.flag + "," + gn + ",'" + yw.from + "','" + yw.keystr
                                  + "','" + yw.remark + "','" + yw.updtime + "')");
                            }

                        }
                    }
                }


            }

            return true;

        }

        /**
           * 同花顺>数据中心>大单追踪
           * http://data.10jqka.com.cn/funds/ddzz/
         **/
        public static bool saveDDZZ(WebBrowser web)
        {
            NewsYw yw = null;

            string type = "大单追踪";
            string url = web.Url.ToString();           

            int gn = 0;
            DateTime qbdate = DateTime.Now;
            String htmlText = "";
            HtmlElementCollection trEles = null;
            HtmlElementCollection tdEles1 = null;
            foreach (HtmlElement item in web.Document.GetElementsByTagName("Table"))
            {
                if (item == null)
                {
                    continue;
                }
                //表
                if ("m-table J-ajax-table".Equals(item.GetAttribute("className")))
                {
                    trEles = item.Document.GetElementsByTagName("Tr");
                    int i = 0;
                    foreach (HtmlElement tritem in trEles)
                    {
                        i++;
                        if (i == 1)
                        {
                            continue;
                        }
                        htmlText = tritem.InnerText.Trim().Replace("\r\n", "");
                       
                        tdEles1 = tritem.GetElementsByTagName("Td");
                        if (tdEles1 == null || tdEles1.Count <= 0)
                        {
                            continue;
                        }                      

                        yw = new NewsYw();
                        yw.from = type;
                        yw.updtime = qbdate;
                        yw.flag = 0;
                        yw.remark = tdEles1[6].InnerText;
                        yw.remark1 = tdEles1[7].InnerText;
                        yw.keystr = tdEles1[2].InnerText;
                        Console.WriteLine(yw.keystr);

                        if (!String.IsNullOrEmpty(yw.keystr))
                        {
                            if (!isInsert(yw.keystr, yw.from))
                            {
                                GPUtil.helper.ExecuteNonQuery("insert into Newsyw (flag,gn,newsfrom,keystr,remark,remark1,updtime) values ("
                                  + yw.flag + "," + gn + ",'" + yw.from + "','" + yw.keystr
                                  + "','" + yw.remark + "','"+yw.remark1+"','" + yw.updtime + "')");
                            }

                        }
                    }
                }


            }

            return true;

        }

        public static void webGetNewsCallBack(WebBrowser gp_web, List<string> urlList, List<string> loadUrlList)
        {
            if (gp_web.Url.ToString().Equals("about:blank"))
            {
                return;
            }          
           
            gp_web.Url = new Uri(urlList[0]);

            if (!loadUrlList.Contains(urlList[0]))
            {
                processNewsUrl(gp_web, urlList[0],"");

                loadUrlList.Add(urlList[0]);

                gp_web.Navigate(urlList[loadUrlList.Count]);

            }
        }

        /**
        * 处理URL
        **/
        public static List<string> processNewsUrl(WebBrowser gp_web, string url,string newsType)
        {
            List<string> rtnUrlList = new List<string>();
            if (url.IndexOf("tzrl") != -1 || url.IndexOf("投资日历") != -1)
            {
                //保存投资日历
                SinaAPI.savetGpTZRL(gp_web);
            }
            else if (url.IndexOf("fupan") != -1)
            {
                SinaAPI.savetGPFP(gp_web);
            }
            else if (url.IndexOf("zaopan") != -1 
                )
            {
                NewsApi.saveZPBD(gp_web);
            }
            else if (url.IndexOf("qingbao") != -1 
                )
            {
                NewsApi.saveJHQB(gp_web);
            }
            else if (url.IndexOf("zhangting") != -1)
            {
                NewsApi.saveYDGC(gp_web);
            }
            else if (url.IndexOf("yaowen") != -1)
            {
                NewsApi.saveJRYW(gp_web);
            }else if (url.IndexOf("xgsgyzq") != -1)
            {
                NewsApi.saveXGSG_GP(gp_web);
            }
            else if (url.IndexOf("dzjy") != -1)
            {
                NewsApi.save_DZJY(gp_web);
            }
            else if (url.IndexOf("cjzx_list") != -1)
            {
                NewsApi.saveCJXW_LIST(gp_web);
            }
            else if (url.IndexOf("longhu") != -1)
            {
                NewsApi.save_LONGHU(gp_web);
            }
            else if (url.IndexOf("yjyg") != -1 )
            {
                NewsApi.saveYJYZ_GG(gp_web);
            }
            else if (url.IndexOf("xzjp") != -1 
                )
            {
                NewsApi.save_XZJP(gp_web);
            }
            else if (url.IndexOf("yjkb") != -1)
            {
                NewsApi.save_YJKB_GG(gp_web);
            }
            else if (url.IndexOf("yypl") != -1)
            {
                NewsApi.save_YJYP(gp_web);
            }
            else if (url.IndexOf("ggjy") != -1 )
            {
                NewsApi.save_GGCG(gp_web);
            }
            else if ( url.IndexOf("ipo") != -1)
            {
                NewsApi.saveIPO_SY_GG(gp_web);
            }
            else if (url.IndexOf("sgpx") != -1)
            {
                NewsApi.saveSGPX_GG(gp_web);
            }
            else if (url.IndexOf("xsjj") != -1 )
            {
                NewsApi.saveXSJJ_GP(gp_web);
            }
            else if (url.Equals("http://stock.10jqka.com.cn/"))
            {
                NewsApi.saveTHS_SY(gp_web);
            }
            else if (url.Equals("http://www.10jqka.com.cn/"))
            {
                NewsApi.saveTHS_JRDT(gp_web);
            }
            else if (url.IndexOf("c5866") != -1)
            {
                NewLinkApi.saveTHS_CONTENT(gp_web,newsType);
            }
            else if (url.IndexOf("ggsd") != -1 || url.IndexOf("公告") != -1)
            {
                NewsApi.save_GGSD(gp_web);
            }
            else if(url.IndexOf("lxsz") != -1 || url.IndexOf("cxfl") != -1
                || url.IndexOf("xstp") != -1
                || url.IndexOf("ljqs") != -1
                || url.IndexOf("cxg") != -1
                || url.IndexOf("cxd") != -1
                || url.IndexOf("lxxd") != -1
                || url.IndexOf("cxsl") != -1
                || url.IndexOf("xxtp") != -1
                || url.IndexOf("ljqd") != -1
                )
            {
                NewsApi.save_QSXG_THS(gp_web);
            }
            else if (url.IndexOf("hyzjl") != -1 || url.IndexOf("gnzjl") != -1)
            {
                NewsApi.saveGNZJ(gp_web);
            }
            else if (url.IndexOf("ddzz") != -1)
            {
                NewsApi.saveDDZZ(gp_web);
            }
            else if (url.IndexOf("today_list") != -1)
            {
                NewsApi.saveCJXW_LIST(gp_web);
            }           
            else if (!string.IsNullOrEmpty(newsType))
            {
                NewLinkApi.saveTHS_CONTENT(gp_web,newsType);
            }
           
            return rtnUrlList;


        }


      


    }
}
