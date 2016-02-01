using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace MdTZ
{
    class Sta_CDFT
    {

        /// <summary>
        /// 下跌大盘
        /// </summary>
        /// <returns></returns>
        public static bool isDpContionDown()
        {
            string his = GPUtil.getDPHis();
            return Regex.IsMatch(his, "^.*[0-4]{2,5}$")
                   || Regex.IsMatch(his, "^.*[0-3]+$");
        }


        /// <summary>
        /// 判断大盘突破
        /// </summary>
        /// <param name="dp"></param>
        /// <returns></returns>
        public static bool isBuyXH(GuoPiao dp)
        {
            if (!isDpContionDown())
            {
                return false;
            }
            //大盘涨幅达到1的时候买入头寸
            if (dp.zf >=1)
            {
                return true;
            }

            return false;
        }


    }
}
