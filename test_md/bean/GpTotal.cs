using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MdTZ
{
    public class GpTotal
    {
        public string code { get; set; }
      
        public double dqj { get; set; } //3：”26.91″，当前价格；           

        public double costPrice { get; set; } //成本价
     
     
        /// <summary>
        /// 最小单位
        /// </summary>
        public int minUnit { get; set; }

        public double atr { get; set; }

        public double jj5 { get; set; } //均价
        public double jj10 { get; set; } //均价
        public double jj20 { get; set; } //均价
        public double buyzf { get; set; }//买入涨幅
       
       
    }
}
