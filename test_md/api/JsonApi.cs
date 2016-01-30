
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Web.Script.Serialization;

namespace MdTZ
{
    class JsonApi
    {

        //http://www.haoservice.com/
        public static string haoservice_url = "http://apis.haoservice.com/lifeservice/exchange/rmbquot?key=c3a6699581464f59bec0a0d1626cf8f2";

        //https://www.juhe.cn//
        public static string juhe_url = "http://op.juhe.cn/onebox/exchange/query?key=f85ae593816d8e6461d4822938462db0";

        //https://www.juhe.cn
        public static string juhe_url_currency = "http://op.juhe.cn/onebox/exchange/currency?key=f85ae593816d8e6461d4822938462db0&from=USD&to=CNY"; 

        /// <summary>
        /// 发送HTTP请求
        /// </summary>
        /// <param name="url">请求的URL</param>
        /// <param name="param">请求的参数</param>
        /// <returns>请求结果</returns>
        private static string getRequest(string url)
        {
            string strURL = url;
            System.Net.HttpWebRequest request;
            request = (System.Net.HttpWebRequest)WebRequest.Create(strURL);
            request.Method = "GET";
            // 添加header
            //request.Headers.Add("apikey", "ace0d47665dad42fe87784faa62fed49");
            System.Net.HttpWebResponse response;
            response = (System.Net.HttpWebResponse)request.GetResponse();
            System.IO.Stream s;
            s = response.GetResponseStream();
            string StrDate = "";
            string strValue = "";
            StreamReader Reader = new StreamReader(s, Encoding.UTF8);
            while ((StrDate = Reader.ReadLine()) != null)
            {
                strValue += StrDate + "\r\n";
            }
            return strValue;
        }


        /// <summary>
        /// 
        /// 人民币中间价
        /// </summary>
        /// <returns></returns>
        public static ApiRequest.HaoserviceData gettHaoserviceRmbQuot()
        {

            JavaScriptSerializer jsonSerializer = new JavaScriptSerializer();
            //执行反序列化
            ApiRequest.HaoserviceData obj = jsonSerializer.Deserialize<ApiRequest.HaoserviceData>(getRequest(haoservice_url));

            return obj;
           
        }

        public static ApiRequest.JuheData getJuheRmbQuot()
        {

            JavaScriptSerializer jsonSerializer = new JavaScriptSerializer();
            string jsone = getRequest(juhe_url);
            //执行反序列化
            ApiRequest.JuheData obj = jsonSerializer.Deserialize<ApiRequest.JuheData>(jsone);

            return obj;

        }

        public static ApiRequest.JuheCurrencyData getJuheCurrencyRmbQuot()
        {

            JavaScriptSerializer jsonSerializer = new JavaScriptSerializer();
            string jsone = getRequest(juhe_url_currency);
            //执行反序列化
            ApiRequest.JuheCurrencyData obj = jsonSerializer.Deserialize<ApiRequest.JuheCurrencyData>(jsone);

            return obj;

        }

        /// <summary>
        /// 获取人民币利率
        /// </summary>
        /// <returns></returns>
        public static double getRMBRate()
        {
            ApiRequest.JuheCurrencyData data = getJuheCurrencyRmbQuot();
            return Convert.ToDouble(data.result[0].result);
        }
       
    }
}
