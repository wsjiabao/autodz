using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MdTZ
{

    /**
        投资日历
    **/
    public class Dzrl
    {
        public int id { get; set; }

        public int gn { get; set; }

        public string keystr { get; set; }
        public string gpstr { get; set; } //关键字
        public string rl { get; set; } //次数      
        public DateTime updtime { get; set; } //次数        
    }
}
