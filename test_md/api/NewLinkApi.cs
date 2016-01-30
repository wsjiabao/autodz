using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace MdTZ
{
    class NewLinkApi
    {

        /**
         *今日头条-同花顺
         * http://www.10jqka.com.cn/
         **/
       public static bool saveTHS_CONTENT(WebBrowser web,string newsFrom)
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
                if ("atc-content".Equals(item.GetAttribute("className")))
                {
                    htmlText = item.InnerText.Trim().Replace("\r\n", "");
                    Console.WriteLine(htmlText);
                    foreach (string key in allKeys)
                    {
                        if (htmlText.IndexOf(key) != -1)
                        {
                            yw = new NewsYw();

                            yw.from = newsFrom;                       
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
                                if (!NewsApi.isInsert(yw.keystr,yw.from))
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
        *新浪外汇
        *http://vip.stock.finance.sina.com.cn/mkt/?f=caishou#jbhl_forex
        **/
       public static bool save_WH_SINA(WebBrowser web)
       {

         
           foreach (HtmlElement item in web.Document.GetElementsByTagName("Div"))
           {
               if (item == null)
               {
                   continue;
               }
               //表
               if ("price_bar down".Equals(item.GetAttribute("className")))
               {

                   Console.WriteLine(item.InnerText);
                   //foreach(HtmlElement link_item in item.Document.Links)
                   //{
                   //    Console.WriteLine(link_item.InnerText);
                   //    Console.WriteLine(link_item.GetAttribute("href"));
                   //    if (link_item.GetAttribute("href").ToLower().
                   //        IndexOf(link_define) != -1)
                   //    {

                   //        urlList.Add(link_item.GetAttribute("href"));
                   //        Console.WriteLine(link_item.GetAttribute("href"));

                   //    }

                   //}
               }

           }

           return true;
       }


       /**
        *新闻联播-同花顺
        * http://news.10jqka.com.cn/ysxwlb_list/
        **/
       public static List<string> save_NWLB_THS_LINK(WebBrowser web)
        {

            List<string> urlList = new List<string>();
            List<String> allKeys = new List<string>();
            allKeys.AddRange(KeyWordAPI.keyData.Keys);
            //allKeys.AddRange(KeyWordAPI.gpNameList);

            string link_define = "http://news.10jqka.com.cn/20160107";
            Console.WriteLine("link_define:" + link_define);
            foreach (HtmlElement item in web.Document.GetElementsByTagName("Div"))
            {
                if (item == null)
                {
                    continue;
                }
                //表
                if ("list-con".Equals(item.GetAttribute("className")))
                {

                    Console.WriteLine(item.InnerText);
                    //foreach(HtmlElement link_item in item.Document.Links)
                    //{
                    //    Console.WriteLine(link_item.InnerText);
                    //    Console.WriteLine(link_item.GetAttribute("href"));
                    //    if (link_item.GetAttribute("href").ToLower().
                    //        IndexOf(link_define) != -1)
                    //    {

                    //        urlList.Add(link_item.GetAttribute("href"));
                    //        Console.WriteLine(link_item.GetAttribute("href"));

                    //    }
                                    
                    //}
                }

            }

            return urlList;
        }


    }
}
