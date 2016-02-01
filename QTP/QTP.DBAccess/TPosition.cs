using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QTP.DBAccess
{
    public class TPosition
    {
        public string Exchange { get; set; }

        public string InstrumentId { get; set; }
        public int Volumn { get; set; }
        public int StopLossStyle { get; set; }
        public double InPrice { get; set; }
    }
}
