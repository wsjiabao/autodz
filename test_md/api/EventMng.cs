using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace MdTZ
{
    class EventMng
    {

        #region 大盘时候涨幅转换事件

        /***
         *  大盘指数涨跌幅事件
         * **/
        public static bool isDpZsChg(DaoPan dp)
        {
            double real_zf = dp.real_zf;

            bool isChg = false;
            if (GPTotalAPI.dpMap.ContainsKey(dp.code))
            {
                DaoPan old_dp = GPTotalAPI.dpMap[dp.code];
                Console.WriteLine(DateTime.Now.ToString() + "[old_dp.real_zf]:" + old_dp.real_zf + ",real_zf:" + real_zf);
                if (old_dp.real_zf >= 0 && real_zf < 0)
                {
                    isChg = true;
                }
                else if (old_dp.real_zf <= 0 && real_zf > 0)
                {
                    isChg = true;
                }
            }

            // 趋势改变
            if (isChg)
            {
                Console.WriteLine(DateTime.Now.ToString() + "[isDpZsChg.real_zf]:" + real_zf + ",isChg:" + isChg);
                if (Math.Abs(real_zf) >= GPConstants.DP_REAL_ZHANGFU_DOWN && Math.Abs(real_zf) <= GPConstants.DP_REAL_ZHANGFU_UP)
                {
                    return true;
                }
            }


            return false;
        }


        /**
        * 大盘实时与个股实时比较
        **/
        public static List<string> getStrongGps(DaoPan dp, List<GuoPiao> gps)
        {
            List<string> codes = new List<string>();
            if (gps == null)
            {
                return codes;
            }
          
            foreach (GuoPiao gp in gps)
            {
                if (gp.real_zf > dp.real_zf)
                {
                    codes.Add(gp.code);
                    updGpTotal(gp.code, 1, 0);
                } else
                {
                    updGpTotal(gp.code, 0, 1);
                }
            }
            
            return codes;
        }       

        #endregion


        #region 辅助方法
      
        /**
       * 放入实时信息
       * */
        public static void putOnLineMap(List<GuoPiao> gps)
        {
            GpTotal gptotal = null;

            foreach (GuoPiao gp in gps)
            {
                /**
               * 统计信息放入内存
               * */
                if (!GPTotalAPI.gpMap.ContainsKey(gp.code))
                {
                    gptotal = new GpTotal();
                    gptotal.code = gp.code;
                    gptotal.lastPrice = gp.dqj;
                    gptotal.dqj = gp.dqj;
                    gptotal.jj = gp.jj;
                    gptotal.zf = gp.zf;
                    gptotal.zhengf = gp.zhengf;
                    GPTotalAPI.gpMap.Add(gp.code, gptotal);
                }
                else
                {
                    gptotal = GPTotalAPI.gpMap[gp.code];
                    if (gptotal.lastPrice != gp.dqj)
                    {
                        //周期内涨幅
                        gptotal.interZf = Math.Round(((gp.dqj - gptotal.lastPrice) / gptotal.lastPrice) * 100, 2);
                    }
                    else
                    {
                        gptotal.interZf = 0;
                    }
                    gptotal.lastPrice = gp.dqj;
                    gptotal.dqj = gp.dqj;
                    gptotal.jj = gp.jj;
                    gptotal.zf = gp.zf;
                    gptotal.zhengf = gp.zhengf;
                }
            }
           
        }

        /**
         * 放入实时信息
         * */
        public static void updGpTotal(string code,int s_cnt,int w_cnt)
        {
            GpTotal gptotal = null;
            if (!GPTotalAPI.gpMap.ContainsKey(code))
            {
                gptotal = new GpTotal();
                gptotal.strongCnt += s_cnt;
                gptotal.weekCnt += w_cnt;
                GPTotalAPI.gpMap.Add(code, gptotal);
            }
            else
            {
                gptotal = GPTotalAPI.gpMap[code];
                gptotal.strongCnt += s_cnt;
                gptotal.weekCnt += w_cnt;
            }
        }

        /**
         * 放入实时信息
         * */
        public static void putOnLineDPMap(DaoPan dp)
        {
            DaoPan tmp_dap = null;

            /**
              * 统计信息放入内存
             * */
            if (!GPTotalAPI.dpMap.ContainsKey(dp.code))
            {
                tmp_dap = new DaoPan();
                tmp_dap.code = dp.code;
                tmp_dap.cje = dp.cje;
                tmp_dap.cjl = dp.cjl;
                tmp_dap.name = dp.name;
                tmp_dap.zs = dp.zs;
                tmp_dap.zdl = dp.zdl;
                tmp_dap.zds = dp.zds;
                tmp_dap.real_zf = dp.real_zf;
                GPTotalAPI.dpMap.Add(dp.code, tmp_dap);
            }
            else
            {
                tmp_dap = GPTotalAPI.dpMap[dp.code];
                tmp_dap.code = dp.code;
                tmp_dap.cje = dp.cje;
                tmp_dap.cjl = dp.cjl;
                tmp_dap.name = dp.name;
                tmp_dap.zs = dp.zs;
                tmp_dap.zdl = dp.zdl;
                tmp_dap.real_zf = dp.real_zf;
                tmp_dap.zds = dp.zds;
            }
        }
        #endregion

    }
}
