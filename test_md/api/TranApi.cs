using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace MdTZ
{
    class TranApi
    {

        public static bool isTraning = false;

        //窗口状态
        public static string winSta = "";

        public static List<string> buyCodes = new List<string>();     
        public static List<string> sellCodes = new List<string>();

        public static List<GpTranBean> buyedCodes = new List<GpTranBean>();
        public static List<GpTranBean> selledCodes = new List<GpTranBean>();

        //交易时间-交易次数
        public static Dictionary<string, int> tranTimes = new Dictionary<string, int>{
             {"9.35-9.50",1},{"14.30-14.50",1},{"20.20-22.30",1}
        };

        //public static Dictionary<string, int> tranTimes = new Dictionary<string, int>();

        public static string tranPlan(string time)
        {
            string a = time.Replace(":", ".");
            return a.Substring(0, a.LastIndexOf('.'));
        }

        /// <summary>
        /// 获取交易计划
        /// </summary>
        public static void loadTranPlan()
        {
            string type = GPUtil.helper.ExecuteDataRow("select qsnow from gpparam")["qsnow"].ToString();
            DataTable ts = GPUtil.helper.ExecuteDataTable("select btime,etime,cnt from tranplan where type='" + type + "' order by btime");
            foreach(DataRow r in ts.Rows) {
                tranTimes.Add(tranPlan(r["btime"].ToString()) + "-" + tranPlan(r["etime"].ToString()), Convert.ToInt16(r["cnt"]));
            }
        }


        /// <summary>
        /// 获取可用金额
        /// </summary>
        /// <returns></returns>
        public static double getCanBuyAmt()
        {
            DataRow row = GPUtil.helper.ExecuteDataRow("SELECT IFNULL(s.zjye,0) * (d.cw/100) je FROM dpqs d,gpparam p,store s WHERE d.type=p.dpqs AND p.code=s.code");
            double je = 0;
            if (row == null)
            {
                return 0;
            }
            if (row["je"] != null && !string.IsNullOrEmpty(row["je"].ToString()))
            {
                je = Convert.ToDouble(row["je"]);
            }           
            return je;
        }

        /// <summary>
        /// 获取当前仓位可买股票数
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        public static double[] getCanBuyNums(string code)
        {
            double[] rntValue = new double[2];
            GuoPiao gp = SinaAPI.getGPList(code)[0];
            double amt = getCanBuyAmt();

            int ss = 0;
            if (gp!=null && gp.s2_price > 0)
            {
                ss = Convert.ToInt16(amt / gp.s2_price / 100);   
            }

            if (ss <= 0)
            {
                ss = 1;
            }
            rntValue[0] = ss;
            rntValue[1] = gp.s2_price;
            return rntValue;
        }

        public static string getTransCode(string code)
        {
            return code.Replace("sh", "").Replace("sz", "");
        }

        /// <summary>
        /// 
        /// 交易时间控制
        /// </summary>
        /// <returns></returns>
        public static int getTranTimeNow()
        {
            double nowTime = Convert.ToDouble(DateTime.Now.Hour.ToString() + "." + DateTime.Now.Minute.ToString());
            double b_time = 0;
            double e_time = 0;
            int rtn = 0;
            foreach (string t_ran in tranTimes.Keys)
            {
                b_time = Convert.ToDouble(t_ran.Split("-".ToCharArray())[0]);
                e_time = Convert.ToDouble(t_ran.Split("-".ToCharArray())[1]);

                if (nowTime >= b_time && nowTime <= e_time)
                {
                    rtn  = tranTimes[t_ran];
                    tranTimes[t_ran] = rtn - 1;
                   
                    return rtn;
                }

            }
            return -1;
        }

  
        public static void add_buys_codes()
        {
           
            TranApi.buyCodes.Clear();

            DataTable codes = GPUtil.helper.ExecuteDataTable("select b.code from gpbuy b where b.flag = 0 order by date desc");
            string code;
            foreach (DataRow r in codes.Rows)
            {
                code = r["code"].ToString();
                TranApi.buyCodes.Add(code);               
            }

        }


        public static void add_sells_codes()
        {
            TranApi.sellCodes.Clear();

            DataTable codes = GPUtil.helper.ExecuteDataTable("select code from gpsell where flag = 0");
            string code = "";
            foreach (DataRow r in codes.Rows)
            {
                code = r["code"].ToString();
                TranApi.sellCodes.Add(code);
            }

        }
     

        /// <summary>
        /// 
        /// 检查是否已经买入
        /// </summary>
        /// <returns></returns>
        public static bool check_is_buyed(string code)
        {
            DataRow row = GPUtil.helper.ExecuteDataRow("select count(*) cnt from gpsell where code='" + code + "'");
            if (row != null && Convert.ToInt16(row["cnt"]) > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// 交易开始
        /// </summary>
        /// <returns></returns>
        public static int do_tran_process()
        {

            isTraning = true;

            int rtnValue = 0;
            //先卖出          
            rtnValue = TranApi.do_sell_process_ext();
            Console.WriteLine("do_sell_process.cnt:" + rtnValue);

            //暂停
            //Thread.Sleep(500);

            //买入
            //rtnValue = TranApi.do_buy_process_ext();


            isTraning = false;
            Console.WriteLine("do_buy_process_ext.cnt:" + rtnValue);
            return rtnValue;
        }

        /// <summary>
        /// 发起买入处理
        /// </summary>
        /// <returns></returns>
        public static int do_buy_process_ext()
        {

            double qsz = QuShi.qsz;

            Console.WriteLine("do_buy_process_ext.QuShi.qsz:" + qsz);         
            /**
             * 买入先决条件
             **/
            //if (qsz < 0)
            //{
            //    return -1;
            //}

            string real_code = "";
            string log_str = "";
            int buyCnt = 0;        
            int buyNums = 100;
            double price = 0;
            double[] rtnValue = null;
            if (buyCodes.Count > 0)
            {                                    

                foreach (string code in buyCodes)
                {
                    real_code = getTransCode(code);                  
                  
                    //不在时间计划内
                    if (TranApi.getTranTimeNow() <= 0)
                    {                      
                        break;
                    }                                    

                    //根据当前仓位获取股票数 
                    try
                    {
                        rtnValue = getCanBuyNums(code);
                        buyNums = Convert.ToInt16(rtnValue[0]);
                        price = rtnValue[1];
                    }
                    catch (Exception e)
                    {
                        GPUtil.write(e.Message);
                    }

                    GPUtil.write("开始买入:" + real_code);
                                      
                    //发起交易
                    //THSAPI.buyIn(real_code, THSAPI.PRICE_OPT_SELL_2, 0, THSAPI.NUM_OPT_INPUT, 1 * 100); 
                    ZXApi.buyIn(real_code, THSAPI.PRICE_OPT_SELL_2, 0, THSAPI.NUM_OPT_INPUT, 1 * 100);                  

                    //更新交易数据                 
                    GPUtil.helper.ExecuteNonQuery("INSERT INTO gpsell (CODE,cbj,num,flag,buydate,DATE) values ('" + code + "',"
                         + price + "," + buyNums * 100 + ",0,'" + DateTime.Now + "','" + DateTime.Now + "')");

                    log_str = "结束买入[" + real_code + "] 数量:" + buyNums * 100 + " 价格:" + price;
                    Console.WriteLine(log_str);
                    GPUtil.write(log_str);

                    buyCnt++;

                    //暂停
                    Thread.Sleep(300);                                                                                    
                }
            }

            return buyCnt;
        }

        public static int do_sell_process_ext()
        {

            double qsz = QuShi.qsz;
            qsz = -1;

            Console.WriteLine("do_sell_process_ext.checkValue:" + qsz);
            GPUtil.write("do_sell_process_ext.checkValue##########:" + qsz);

            int rtnSells = 0;

            /**
             * 卖出先决条件
             **/
            if (qsz <= 0)
            {
                    
                string real_code = "";              
                string log_str = "";              
                double price = 0;      
                foreach (string code in TranApi.sellCodes)
                    {                       
                        real_code = getTransCode(code);
                        log_str = "开始卖出[" + code  + "]";
                        Console.WriteLine(log_str);                    
                        GPUtil.write(log_str);              

                        //不在时间计划内
                        if (TranApi.getTranTimeNow() <= 0)
                        {
                            break;
                        }                       

                        //方法测试
                        //THSAPI.sellOut(real_code, THSAPI.PRICE_OPT_BUY_2, 0, THSAPI.NUM_OPT_INPUT, 100);  
                        ZXApi.sellOut(real_code, THSAPI.PRICE_OPT_BUY_2, 0, THSAPI.NUM_OPT_INPUT, 100);                       

                        //更新交易数据                 
                        GPUtil.helper.ExecuteNonQuery("update gpsell set num = num-100 where code='" + code + "'");

                        log_str = "结束卖出[" + real_code + "] 数量:" + 1 * 100;
                        GPUtil.write(log_str);  

                        rtnSells++;

                        //暂停
                        Thread.Sleep(300);
                    }               

                }


            return rtnSells;
        }




        /// <summary>
        /// 更新买入，卖出后状态
        /// </summary>
        public static void synBuyOrSellStatus()
        {
     
            foreach(GpTranBean tran in buyedCodes) {
                //更新状态
                GPUtil.helper.ExecuteNonQuery("update gpbuy set flag=1 where code='" + tran.code  + "'");
                GPUtil.helper.ExecuteNonQuery("INSERT INTO gpsell (CODE,cbj,num,flag,buydate,DATE) values ('"+tran.code+"',"
                    +tran.price+","+tran.nums+",0,'"+tran.date+"','"+GPUtil.nowTranDate+"')");  
            }

            foreach (GpTranBean tran in selledCodes)
            {
                //更新状态
                GPUtil.helper.ExecuteNonQuery("update gpsell set flag=1 where code='" + tran.code + "'");
            }
        }
    }
}
