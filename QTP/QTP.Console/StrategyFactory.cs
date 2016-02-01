using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using System.Configuration;

using QTP.Infra;
using QTP.DBAccess;
using QTP.Domain;

namespace QTP.Console
{
    public class StrategyFactory
    {
        public static StrategyQTP CreateFromDB(string id, NLog nlog)
        {
            // Read config file
            Configuration config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            CRUD.ConnectionString = config.ConnectionStrings.ConnectionStrings["QTP_DB"].ConnectionString.ToString();

            TStrategy t = CRUD.GetTStrategy(id, nlog);

            Assembly assembly = Assembly.LoadFrom(@"QTP.Domain.dll");
            Type type = assembly.GetType(string.Format("QTP.Domain.{0}", t.RiskMClass));

            RiskM trader = (RiskM)Activator.CreateInstance(type);

            StrategyQTP s = new StrategyQTP(t, trader, nlog);
            
            return s;
        }
    }
}
