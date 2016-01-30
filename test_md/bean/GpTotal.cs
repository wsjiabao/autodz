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
        public double lastPrice { get; set; } //上个周期价格
        public double zhengf { get; set; } //振幅
        public double jj { get; set; } //均价
        public double zf { get; set; }//涨幅
        public double interZf { get; set; } //主线程周期涨幅

        public int strongCnt { get; set; } //强于大盘占比
        public int weekCnt { get; set; } //弱于大盘
    }
}
