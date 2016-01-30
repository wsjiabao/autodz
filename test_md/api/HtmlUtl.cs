using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;

namespace MdTZ
{
    class HtmlUtl
    {

        /**
         * 保存到本地HTML
         **/        
        public static string GetToLocalHtml(string url, string exportPath)
        {
            string htm = "";
            try
            {
                WebClient webClient = new WebClient();
                webClient.Credentials = CredentialCache.DefaultCredentials;//获取或设置用于向Internet资源的请求进行身份验证的网络凭据  
                Byte[] pageData = webClient.DownloadData(url);
                string pageHtml = Encoding.Default.GetString(pageData);  //如果获取网站页面采用的是GB2312，则使用这句         
                //string pageHtml = Encoding.UTF8.GetString(pageData); //如果获取网站页面采用的是UTF-8，则使用这句  
                //string pageHtml = Encoding.GetEncoding("GBK").GetString(pageData); //如果获取网站页面采用的是UTF-8，则使用这句  
                using (StreamWriter sw = new StreamWriter(exportPath))//将获取的内容写入文本  
                {
                    htm = sw.ToString();//测试StreamWriter流的输出状态，非必须  
                    sw.Write(pageHtml);
                }
            }
            catch (WebException webEx)
            {
                Console.WriteLine(webEx.Message);
            }

            return exportPath;
        }

        /**
         HTML保存到本地
        **/
        public static void GetToLocalHtml1()
        {
            var url = "http://stock.10jqka.com.cn/fincalendar.shtml#2015-12-18";
            string strBuff = "";//定义文本字符串，用来保存下载的html  
            int byteRead = 0;

            HttpWebRequest webRequest = (HttpWebRequest)WebRequest.Create(url);
            HttpWebResponse webResponse = (HttpWebResponse)webRequest.GetResponse();
            //若成功取得网页的内容，则以System.IO.Stream形式返回，若失败则产生ProtoclViolationException错 误。在此正确的做法应将以下的代码放到一个try块中处理。这里简单处理   
            Stream reader = webResponse.GetResponseStream();
            ///返回的内容是Stream形式的，所以可以利用StreamReader类获取GetResponseStream的内容，并以StreamReader类的Read方法依次读取网页源程序代码每一行的内容，直至行尾（读取的编码格式：UTF8）  
            StreamReader respStreamReader = new StreamReader(reader, Encoding.UTF8);

            ///分段，分批次获取网页源码  
            char[] cbuffer = new char[1024];
            byteRead = respStreamReader.Read(cbuffer, 0, 256);
            string htm = "";           
            while (byteRead != 0)
            {
                string strResp = new string(cbuffer, 0, byteRead);
                strBuff = strBuff + strResp;
                byteRead = respStreamReader.Read(cbuffer, 0, 256);
            }
            using (StreamWriter sw = new StreamWriter("d:\\GetHtml.html"))//将获取的内容写入文本  
            {
                htm = sw.ToString();//测试StreamWriter流的输出状态，非必须  
                sw.Write(strBuff);
            }
        }


        public static void demo_click()
        {
            ////模拟鼠标事件
            //var manDiv = gp_web.Document.GetElementsByTagName("div");
            //var pageInfo = gp_web.Document.GetElementsByTagName("span");
            //int pageCnt = 0;
            //foreach (HtmlElement span in pageInfo)
            //{
            //    //表
            //    if ("page_info".Equals(span.GetAttribute("className")))
            //    {
            //        pageCnt = Convert.ToInt16(span.InnerText.Trim().Substring(2));
            //    }
            //}


            //foreach (HtmlElement div in manDiv)
            //{
            //    //表
            //    if ("m-page J-ajax-page".Equals(div.GetAttribute("className")))
            //    {
            //        HtmlElementCollection links = div.GetElementsByTagName("a");
            //        foreach (HtmlElement btn in links)
            //        {
            //            if (btn.InnerText.Equals("下一页"))
            //            {
            //                for (int i = 0; i < pageCnt; i++)
            //                {
            //                    string localUrl = HtmlUtl.GetToLocalHtml(url, "d:\\gggg" + i + ".html");

            //                    Console.WriteLine("btn.InvokeMember");
            //                    btn.InvokeMember("click");
            //                }

            //            }
            //        }
            //    }
            //}
        }

    }
}
