using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MdTZ
{
    /**
     * 损益管理
     * */
    class ProfitMng
    {
        public static Dictionary<int, double> profitDict = new Dictionary<int, double>{
          {1,2}, {2,5}, {3,6}, {4,8}, {5,10}
         };

        public static Dictionary<int, double> lossDict = new Dictionary<int, double>{
          {1,1}, {2,3}, {3,5}, {4,8}, {5,10}
         };

    }
}
