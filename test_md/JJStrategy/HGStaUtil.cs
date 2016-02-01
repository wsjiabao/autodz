using GMSDK;
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

        public static Dictionary<string, int> hasXHDic = new Dictionary<string, int>();             

        /// <summary>
        /// 是否上证大盘
        /// </summary>
        /// <param name="tick"></param>
        /// <returns></returns>
        public static bool isSHTick(Tick tick)
        {
            if (tick == null)
            {
                return false;
            }

            if (tick.sec_id.Equals("000001"))
            {
                return true;
            }

            return false;
        }

        /// <summary>        
        /// 涨幅
        /// </summary>
        /// <param name="gp"></param>
        /// <returns></returns>
        public static double getTickZF(Tick gp)
        {
            if (gp.pre_close > 0)
                {
                    return Math.Round(((gp.last_price - gp.pre_close) / gp.pre_close) * 100,2); //涨幅
                }

            return 0;
        }

        /// <summary>        
        /// 上次买入后的涨幅
        /// </summary>
        /// <param name="gp"></param>
        /// <returns></returns>
        public static double getTickZFFrmLastBuy(Tick gp, double lastBuy)
        {
            if (lastBuy > 0)
            {
                return Math.Round(((gp.last_price - lastBuy) / lastBuy) * 100, 2); //涨幅
            }

            return 0;
        }


    }
}
