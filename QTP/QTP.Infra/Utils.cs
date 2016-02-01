using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QTP.Infra
{
    public class Utils
    {
        public static string StampToDateTimeString(double utc)
        {
            DateTime dateTimeStart = TimeZone.CurrentTimeZone.ToLocalTime(new DateTime(1970, 1, 1));
            DateTime dt = dateTimeStart.AddSeconds(utc);
            return dt.ToShortDateString() + " " + dt.ToLongTimeString();
        }
    }
}
