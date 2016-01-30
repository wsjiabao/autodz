using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace MdTZ
{
    /// <summary>
    /// 海龟策略工具类
    /// </summary>
    class HGStaUtil
    {
        public static Dictionary<string, HgZb> hgDics = new Dictionary<string, HgZb>();

        /// <summary>
        /// 获取指标MAP
        /// </summary>
        /// <param name="codes"></param>
        /// <returns></returns>
        public static Dictionary<string, HgZb> getHgZbList(string codes)
        {

            if (codes.IndexOf(",") != -1)
            {
                codes = codes.Substring(0, codes.IndexOf(","));
            }

            Dictionary<string, HgZb> hgDics = new Dictionary<string, HgZb>();
            HgZb zb = null;

            DataTable dataTable = GPUtil.helper.ExecuteDataTable("SELECT code,zg20,zd20,zg55,zd55 FROM gpjx where code in (" + codes+ ")", GPUtil.parms);
        
            if (dataTable != null)
            {
                foreach (DataRow row in dataTable.Rows)
                {
                    zb = new HgZb();
                    zb.code = row["code"].ToString();
                    zb.zd20 = (Double)row["zd20"];                 
                    zb.zg20 = (Double)row["zg20"];
                    zb.zd55 = (Double)row["zg20"];
                    zb.zg55 = (Double)row["zg55"];
                    hgDics.Add(zb.code, zb);
                }
            }
            return hgDics;
        }

       
       

        


    }
}
