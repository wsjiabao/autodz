using GMSDK;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MdTZ
{
    class MdQry
    {
        /// <summary>
        /// 提取最新的1笔Tick数据，支持单个代码提取或多个代码组合提取。策略类和行情服务类都提供该接口。
        /// </summary>
        /// <param name="codes"></param>
        /// <returns></returns>
        public static List<Tick> GetLastTicks(string codes)
        {
             //初始化账号
             MdComm.initMd();

             string codes_str = "";
             List<string> codeList = codes.Split(",".ToCharArray()).ToList();
             foreach(string c in codeList) {
                 codes_str += MdComm.getJJCode(c) + ",";
             }

             var ticks = MdComm.md.GetLastTicks(codes_str);

             return ticks;
        }

        /// <summary>
        /// 提取最新1条Bar数据，支持单个代码提取或多个代码组合提取。策略类和行情服务类都提供该接口。
        /// </summary>
        /// <param name="codes"></param>
        /// <param name="barType"></param>
        /// <returns></returns>
        public static List<Bar> GetLastBars(string codes, int barType)
        {
            //初始化账号
            MdComm.initMd();

            //默认30描述
            if (barType <= 0)
            {
                barType = 30;
            }

            string codes_str = "";
            List<string> codeList = codes.Split(",".ToCharArray()).ToList();
            foreach (string c in codeList)
            {
                codes_str += MdComm.getJJCode(c) + ",";
            }

            var bars = MdComm.md.GetLastBars(codes_str, barType);

            return bars;
        }


        /// <summary>
        /// 提取最新1条DailyBar数据，支持单个代码提取或多个代码组合提取。策略类和行情服务类都提供该接口。
        /// </summary>
        /// <param name="codes"></param>
        /// <returns></returns>
        public static List<DailyBar> GetLastDailyBars(string codes)
        {
            //初始化账号
            MdComm.initMd();          

            string codes_str = "";
            List<string> codeList = codes.Split(",".ToCharArray()).ToList();
            foreach (string c in codeList)
            {
                codes_str += MdComm.getJJCode(c) + ",";
            }

            var bars = MdComm.md.GetLastDailyBars(codes_str);

            return bars;
        }


     





    }
}
