using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MySql.Data.MySqlClient;
using System.Data;

namespace MdTZ
{
    /**
     * 均线策略：
     *    1, 获取振幅在低位的小盘股
     *    2,监控这些小盘股，如果振幅变大并且股票跌幅增大则去除，反之投入股票池并考虑买入
     * */
    class QuShi
    {
        /// <summary>
        /// 
        /// 趋势值
        /// </summary>
        public static double qsz = 0;

        /// <summary>
        /// 获取趋势值
        /// </summary>
        /// <returns></returns>
        public static double getQuShiFS()
        {
            DataRow row = GPUtil.helper.ExecuteDataRow("select total from gpparam", GPUtil.parms);          
            if (row != null)
            {              
                if (row["total"] != null && !string.IsNullOrEmpty((row["total"].ToString()))) {
                     return Convert.ToDouble(row["total"]);
                }               
            }
            return -99;
        }

    }
}
