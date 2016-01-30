using System;
using System.Runtime.InteropServices;
using System.Drawing;

namespace MdTZ
{
    public class GPConstants
    {
        //s_sh000001,s_sz399001,s_sz399006
        public const string SH_CODE = "sh000001";
        public const string SZ_CODE = "sz399001";
        public const string CYB_CODE = "sz39900";
        public const string DP_ALL_CODES = "sh000001,sz399001,sz39900";


        public const int EVENT_REFSH_YW= 0; // 刷新新闻
        public const int EVENT_TRANS = 1; //交易事件     


        public const double DP_REAL_ZHANGFU_DOWN = 0.03; //低位
        public const double DP_REAL_ZHANGFU_UP = 0.1; //高位	

    }

}
