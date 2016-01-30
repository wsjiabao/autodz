using GMSDK;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MdTZ
{
    /// <summary>
    ///  行情获取对象
    ///  
    /// </summary>
    class MdComm
    {
        #region 常量定义
        public const string DEFAULT_STRATAGY_CONFIG = "strategy.ini";
        public const string DEFAULT_STRATAGY_ID = "startegy_1";
        public const double DEFAULT_INITIAL_CASH = 1000000;


        /// <summary>
        /// 日频行情
        /// 日频行情仅在策略回测时订阅使用，其他策略运行模式下订阅将接收不到DailyBar。
        /// </summary>
        public  const string HQ_TYPE_DAILY = "daily";

        

        #endregion

        public static MdApi md = null;
        private static bool isInit = false;

        /// <summary>
        /// 初始化 MD 对象
        /// </summary>
        public static void initMd()
        {
            if (isInit)
            {
                return;
            }

            //本例子演示如何用行情API提取数据
            md = MdApi.Instance;

            int ret = md.Init("18221685724", "yjb_1983");

            if (ret != 0)
            {
                isInit = false;
                //登录失败
                return;
            }
            else
            {
                isInit = true;
            }
        }

        /// <summary>
        /// 设置配置文件
        /// </summary>
        /// <param name="strategy"></param>
        /// <param name="cfgFile"></param>
        /// <returns></returns>
        public static int setStrategyConfig(Strategy strategy, string cfgFile)
        {
            if (string.IsNullOrEmpty(cfgFile))
            {
                cfgFile = DEFAULT_STRATAGY_CONFIG;
            }
           
            int ret = strategy.InitWithConfig(cfgFile);
            if (ret != 0)
            {
                System.Console.WriteLine("Init error: {0}", ret);
            }

            return ret;
           
        }

        /// <summary>
        /// 初始化策略
        /// </summary>
        /// <param name="strategy"></param>
        /// <param name="mdMode"></param>
        /// <param name="codes"></param>
        /// <param name="strategyId"></param>
        /// <param name="bar_cfg"></param>
        /// <param name="bTime"></param>
        /// <param name="eTime"></param>
        /// <returns></returns>
        public static int setStrategyInit(Strategy strategy,int mdMode,string codes,String strategyId,string bar_cfg)
        {           
          
            if (String.IsNullOrEmpty(codes))
            {
                return -1;
            }

            if (bar_cfg == null)
            {
                bar_cfg = "bar.60";
            }

            if (string.IsNullOrEmpty(strategyId))
            {
                strategyId = DEFAULT_STRATAGY_ID;
            }
          
            string subscribe_symbols = "";
            List<string> codeList = codes.Split(",".ToCharArray()).ToList();
            foreach (string c in codeList)
            {
                subscribe_symbols += MdComm.getJJCfgCode(c, "tick", bar_cfg) + ",";
            }
        
            int ret = -1;
            switch (mdMode)
            {
                case 1:
                    ret = strategy.Init("18221685724",
                         "yjb_1983",
                         strategyId,
                         subscribe_symbols,
                         MDMode.MD_MODE_NULL,
                         "localhost:8001");
                    break;
                case 2:
                    ret = strategy.Init("18221685724",
                         "yjb_1983",
                         strategyId,
                         subscribe_symbols,
                         MDMode.MD_MODE_LIVE,
                         "localhost:8001");
                    break;
                case 3:
                     ret = strategy.Init("18221685724",
                         "yjb_1983",
                         strategyId,
                         subscribe_symbols,
                         MDMode.MD_MODE_SIMULATED,
                         "localhost:8001");
                    break;
                 case 4:
                    ret = strategy.Init("18221685724",
                         "yjb_1983",
                         strategyId,
                         subscribe_symbols,
                         MDMode.MD_MODE_PLAYBACK,
                         "localhost:8001");
                    break;
                default:
                    ret = strategy.Init("18221685724",
                         "yjb_1983",
                         strategyId,
                         subscribe_symbols,
                         MDMode.MD_MODE_SIMULATED,
                         "localhost:8001");
                    break;
            }
       
            if (ret != 0)
            {
                System.Console.WriteLine("Init error: {0}", ret);
            }

            return ret;

        }

        
        /// <summary>
        /// 参数名	类型	说明        
        /**
            start_time	string	回放行情开始时间，格式：yyyy-mm-dd HH:MM:SS
            end_time	string	回放行情结束时间，格式：yyyy-mm-dd HH:MM:SS
            initial_cash	double	回测初始资金，默认1000000
            transaction_ratio	double	委托量成交比率,默认1，按委托量全部成交
            commission_ratio	double	手续费率,默认0，无手续费
            slippage_ratio	double	滑点比率,默认0，无滑点
            price_type	int	复权方式，0-不复权，1-前复权 **/
        /// </summary>
        /// <param name="strategy"></param>
        /// <param name="start_time"></param>
        /// <param name="end_time"></param>
        /// <param name="initial_cash"></param>
        public static void setStrategyBackTestInit(Strategy strategy, string start_time,
               string end_time,
               double initial_cash 
         )
        {
            if (initial_cash == 0)
            {
                initial_cash = DEFAULT_INITIAL_CASH;
            }

            strategy.BacktestConfig(start_time, end_time, initial_cash, 1, 0, 0, 1);            
        }

       /// <summary>
        ///  转化URL code
       /// </summary>
       /// <param name="code"></param>
       /// <returns></returns>
        public static string getJJCfgCode(string code, string tick_cfg, string bar_cfg)
        {
            string rtnCode = "";
            if (string.IsNullOrEmpty(code))
            {
                return rtnCode;
            }

            if (code.IndexOf("SHSE") != -1 || code.IndexOf("SZSE") != -1)
            {
                return code;
            }   

            if (code.IndexOf("sh") != -1 || code.IndexOf("sz") != -1)
            {
                code = code.Replace("sh", "").Replace("sz", "");
            }           

            if (code.IndexOf("6") == 0)
            {
                rtnCode = "SHSE." + code;
            }
            else if (code.IndexOf("0") == 0 || code.IndexOf("3") == 0)
            {
                rtnCode = "SZSE." + code;
            }

            string cfgRtnCode = rtnCode;
            if (!string.IsNullOrEmpty(tick_cfg))
            {
                cfgRtnCode = rtnCode + "." + tick_cfg;
            }

            if (!string.IsNullOrEmpty(bar_cfg))
            {
                cfgRtnCode = cfgRtnCode+","+rtnCode + "." + bar_cfg;
            }

            return cfgRtnCode;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        public static string getJJCode(string code)
        {
            return getJJCfgCode(code,"","");
        }

      
    }
}
