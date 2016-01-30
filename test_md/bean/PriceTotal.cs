using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MdTZ
{
    /**
     * 日交易分时统计
     * */
    public class PriceTotal
    {
        public double price { get; set; }
        public double cj_num { get; set; }
        public double zb { get; set; }  //占比
        public string code { get; set; }
    }
}
