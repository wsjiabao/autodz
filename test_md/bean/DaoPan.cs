using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MdTZ
{
     public class DaoPan
    {
        public string code { get; set; }
        public string name { get; set; } //指数名称
        public double zs { get; set; } //当前点数
        public double zds { get; set; } //当前价格
        public double zdl { get; set; } //涨跌率
        public int cjl { get; set; }//成交量（手）
        public double cje { get; set; }//成交额（万元）

        public double real_zf { get; set; } //实时涨跌幅
        
    }
}
