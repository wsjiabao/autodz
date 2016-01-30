using GMSDK;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MdTZ
{
    /// <summary>
    /// 交易API
    /// </summary>
    class TdComm
    {

        public static TdApi td = null;

        private static bool isInit = false;

        /// <summary>
        /// 初始化 MD 对象
        /// </summary>
        public static int initTd()
        {
            if (isInit)
            {
                return 0;
            }

            int ret;

            //本例子演示如何用行情API提取数据
            td = TdApi.Instance;           
            ret = td.Init(
                "18221685724",
                "yjb_1983",
                "strategy_2",
                "localhost:8001"//连接到本地掘金终端, 此项为null或空字符串时，连接到掘金云服务
                ); 

            if (ret != 0)
            {
                isInit = false;              
            }
            else
            {
                isInit = true;
            }

            return ret;
        }


    }
}
