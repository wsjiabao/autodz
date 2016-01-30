using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MdTZ
{
    class ApiRequest
    {
        public class HaoserviceData
        {        
            public string error_code { get; set; }
            public string reason { get; set; }
            public List<HaoserviceResultData> result { get; set; }


        }

        public class HaoserviceResultData
        {
            // [ScriptIgnore]
            public string name { get; set; }    /*货币名称*/
            public string fBuyPri { get; set; }   /*现汇买入价*/
            public string mBuyPri { get; set; }      /*现钞买入价*/
            public string fSellPri { get; set; }    /*现汇卖出价*/
            public string mSellPri { get; set; }    /*现钞卖出价*/
            public string bankConversionPri { get; set; }      /*中行折算价*/
            public string date { get; set; }   /*发布日期*/
            public string time { get; set; }  /*发布时间*/
        }


        public class JuheData
        {
            public string error_code { get; set; }
            public string reason { get; set; }
            public JuheResultData result { get; set; }
        }

        public class JuheResultData
        {
            public string update { get; set; }
            public List<string[]> list { get; set; }
        }


        public class JuheCurrencyData
        {
            public string error_code { get; set; }
            public string reason { get; set; }
            public List<JuheResultCurrencyData> result { get; set; }
        }      

        public class JuheResultCurrencyData
        {
            // [ScriptIgnore]
            public string currencyF { get; set; }   
            public string currencyF_Name { get; set; }   
            public string currencyT { get; set; }     
            public string currencyT_Name { get; set; }    
            public string currencyFD { get; set; }   
            public string exchange { get; set; }    
            public string result { get; set; }  
            public string updateTime { get; set; }  
        }






    }
}
