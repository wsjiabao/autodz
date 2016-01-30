using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Script.Serialization;

namespace MdTZ
{


    public class BaiduData
    {
        // [ScriptIgnore]
        public int errNum { get; set; }

        public string errMsg { get; set; }

        public BaiduRetData retData { get; set; }


    }

    public class BaiduRetData
    {
        // [ScriptIgnore]
        public string fromCurrency { get; set; }

        public string toCurrency { get; set; }

        public string date { get; set; }
        public string time { get; set; }
        public string currency { get; set; }
        public string amount { get; set; }
        public double convertedamount { get; set; }


    }

    public class JsonPaserWeb
    {
        // Object->Json
        public static string Serialize(BaiduData obj)
        {
            JavaScriptSerializer jsonSerializer = new JavaScriptSerializer();
            string json = jsonSerializer.Serialize(obj);
            return json;
        }

        // Json->Object
        public static BaiduData Deserialize(string json)
        {
            JavaScriptSerializer jsonSerializer = new JavaScriptSerializer();
            //执行反序列化
            BaiduData obj = jsonSerializer.Deserialize<BaiduData>(json);
            return obj;
        }
    }
}
