using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;

using System.Threading;
using System.Windows.Forms;
using System.Data;
using MySql.Data.MySqlClient;


namespace MdTZ
{
    /**
     * 
     * 股票统计
     * */
    public class GPTotalAPI
    {
        /**
         * 股票内存
         * */
        public static Dictionary<String, GpTotal> gpMap = new Dictionary<String, GpTotal>();

        /**
           * 大盘内存
           * */
        public static Dictionary<String, DaoPan> dpMap = new Dictionary<String, DaoPan>();

        /**
        * 分时统计
        * */
        public static void gpZfRecord(string codes)
        {           
            //清空表                   
            char[] ch = new char[] { ',' };
            string[] gpcodeList = codes.Split(ch);

            foreach (string code in gpcodeList)
            {
                GPUtil.helper.ExecuteProcNoOut("CALL zf_record('" + code + "')");
                GPUtil.helper.ExecuteProcNoOut("CALL cjl_record('" + code + "')");
            }
        }


        /// <summary>
        /// 分时价格成交占比图
        /// </summary>
        /// <param name="codes"></param>
        public static void gpJgZbTotal(string codes)
        {
            //清空表                   
            char[] ch = new char[] { ',' };
            string[] gpcodeList = codes.Split(ch);

            foreach (string code in gpcodeList)
            {
                GPUtil.helper.ExecuteProcNoOut("CALL jg_time_total('"+ code+"')");
            }
    
        }


        /// <summary>
        /// 分时波段统计
        /// </summary>
        /// <param name="codes"></param>
        public static void bdTimeTotal(string codes)
        {
            //清空表                   
            char[] ch = new char[] { ',' };
            string[] gpcodeList = codes.Split(ch);

            foreach (string code in gpcodeList)
            {
                GPUtil.helper.ExecuteProcNoOut("CALL bd_time_total('" + code + "')");
            }

        }

        /// <summary>
        /// 分时均线统计
        /// </summary>
        /// <param name="codes"></param>
        public static void jxTimeTotal(string codes)
        {
            //清空表                   
            char[] ch = new char[] { ',' };
            string[] gpcodeList = codes.Split(ch);

            foreach (string code in gpcodeList)
            {
                GPUtil.helper.ExecuteProcNoOut("CALL jx_time_total('" + code + "')");
            }

        }

        /**
         * 更新统计5,10 均价， 5,10 涨幅
         * */
        public static void yaohHisDataTotal(string codes)
        {
            //清空表                   
            char[] ch = new char[] { ',' };
            string[] gpcodeList = codes.Split(ch);

            foreach (string code in gpcodeList)
            {
                GPUtil.helper.ExecuteProcNoOut("CALL yaoh_total_his('" + code + "')");
            }
        }


    }


}
