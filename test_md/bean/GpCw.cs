using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MdTZ
{

    /**
    每股净资产	14.4934元
每股收益	1.9923元
每股现金含量	15.796元
每股资本公积金	3.2509元
固定资产合计	 
流动资产合计	 
资产总计	482263000万元
长期负债合计	 
主营业务收入	10863200万元
财务费用	 
净利润	3716200万元

    **/
    public class GpCw
    {
        public string code { get; set; }
        public double mgjzc { get; set; }
        public double mgsy { get; set; } 
        public double mgxjhl { get; set; }
        public double mgzbgjj { get; set; }
        public double zyywsr { get; set; } 
        public double jll { get; set; }
        public DateTime date { get; set; }

    }
}
