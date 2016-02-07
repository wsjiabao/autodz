using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MdTZ
{
     public class GuoPiao
    {

        public GpTotal total { get; set; }
        public string code { get; set; }
        public string name { get; set; } //0：”大秦铁路”，股票名字；
        public double kpj { get; set; } //1：”27.55″，今日开盘价；
        public double zrspj { get; set; } //2：”27.25″，昨日收盘价；
        public double dqj { get; set; } //3：”26.91″，当前价格；
        public double jrzgj { get; set; }//4：”27.55″，今日最高价；
        public double jrzdj { get; set; }//5：”26.20″，今日最低价；
        public double b_now { get; set; }//6：”26.91″，竞买价，即“买一”报价；
        public double s_now { get; set; }//7：”26.92″，竞卖价，即“卖一”报价；
        public double cj_num { get; set; }//8：”22114263″，成交的股票数，由于股票交易以一百股为基本单位，所以在使用时，通常把该值除以一百；
        public double cj_amt { get; set; }//9：”589824680″，成交金额，单位为“元”，为了一目了然，通常以“万元”为成交金额的单位，所以通常把该值除以一万；

        public double b1_num { get; set; }//10：”4695″，“买一”申请4695股，即47手；
        public double b1_price { get; set; }//11：”26.91″，“买一”报价；
        public double b2_num { get; set; }//12：”57590″，“买二”
        public double b2_price { get; set; }//13：”26.90″，“买二”
        public double b3_num { get; set; }//14：”14700″，“买三”
        public double b3_price { get; set; }//15：”26.89″，“买三”
        public double b4_num { get; set; }//16：”14300″，“买四”
        public double b4_price { get; set; }//17：”26.88″，“买四”
        public double b5_num { get; set; } //18：”15100″，“买五”
        public double b5_price { get; set; } //19：”26.87″，“买五”

        public double s1_num { get; set; }//20：”3100″，“卖一”申报3100股，即31手；
        public double s1_price { get; set; }//21：”26.92″，“卖一”报价

        public double s2_num { get; set; }//20：”3100″，“卖一”申报3100股，即31手；
        public double s2_price { get; set; }//21：”26.92″，“卖一”报价

        public double s3_num { get; set; }//20：”3100″，“卖一”申报3100股，即31手；
        public double s3_price { get; set; }//21：”26.92″，“卖一”报价

        public double s4_num { get; set; }//20：”3100″，“卖一”申报3100股，即31手；
        public double s4_price { get; set; }//21：”26.92″，“卖一”报价

        public double s5_num { get; set; }//20：”3100″，“卖一”申报3100股，即31手；
        public double s5_price { get; set; }//21：”26.92″，“卖一”报价


        public string date { get; set; }//30：”2008-01-11″，日期；
        public string time { get; set; }//31：”15:05:32″，时间；


         /**
          * 附加属性
          **/
        public double zhengf { get; set; } //振幅
        public double jj { get; set; } //均价
        public double  zf { get; set; }//涨幅

        public double hsl { get; set; }
        public double ltsz { get; set; }
        public double syl { get; set; }

        public double  tr { get; set; }

     
         

    }
}
