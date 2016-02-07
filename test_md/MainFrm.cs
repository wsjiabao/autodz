using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using System.IO;
using System.Threading;
using GMSDK;

namespace MdTZ
{
    public partial class MainFrm : Form
    {
        public MainFrm()
        {
            InitializeComponent();
        }

        /**
         * 初始化股票代码
         * */
      
        private static List<string> codeList = new List<string>();
        private static List<string> loadCodeList = new List<string>();
       
        private static List<string> urlList = new List<string>();
        private static List<string> url_dg_List = new List<string>();
        private static List<string> loadUrlList = new List<string>();

        private static List<string> gg_urlList = null;
        private static List<string> loadGGUrlList = new List<string>();


        private static string dpCodes = "s_sh000001,s_sz399001,s_sz399006";

        //交易次数
        private static int tran_cnt = 0;
        private static List<GuoPiao> tran_gps = null;
             
                     
        private void buyIn_Click(object sender, EventArgs e)
        {
            //方法测试
            //THSAPI.buyIn("000001", THSAPI.PRICE_OPT_NOW, 0, THSAPI.NUM_OPT_ALL, 0);
            //ZXApi.buyIn("600007", THSAPI.PRICE_OPT_SELL_2, 0, THSAPI.NUM_OPT_INPUT, 100);
            ZXApi.buyIn_cur("601288", THSAPI.NUM_OPT_INPUT, 100);
        }

        private void buyOut_Click(object sender, EventArgs e)
        {
            //方法测试
            //THSAPI.sellOut("600007", THSAPI.PRICE_OPT_BUY_2,0, THSAPI.NUM_OPT_INPUT, 100);
            //ZXApi.sellOut("601288", THSAPI.PRICE_OPT_BUY_2, 0, THSAPI.NUM_OPT_INPUT, 100, 1000);
            ZXApi.sellOut_cur("601288", THSAPI.NUM_OPT_INPUT, 100);
        }

        private void storeSell_Click(object sender, EventArgs e)
        {
            THSAPI.storeSell(THSAPI.PRICE_OPT_NOW, 0, THSAPI.NUM_OPT_ALL, 100);
        }

        private void cancelOrder_Click(object sender, EventArgs e)
        {
            //THSAPI.agentCancel("000540", THSAPI.CANCEL_OPT_FRIST);
            ZXApi.agentCancel("000725", THSAPI.CANCEL_OPT_FRIST);
        }
     

        private void start_Click(object sender, EventArgs e)
        {
            get_filter_timer.Enabled = true;
            get_filter_timer.Start();                                       
        }       

        #region 事件处理方法

        //定义股票处理事件
        public static event EventHandler<EventArgs> gpOnEvent;
        public static Delegate[] gpDelegates;

        //获取要闻新闻
        public static void getYwNewHander(object sender, EventArgs e)
        {

            WebBrowser web = (WebBrowser)sender;
            Console.WriteLine("获取要闻数据开始..");
            urlList.Clear();
            urlList.Add("http://news.10jqka.com.cn/yaowen/");
            urlList.Add("http://www.10jqka.com.cn/");
            urlList.Add("http://stock.10jqka.com.cn/");        
        
            loadCodeList = new List<string>();
            loadUrlList = new List<string>();

            NewsApi.clearYwData();
            if (urlList != null && urlList.Count > 0)
            {
                web.Navigate(urlList[0]);
            }
        }

        //强势个股列表
        public static void gpBuysTransHander(object sender, EventArgs e)
        {
            Console.WriteLine(DateTime.Now.ToString() + "[gpBuysTransHander]");
            List<GuoPiao> gps = (List<GuoPiao>)sender;
            TranApi.do_buy_process(StaUtil.getTransBean(gps,"dp"), StaUtil.getTransBean(gps,"buy"));                  
        }

        //强势个股列表
        public static void gpSellsTransHander(object sender, EventArgs e)
        {
            Console.WriteLine(DateTime.Now.ToString() + "[gpSellTransHander]");
            List<GuoPiao> gps = (List<GuoPiao>)sender;
            TranApi.do_sell_process(StaUtil.getTransBean(gps, "dp"), StaUtil.getTransBean(gps, "sell"));  
        }    

        #endregion

        private void MainFrm_Load(object sender, EventArgs e)
        {

            output("01-27-4版本这是");

            //设置当前交易日
            GPUtil.setTodayTranDay();

            //加载计划
            //TranApi.loadTranPlan();

            //财务初始化用
            codeList = HisDataAPI.getLoadCwInfoCodes();
            //全量股票列表
            GPUtil.codes = HisDataAPI.getGpCodesFromDb();

            //将Method1和Method2注册到事件中
            gpOnEvent += new EventHandler<EventArgs>(getYwNewHander);
            gpOnEvent += new EventHandler<EventArgs>(gpBuysTransHander);
            gpOnEvent += new EventHandler<EventArgs>(gpSellsTransHander);
            gpDelegates = gpOnEvent.GetInvocationList();

            output("初始化数据开始..");
            //初始化股票信息 
            HisDataAPI.saveTodayHisDataFromSina();
           
            //初始化下外汇          
            HisDataAPI.refreshRMBLast();          

            //关键字
            KeyWordAPI.loadKeyWord();

            QuShi.qsz = QuShi.getQuShiFS();
            output("趋势值:" + QuShi.qsz);        

            //更新到买入队列
            StaUtil.add_buys_codes();
            StaUtil.add_sells_codes();          
                                                        
        }

        private void stop_Click(object sender, EventArgs e)
        {
            get_filter_timer.Enabled = false;
            get_filter_timer.Stop();         
           
        }

        private void forTest_Click(object sender, EventArgs e)
        {
            //MySqlParameter lev_param = new MySqlParameter("?lev", MySqlDbType.VarChar, 1);
            //lev_param.Value = '7';
            //MySqlParameter[] parms = new MySqlParameter[] { lev_param };
            //DataSet ds = GPUtil.helper.ExecuteProc("gp_sort", parms);

            //DataTable result  = ds.Tables[0];
            //Console.WriteLine(result.Rows.Count.ToString());

            //分时数据导入测试
            //RealDataApi.loadTimeHis();

           // JsonApi.getJuheCurrencyRmbQuot();  

           // Console.WriteLine("get sina code:" + SinaAPI.getGPDataFromSina("sh600195"));

            //Console.WriteLine(TranApi.tranPlan("09:35:00"));

            //double rmbRate = JsonApi.getRMBRate();

           // MdQry.GetLastBars("SHSE.601318", 30);

            Console.Write(" RegexDao.isDpContionDown():" + RegexDao.isDpContionDown());
        }

        public void output(string log)
        {
            //长度超过100清空
            if (txt_log.GetLineFromCharIndex(txt_log.Text.Length) > 1000)
            {
                txt_log.Text = "";
            }

            //添加日志
            txt_log.AppendText(DateTime.Now.ToString("HH:mm:ss  ") + log + "\r\n");
        }

        public void write(string msg)
        {
            //当前程序目录
            string logPath = Path.GetDirectoryName(Application.ExecutablePath);
            //新建文件
            System.IO.StreamWriter sw = System.IO.File.AppendText(logPath + "/日志.txt");
            sw.WriteLine(DateTime.Now.ToString("HH:mm:ss  ") + msg);

            sw.Close();
            sw.Dispose();

        }

        private void txt_log_DoubleClick(object sender, EventArgs e)
        {
            txt_log.Text = "";
        }

        private void gp_web_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
        {

            string url = gp_web.Url.ToString();
                      
            if (String.IsNullOrEmpty(url) || url.Equals("about:blank"))
            {
                return;
            }

            Console.WriteLine("url:" + url);

            if (!string.IsNullOrEmpty(cb_new_type.Text) && !string.IsNullOrEmpty(txt_url.Text))
            {
                if (!loadUrlList.Contains(url))
                {
                    loadUrlList.Add(url);
                    NewsApi.processNewsUrl(gp_web, url, cb_new_type.Text);
                    output("新闻后处理");
                    NewsApi.afterLoadProcess();
                    output("新闻后处理结束");
                    output("获取单个新闻[" + url + "]结束");
                }
            }           
            else if (url.IndexOf("公告") != -1 || url.IndexOf("投资日历") != -1)
            {
                if (!loadGGUrlList.Contains(url))
                {
                    loadGGUrlList.Add(url);
                    NewsApi.processNewsUrl(gp_web, url,cb_new_type.Text);

                    if (loadGGUrlList.Count < gg_urlList.Count)
                    {
                        Console.WriteLine("gg_urlList.count:" + gg_urlList.Count + ",loadGGUrlList.Count:" + loadGGUrlList.Count);
                        output("获取公告数据结束..");
                        gp_web.Navigate(gg_urlList[loadGGUrlList.Count]);
                    }                                      
                }
            }
            else if (url.IndexOf("http://vip.stock.finance.sina.com.cn/corp/go.php") != -1)
            {
                //获取财务数据回调
                SinaAPI.webGetCwCallBack(gp_web, codeList, loadCodeList);
                output("获取财务数据结束..");
            }
            else
            {
                if (!loadUrlList.Contains(url))
                {
                    loadUrlList.Add(url);
                    List<string> rtnUrlList = NewsApi.processNewsUrl(gp_web, url, cb_new_type.Text);
                    urlList.AddRange(rtnUrlList);

                    if (loadUrlList.Count < urlList.Count)
                    {
                        Console.WriteLine("urlList.count:" + urlList.Count + ",loadCodeList.Count:" + loadCodeList.Count);
                        gp_web.Navigate(urlList[loadUrlList.Count]);
                    }
                    else
                    {
                        output("新闻后处理");
                        NewsApi.afterLoadProcess();
                        output("新闻后处理结束");                      
                    }

                    output("获取新闻[" + url + "]结束");
                }
            }
            

        }

        private void 新闻获取ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            output("获取新闻数据开始..");

            loadCodeList = new List<string>();     
            loadUrlList = new List<string>();

            string localUrl = HtmlUtl.GetToLocalHtml("http://stock.10jqka.com.cn/fincalendar.shtml#"
               + DateTime.Now.ToShortDateString(), "d:\\tzrl.html");
            urlList.Add(localUrl);

            urlList.Add("http://stock.10jqka.com.cn/fupan/");
            urlList.Add("http://yuanchuang.10jqka.com.cn/qingbao//");
            urlList.Add("http://yuanchuang.10jqka.com.cn/zhangting/");

            urlList.Add("http://stock.10jqka.com.cn/zaopan/");

            urlList.Add("http://news.10jqka.com.cn/yaowen/");
            urlList.Add("http://www.10jqka.com.cn/");
            urlList.Add("http://stock.10jqka.com.cn/");


            urlList.Add("http://data.10jqka.com.cn/financial/yjyg/");
            urlList.Add("http://data.10jqka.com.cn/ipo/syg/");
            urlList.Add("http://data.10jqka.com.cn/financial/sgpx/");
            urlList.Add("http://data.10jqka.com.cn/financial/yjkb/");
            urlList.Add("http://data.10jqka.com.cn/financial/yypl/");
            urlList.Add("http://data.10jqka.com.cn/financial/ggjy/");
            urlList.Add("http://data.10jqka.com.cn/market/dzjy/");
            urlList.Add("http://data.10jqka.com.cn/market/longhu/");
            urlList.Add("http://data.10jqka.com.cn/market/xsjj/");

            //urlList.Add("http://data.10jqka.com.cn/market/ggsd/");
            urlList.Add("http://data.10jqka.com.cn/financial/xzjp/");

            urlList.Add("http://data.10jqka.com.cn/rank/cxg/");
            urlList.Add("http://data.10jqka.com.cn/rank/cxd/");
            //urlList.Add("http://data.10jqka.com.cn/rank/lxsz/");
            //urlList.Add("http://data.10jqka.com.cn/rank/cxfl/");
            //urlList.Add("http://data.10jqka.com.cn/rank/xstp/");
            //urlList.Add("http://data.10jqka.com.cn/rank/ljqs/");           
            //urlList.Add("http://data.10jqka.com.cn/rank/lxxd/");
            //urlList.Add("http://data.10jqka.com.cn/rank/cxsl/");
            //urlList.Add("http://data.10jqka.com.cn/rank/xxtp/");
            //urlList.Add("http://data.10jqka.com.cn/rank/ljqd/");

            urlList.Add("http://data.10jqka.com.cn/ipo/xgsgyzq/");
            urlList.Add("http://data.10jqka.com.cn/funds/gnzjl/");
            urlList.Add("http://data.10jqka.com.cn/funds/hyzjl/");
            urlList.Add("http://data.10jqka.com.cn/funds/ddzz/");
            urlList.Add("http://news.10jqka.com.cn/today_list/");    

            NewsApi.clearData();
            if (urlList != null && urlList.Count > 0)
            {
                gp_web.Navigate(urlList[0]);
            }
        }

        private void 财务数据ToolStripMenuItem_Click(object sender, EventArgs e)
        {            
            output("获取财务数据开始..");
            gp_web.Navigate("http://vip.stock.finance.sina.com.cn/corp/go.php/vFD_FinanceSummary/stockid/600000/displaytype/4.phtml");
        }

        private void 个股公告ToolStripMenuItem_Click(object sender, EventArgs e)
        {

            output("获取公告数据开始..");
            gg_urlList = new List<string>();
            loadGGUrlList = new List<string>();
           
            gg_urlList.Add("d:/公告/投资日历.html");

            gg_urlList.Add("d:/公告/最新公告1.html");
            gg_urlList.Add("d:/公告/最新公告2.html");
            gg_urlList.Add("d:/公告/最新公告3.html");
            gg_urlList.Add("d:/公告/最新公告4.html");
            gg_urlList.Add("d:/公告/最新公告5.html");
            gg_urlList.Add("d:/公告/最新公告6.html");

            gg_urlList.Add("d:/公告/利好公告1.html");
            gg_urlList.Add("d:/公告/利好公告2.html");
            gg_urlList.Add("d:/公告/利好公告3.html");
            gg_urlList.Add("d:/公告/利好公告4.html");
            gg_urlList.Add("d:/公告/利好公告5.html");

            gg_urlList.Add("d:/公告/利空公告1.html");
            gg_urlList.Add("d:/公告/利空公告2.html");
            gg_urlList.Add("d:/公告/利空公告3.html");
            gg_urlList.Add("d:/公告/利空公告4.html");
            gg_urlList.Add("d:/公告/利空公告5.html");

            NewsApi.clearGGData();
            if (gg_urlList != null && gg_urlList.Count > 0)
            {
                gp_web.Navigate(gg_urlList[0]);
            }
           
        }

        private void 新闻后处理ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            output("新闻后处理");
            NewsApi.afterLoadProcess();
            output("新闻后处理结束");
        }

        private void 新闻分析ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            url_dg_List = new List<string>();
            loadCodeList = new List<string>();
            loadUrlList = new List<string>();

            loadUrlList.Remove(txt_url.Text);

            if (!string.IsNullOrEmpty(txt_url.Text.ToString()) &&
                !string.IsNullOrEmpty(cb_new_type.Text))
            {
                url_dg_List.Add(txt_url.Text.ToString());
            }

            if (url_dg_List != null && url_dg_List.Count > 0)
            {
                gp_web.Navigate(url_dg_List[0]);
            }
        }

        private void 保存到自选股ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            GpSelectUtl.gpSelfSel("add");
        }

        private void 删除自选股ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            GpSelectUtl.gpSelfSel("del");
        }

        private void 最大化ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //THSAPI.activeTranWin("max");
            ZXApi.refreshWin();
        }

        private void 最小化ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            THSAPI.activeTranWin("min");
        }


        private void 要闻刷新ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            output("获取要闻数据开始..");
            urlList.Clear();
            urlList.Add("http://news.10jqka.com.cn/yaowen/");
            urlList.Add("http://www.10jqka.com.cn/");
            urlList.Add("http://stock.10jqka.com.cn/");

            loadCodeList = new List<string>();
            loadUrlList = new List<string>();

            NewsApi.clearYwData();
            if (urlList != null && urlList.Count > 0)
            {
                gp_web.Navigate(urlList[0]);
            }
        }

       
        #region 定时器

        private void get_filter_timer_Tick(object sender, EventArgs e)
        {

            //初始化股票信息 
            if (DateTime.Now.Hour >= 16)
            {
                HisDataAPI.saveTodayHisDataFromSina();
            }

            //output("###初始化下外汇开始##");
            ////初始化下外汇          
            ////HisDataAPI.refreshRMB();
            //output("###结束初始化下外汇 ##");

            //output("###刷新要闻信息##");
            //EventHandler<EventArgs> gpEvent = (EventHandler<EventArgs>)gpDelegates[GPConstants.EVENT_REFSH_YW];
            //gpEvent.BeginInvoke(gp_web, EventArgs.Empty, null, null);

            //QuShi.qsz = QuShi.getQuShiFS();
            //output("趋势值:" + QuShi.qsz);

            //更新到买入队列
            //TranApi.add_buys_codes();
            //TranApi.add_sells_codes();

            ////更新交易状态
            //TranApi.synBuyOrSellStatus();        

            //List<DaoPan> dpList = SinaAPI.getDPList("a");
            //DaoPan sh_dp = dpList[0];
            //Console.WriteLine(DateTime.Now.ToString() + "[sh_dp.zdf]:" + sh_dp.zs + "," + sh_dp.zdl);

        }

        private void gp_buys_Time_Tick(object sender, EventArgs e)
        {        
            tran_cnt++;
            output("开始买入趋势值:" + QuShi.qsz);
            //获取TICK 数据          
            try
            {
                tran_gps = SinaAPI.getGPList(TranApi.tickCodes);
                if (tran_gps != null)
                {
                    output("####开始交易交易标的数gps.size:" + tran_gps.Count);
                }               
            }
            catch (Exception ee)
            {
                output(ee.Message);      
            }                                 

            //刷新交易开始           
            if (GPUtil.isTranTime() && !TranApi.isTraning)
            {
                //10次刷新一次
                Console.WriteLine("trans.cn {0}", tran_cnt);
                if (tran_cnt % 10 == 0)
                {                  
                    TranApi.isTraning = true;
                    //方法测试
                    ZXApi.refreshWin();
                    TranApi.isTraning = false;
                }               
            }            

            //交易事件触发
            if (GPUtil.isTranTime() && !TranApi.isTraning)
            {
                EventHandler<EventArgs> tranEvent = (EventHandler<EventArgs>)gpDelegates[GPConstants.EVENT_BUY_TRANS];
                tranEvent.BeginInvoke(tran_gps, EventArgs.Empty, null, null);
                 
            }
            output("###结束交易##");      
        }

        private void gp_sells_timer_Tick(object sender, EventArgs e)
        {           
            output("开市卖出趋势值:" + QuShi.qsz);

            if (tran_gps == null || tran_gps.Count == 0)
            {
                return;
            }

            //交易事件触发
            if (GPUtil.isTranTime() && !TranApi.isTraning)
            {
                //EventHandler<EventArgs> tranEvent = (EventHandler<EventArgs>)gpDelegates[GPConstants.EVENT_SELL_TRANS];
                //tranEvent.BeginInvoke(tran_gps, EventArgs.Empty, null, null);

            }
            output("###结束交易##");   
        }

        #endregion

        private void 交易启动ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            gp_buys_Time.Enabled = true;
            gp_sells_timer.Enabled = true;
        }

        private void 交易停止ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            gp_buys_Time.Enabled = false;
            gp_buys_Time.Stop();

            gp_sells_timer.Enabled = false;
            gp_sells_timer.Stop();
        }

        private void 退出程序ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            System.Environment.Exit(0);
        }

        private void 测试窗口ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form4 f4 = new Form4();
            f4.Show();
        }

        private void 演示策略ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            StrategySimple test = new StrategySimple();

            int ret = MdComm.setStrategyInit(test, 3, "000001,002027", "strategy_1", null);
            MdComm.setStrategyBackTestInit(test, "2015-12-25 09:30:00", "2016-1-29 15:15:00", 1000000);

            System.Console.WriteLine("init: {0}", ret);

            ret = test.Run();

            if (ret != 0)
            {
                System.Console.WriteLine("run error: {0}", ret);
            }

        }

 
       
    }
}