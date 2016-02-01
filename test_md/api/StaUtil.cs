using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace MdTZ
{
    class StaUtil
    {
        /// <summary>
        /// 获取交易BEAN
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public static List<GuoPiao> getTransBean(List<GuoPiao> gps, string type)
        {
            if (gps == null)
            {
                return null;
            }
            List<GuoPiao> rtnBeans = new List<GuoPiao>();

            if ("dp".Equals(type))
            {
                for (int i = 0; i < 2; i++)
                {
                    rtnBeans.Add(gps[i]);
                }
            }
            else if ("buy".Equals(type))
            {
                foreach(GuoPiao gp in gps) {
                    if (TranApi.buyCodes.Contains(gp.code))
                    {
                        rtnBeans.Add(gp);
                    }
                }
            }
            else if ("sell".Equals(type))
            {
                foreach (GuoPiao gp in gps)
                {
                    if (TranApi.sellCodes.Contains(gp.code))
                    {
                        rtnBeans.Add(gp);
                    }
                }
            }

            return rtnBeans;
        }


       

    }
}
