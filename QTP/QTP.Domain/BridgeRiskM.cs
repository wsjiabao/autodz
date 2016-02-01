using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using QTP.DBAccess;
using GMSDK;

namespace QTP.Domain
{
    public class BridgeRiskM : RiskM
    {

        public override bool Initialize()
        {
            if (base.Initialize() == false)
            {
                return false;
            }

            // itself init
            strategy.WriteInfo(string.Format("{0} 初始化完成", this.ToString()));
            return true;
        }

        public override double GetVolume(string exchange, string sec_id)
        {
            return 1000;
        }
    }
}
