using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

/**
 * 
 * 仓位管理
 * */
namespace MdTZ
{
    class StoreMng
    {
        /**
         * 级别 仓位 map
         * */
        public static Dictionary<int, double> levelDict = new Dictionary<int, double>{
          {1,10.0}, {2,20.0}, {3,50}, {4,70}, {5,100}
         };

        /**
         * 仓金额 
         * */
        public static double amt =2000;

        /**
       * 已购金额
       * */
        public static double buyedAmt = 100;

        /**
         * 剩余金额
         * */
        public static double liveAmt = amt - buyedAmt;

        /**
         * 
         * 获取可能仓位
         * */
        public static double getCanBuyAmt(int dpLevel)
        {
            if (levelDict.Keys.Contains(dpLevel))
            {
                return amt * (levelDict[dpLevel] / 100);
            }
            else
            {
                return 0;
            }
        }

        /**
        * 
        * 获取可能仓位
        * */
        public static void updateBuyedAmt(double amt)
        {
           
        }


    }
}
