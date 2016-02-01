using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using GMSDK;
using QTP.TAlib;

namespace QTP.Domain
{
    public class DailyTA
    {       
        private RList<KLineDaily> xs;

        // Quota lists
        private class Quota
        {
            public double MTR { get; set; }
            public double ATR { get; set; }
        }

        private RList<Quota> ys;

        public DailyTA()
        {
            xs = new RList<KLineDaily>();
            ys = new RList<Quota>();
        }

        public void Push(KLineDaily kx)
        {
            xs.Add(kx);
            ys.Add(new Quota());

            Formula.ATR<KLineDaily, Quota>(xs, 14, ys);
        }
    }
}
