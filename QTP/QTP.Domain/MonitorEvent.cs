using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using GMSDK;

namespace QTP.Domain
{
    public class MonitorEvent
    {
        public Tick tick;
        public MonitorEvent(Tick tick)
        {
            this.tick = tick;
        }
    }

    public class OpenLongMonitorEvent : MonitorEvent
    {
        public OpenLongMonitorEvent(Tick tick)
            : base(tick)
        { }
    }

    public class CloseLongMonitorEvent : MonitorEvent
    {
        public double Volume { get; set; }
        public CloseLongMonitorEvent(Tick tick, double volume)
            : base(tick)
        {
            Volume = volume;
        }
    }

    public class OpenShortMonitorEvent : MonitorEvent
    {
        public OpenShortMonitorEvent(Tick tick)
            : base(tick)
        { }
    }

    public class CloseShortMonitorEvent : MonitorEvent
    {
        public double Volume { get; set; }
        public CloseShortMonitorEvent(Tick tick, double volume)
            : base(tick)
        {
            Volume = volume;
        }
    }

}
