using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MdTZ
{

    /**
        投资复盘
    **/
    public class Dzfp
    {
        public int id { get; set; }
        public string eventType { get; set; }   
        public string gpname { get; set; } //关键字
        public double zf { get; set; } //次数      
        public string remark { get; set; } //关键字
        public DateTime updtime { get; set; } //次数        
    }
}
