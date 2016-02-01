using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using QTP.TAlib;
using GMSDK;

namespace QTP.Domain
{
    public class KLineDaily : IKLine
    {
        private DailyBar bar;

        public KLineDaily(DailyBar bar)
        {
            this.bar = bar;
        }

        #region IKline

        public float OPEN
        {
            get { return bar.open; }
        }

        public float CLOSE
        {
            get { return bar.close; }
        }

        public float HIGH
        {
            get { return bar.high; }
        }

        public float LOW
        {
            get { return bar.low; }
        }


        #endregion
    }

    public class KLineBar : IKLine
    {
        private Bar bar;

        public KLineBar(Bar bar)
        {
            this.bar = bar;
        }

        #region IKline

        public float OPEN
        {
            get { return bar.open; }
        }

        public float CLOSE
        {
            get { return bar.close; }
        }

        public float HIGH
        {
            get { return bar.high; }
        }

        public float LOW
        {
            get { return bar.low; }
        }


        #endregion
    }

}
