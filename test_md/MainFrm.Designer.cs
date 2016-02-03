namespace MdTZ
{
    partial class MainFrm
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.历史数据ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.新闻分析ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.要闻刷新ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.退出程序ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.实时数据ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.财务数据ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.forTest = new System.Windows.Forms.ToolStripMenuItem();
            this.新闻获取ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.个股公告ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.新闻后处理ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.保存到自选股ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.删除自选股ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.交易ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.buyIn = new System.Windows.Forms.ToolStripMenuItem();
            this.buyOut = new System.Windows.Forms.ToolStripMenuItem();
            this.cancelOrder = new System.Windows.Forms.ToolStripMenuItem();
            this.storeSell = new System.Windows.Forms.ToolStripMenuItem();
            this.最大化ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.最小化ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.测试窗口ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.mainPro = new System.Windows.Forms.ToolStripMenuItem();
            this.start = new System.Windows.Forms.ToolStripMenuItem();
            this.stop = new System.Windows.Forms.ToolStripMenuItem();
            this.交易启动ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.交易停止ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.策略测试ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.演示策略ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.gp_buys_Time = new System.Windows.Forms.Timer(this.components);
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.gp_web = new System.Windows.Forms.WebBrowser();
            this.txt_log = new System.Windows.Forms.TextBox();
            this.txt_url = new System.Windows.Forms.TextBox();
            this.cb_new_type = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.get_filter_timer = new System.Windows.Forms.Timer(this.components);
            this.gp_sells_timer = new System.Windows.Forms.Timer(this.components);
            this.menuStrip1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.历史数据ToolStripMenuItem,
            this.实时数据ToolStripMenuItem,
            this.交易ToolStripMenuItem,
            this.mainPro,
            this.策略测试ToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(471, 25);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // 历史数据ToolStripMenuItem
            // 
            this.历史数据ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripSeparator1,
            this.新闻分析ToolStripMenuItem,
            this.要闻刷新ToolStripMenuItem,
            this.退出程序ToolStripMenuItem});
            this.历史数据ToolStripMenuItem.Name = "历史数据ToolStripMenuItem";
            this.历史数据ToolStripMenuItem.Size = new System.Drawing.Size(68, 21);
            this.历史数据ToolStripMenuItem.Text = "历史数据";
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(149, 6);
            // 
            // 新闻分析ToolStripMenuItem
            // 
            this.新闻分析ToolStripMenuItem.Name = "新闻分析ToolStripMenuItem";
            this.新闻分析ToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.新闻分析ToolStripMenuItem.Text = "新闻分析";
            this.新闻分析ToolStripMenuItem.Click += new System.EventHandler(this.新闻分析ToolStripMenuItem_Click);
            // 
            // 要闻刷新ToolStripMenuItem
            // 
            this.要闻刷新ToolStripMenuItem.Name = "要闻刷新ToolStripMenuItem";
            this.要闻刷新ToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.要闻刷新ToolStripMenuItem.Text = "要闻刷新";
            this.要闻刷新ToolStripMenuItem.Click += new System.EventHandler(this.要闻刷新ToolStripMenuItem_Click);
            // 
            // 退出程序ToolStripMenuItem
            // 
            this.退出程序ToolStripMenuItem.Name = "退出程序ToolStripMenuItem";
            this.退出程序ToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.退出程序ToolStripMenuItem.Text = "退出程序";
            this.退出程序ToolStripMenuItem.Click += new System.EventHandler(this.退出程序ToolStripMenuItem_Click);
            // 
            // 实时数据ToolStripMenuItem
            // 
            this.实时数据ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.财务数据ToolStripMenuItem,
            this.forTest,
            this.新闻获取ToolStripMenuItem,
            this.个股公告ToolStripMenuItem,
            this.新闻后处理ToolStripMenuItem,
            this.保存到自选股ToolStripMenuItem,
            this.删除自选股ToolStripMenuItem});
            this.实时数据ToolStripMenuItem.Name = "实时数据ToolStripMenuItem";
            this.实时数据ToolStripMenuItem.Size = new System.Drawing.Size(68, 21);
            this.实时数据ToolStripMenuItem.Text = "实时数据";
            // 
            // 财务数据ToolStripMenuItem
            // 
            this.财务数据ToolStripMenuItem.Name = "财务数据ToolStripMenuItem";
            this.财务数据ToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.财务数据ToolStripMenuItem.Text = "财务数据";
            this.财务数据ToolStripMenuItem.Click += new System.EventHandler(this.财务数据ToolStripMenuItem_Click);
            // 
            // forTest
            // 
            this.forTest.Name = "forTest";
            this.forTest.Size = new System.Drawing.Size(152, 22);
            this.forTest.Text = "存测试";
            this.forTest.Click += new System.EventHandler(this.forTest_Click);
            // 
            // 新闻获取ToolStripMenuItem
            // 
            this.新闻获取ToolStripMenuItem.Name = "新闻获取ToolStripMenuItem";
            this.新闻获取ToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.新闻获取ToolStripMenuItem.Text = "新闻获取";
            this.新闻获取ToolStripMenuItem.Click += new System.EventHandler(this.新闻获取ToolStripMenuItem_Click);
            // 
            // 个股公告ToolStripMenuItem
            // 
            this.个股公告ToolStripMenuItem.Name = "个股公告ToolStripMenuItem";
            this.个股公告ToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.个股公告ToolStripMenuItem.Text = "个股公告";
            this.个股公告ToolStripMenuItem.Click += new System.EventHandler(this.个股公告ToolStripMenuItem_Click);
            // 
            // 新闻后处理ToolStripMenuItem
            // 
            this.新闻后处理ToolStripMenuItem.Name = "新闻后处理ToolStripMenuItem";
            this.新闻后处理ToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.新闻后处理ToolStripMenuItem.Text = "新闻后处理";
            this.新闻后处理ToolStripMenuItem.Click += new System.EventHandler(this.新闻后处理ToolStripMenuItem_Click);
            // 
            // 保存到自选股ToolStripMenuItem
            // 
            this.保存到自选股ToolStripMenuItem.Name = "保存到自选股ToolStripMenuItem";
            this.保存到自选股ToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.保存到自选股ToolStripMenuItem.Text = "保存到自选股";
            this.保存到自选股ToolStripMenuItem.Click += new System.EventHandler(this.保存到自选股ToolStripMenuItem_Click);
            // 
            // 删除自选股ToolStripMenuItem
            // 
            this.删除自选股ToolStripMenuItem.Name = "删除自选股ToolStripMenuItem";
            this.删除自选股ToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.删除自选股ToolStripMenuItem.Text = "删除自选股";
            this.删除自选股ToolStripMenuItem.Click += new System.EventHandler(this.删除自选股ToolStripMenuItem_Click);
            // 
            // 交易ToolStripMenuItem
            // 
            this.交易ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.buyIn,
            this.buyOut,
            this.cancelOrder,
            this.storeSell,
            this.最大化ToolStripMenuItem,
            this.最小化ToolStripMenuItem,
            this.测试窗口ToolStripMenuItem});
            this.交易ToolStripMenuItem.Name = "交易ToolStripMenuItem";
            this.交易ToolStripMenuItem.Size = new System.Drawing.Size(44, 21);
            this.交易ToolStripMenuItem.Text = "交易";
            // 
            // buyIn
            // 
            this.buyIn.Name = "buyIn";
            this.buyIn.Size = new System.Drawing.Size(124, 22);
            this.buyIn.Text = "买入";
            this.buyIn.Click += new System.EventHandler(this.buyIn_Click);
            // 
            // buyOut
            // 
            this.buyOut.Name = "buyOut";
            this.buyOut.Size = new System.Drawing.Size(124, 22);
            this.buyOut.Text = "卖出";
            this.buyOut.Click += new System.EventHandler(this.buyOut_Click);
            // 
            // cancelOrder
            // 
            this.cancelOrder.Name = "cancelOrder";
            this.cancelOrder.Size = new System.Drawing.Size(124, 22);
            this.cancelOrder.Text = "撤单";
            this.cancelOrder.Click += new System.EventHandler(this.cancelOrder_Click);
            // 
            // storeSell
            // 
            this.storeSell.Name = "storeSell";
            this.storeSell.Size = new System.Drawing.Size(124, 22);
            this.storeSell.Text = "持仓卖出";
            this.storeSell.Click += new System.EventHandler(this.storeSell_Click);
            // 
            // 最大化ToolStripMenuItem
            // 
            this.最大化ToolStripMenuItem.Name = "最大化ToolStripMenuItem";
            this.最大化ToolStripMenuItem.Size = new System.Drawing.Size(124, 22);
            this.最大化ToolStripMenuItem.Text = "最大化";
            this.最大化ToolStripMenuItem.Click += new System.EventHandler(this.最大化ToolStripMenuItem_Click);
            // 
            // 最小化ToolStripMenuItem
            // 
            this.最小化ToolStripMenuItem.Name = "最小化ToolStripMenuItem";
            this.最小化ToolStripMenuItem.Size = new System.Drawing.Size(124, 22);
            this.最小化ToolStripMenuItem.Text = "最小化";
            this.最小化ToolStripMenuItem.Click += new System.EventHandler(this.最小化ToolStripMenuItem_Click);
            // 
            // 测试窗口ToolStripMenuItem
            // 
            this.测试窗口ToolStripMenuItem.Name = "测试窗口ToolStripMenuItem";
            this.测试窗口ToolStripMenuItem.Size = new System.Drawing.Size(124, 22);
            this.测试窗口ToolStripMenuItem.Text = "测试窗口";
            this.测试窗口ToolStripMenuItem.Click += new System.EventHandler(this.测试窗口ToolStripMenuItem_Click);
            // 
            // mainPro
            // 
            this.mainPro.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.start,
            this.stop,
            this.交易启动ToolStripMenuItem,
            this.交易停止ToolStripMenuItem});
            this.mainPro.Name = "mainPro";
            this.mainPro.Size = new System.Drawing.Size(56, 21);
            this.mainPro.Text = "主线程";
            // 
            // start
            // 
            this.start.Name = "start";
            this.start.Size = new System.Drawing.Size(124, 22);
            this.start.Text = "启动";
            this.start.Click += new System.EventHandler(this.start_Click);
            // 
            // stop
            // 
            this.stop.Name = "stop";
            this.stop.Size = new System.Drawing.Size(124, 22);
            this.stop.Text = "停止";
            this.stop.Click += new System.EventHandler(this.stop_Click);
            // 
            // 交易启动ToolStripMenuItem
            // 
            this.交易启动ToolStripMenuItem.Name = "交易启动ToolStripMenuItem";
            this.交易启动ToolStripMenuItem.Size = new System.Drawing.Size(124, 22);
            this.交易启动ToolStripMenuItem.Text = "交易启动";
            this.交易启动ToolStripMenuItem.Click += new System.EventHandler(this.交易启动ToolStripMenuItem_Click);
            // 
            // 交易停止ToolStripMenuItem
            // 
            this.交易停止ToolStripMenuItem.Name = "交易停止ToolStripMenuItem";
            this.交易停止ToolStripMenuItem.Size = new System.Drawing.Size(124, 22);
            this.交易停止ToolStripMenuItem.Text = "交易停止";
            this.交易停止ToolStripMenuItem.Click += new System.EventHandler(this.交易停止ToolStripMenuItem_Click);
            // 
            // 策略测试ToolStripMenuItem
            // 
            this.策略测试ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.演示策略ToolStripMenuItem});
            this.策略测试ToolStripMenuItem.Name = "策略测试ToolStripMenuItem";
            this.策略测试ToolStripMenuItem.Size = new System.Drawing.Size(68, 21);
            this.策略测试ToolStripMenuItem.Text = "策略测试";
            // 
            // 演示策略ToolStripMenuItem
            // 
            this.演示策略ToolStripMenuItem.Name = "演示策略ToolStripMenuItem";
            this.演示策略ToolStripMenuItem.Size = new System.Drawing.Size(124, 22);
            this.演示策略ToolStripMenuItem.Text = "演示策略";
            this.演示策略ToolStripMenuItem.Click += new System.EventHandler(this.演示策略ToolStripMenuItem_Click);
            // 
            // gp_buys_Time
            // 
            this.gp_buys_Time.Interval = 5000;
            this.gp_buys_Time.Tick += new System.EventHandler(this.gp_buys_Time_Tick);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.splitContainer1);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.groupBox1.Location = new System.Drawing.Point(0, 71);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(471, 275);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "日志信息";
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(3, 17);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.gp_web);
            this.splitContainer1.Panel2.Controls.Add(this.txt_log);
            this.splitContainer1.Size = new System.Drawing.Size(465, 255);
            this.splitContainer1.SplitterDistance = 107;
            this.splitContainer1.TabIndex = 0;
            // 
            // gp_web
            // 
            this.gp_web.Location = new System.Drawing.Point(205, 218);
            this.gp_web.MinimumSize = new System.Drawing.Size(20, 20);
            this.gp_web.Name = "gp_web";
            this.gp_web.ScriptErrorsSuppressed = true;
            this.gp_web.Size = new System.Drawing.Size(87, 28);
            this.gp_web.TabIndex = 2;
            this.gp_web.Url = new System.Uri("", System.UriKind.Relative);
            this.gp_web.Visible = false;
            this.gp_web.DocumentCompleted += new System.Windows.Forms.WebBrowserDocumentCompletedEventHandler(this.gp_web_DocumentCompleted);
            // 
            // txt_log
            // 
            this.txt_log.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txt_log.Location = new System.Drawing.Point(0, 0);
            this.txt_log.Multiline = true;
            this.txt_log.Name = "txt_log";
            this.txt_log.ScrollBars = System.Windows.Forms.ScrollBars.Horizontal;
            this.txt_log.Size = new System.Drawing.Size(354, 255);
            this.txt_log.TabIndex = 0;
            this.txt_log.DoubleClick += new System.EventHandler(this.txt_log_DoubleClick);
            // 
            // txt_url
            // 
            this.txt_url.Location = new System.Drawing.Point(187, 29);
            this.txt_url.Name = "txt_url";
            this.txt_url.Size = new System.Drawing.Size(278, 21);
            this.txt_url.TabIndex = 2;
            // 
            // cb_new_type
            // 
            this.cb_new_type.FormattingEnabled = true;
            this.cb_new_type.Items.AddRange(new object[] {
            "新闻联播",
            "IPO收益",
            "业绩快报",
            "业绩预增",
            "业绩预披",
            "个股公告",
            "今日头条-同花顺",
            "今日要闻",
            "创新低",
            "创新高",
            "向上突波",
            "向下突波",
            "大众交易",
            "大单追踪",
            "异动观察",
            "持续放量",
            "持续缩量",
            "新股申购",
            "早盘必读",
            "机会情报",
            "概念资金",
            "行业资金",
            "财经要闻",
            "财经要闻-同花顺",
            "连续上涨",
            "连续下跌",
            "送股派息",
            "量价齐升",
            "量较齐跌",
            "限售解禁",
            "险资举牌",
            "首页-同花顺",
            "高管持股"});
            this.cb_new_type.Location = new System.Drawing.Point(60, 30);
            this.cb_new_type.Name = "cb_new_type";
            this.cb_new_type.Size = new System.Drawing.Size(121, 20);
            this.cb_new_type.TabIndex = 3;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 33);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(41, 12);
            this.label1.TabIndex = 4;
            this.label1.Text = "分类：";
            // 
            // get_filter_timer
            // 
            this.get_filter_timer.Interval = 60000;
            this.get_filter_timer.Tick += new System.EventHandler(this.get_filter_timer_Tick);
            // 
            // gp_sells_timer
            // 
            this.gp_sells_timer.Interval = 5000;
            this.gp_sells_timer.Tick += new System.EventHandler(this.gp_sells_timer_Tick);
            // 
            // MainFrm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(471, 346);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.cb_new_type);
            this.Controls.Add(this.txt_url);
            this.Controls.Add(this.menuStrip1);
            this.Controls.Add(this.groupBox1);
            this.MainMenuStrip = this.menuStrip1;
            this.MaximumSize = new System.Drawing.Size(487, 384);
            this.MinimumSize = new System.Drawing.Size(487, 384);
            this.Name = "MainFrm";
            this.Text = "量化投资";
            this.Load += new System.EventHandler(this.MainFrm_Load);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem 历史数据ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 实时数据ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 交易ToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem buyIn;
        private System.Windows.Forms.ToolStripMenuItem buyOut;
        private System.Windows.Forms.ToolStripMenuItem cancelOrder;
        private System.Windows.Forms.ToolStripMenuItem storeSell;
        private System.Windows.Forms.ToolStripMenuItem mainPro;
        private System.Windows.Forms.ToolStripMenuItem start;
        private System.Windows.Forms.ToolStripMenuItem stop;
        private System.Windows.Forms.Timer gp_buys_Time;
        private System.Windows.Forms.ToolStripMenuItem forTest;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox txt_log;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.WebBrowser gp_web;
        private System.Windows.Forms.ToolStripMenuItem 新闻获取ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 财务数据ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 个股公告ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 新闻后处理ToolStripMenuItem;
        private System.Windows.Forms.TextBox txt_url;
        private System.Windows.Forms.ComboBox cb_new_type;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ToolStripMenuItem 新闻分析ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 保存到自选股ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 删除自选股ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 最大化ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 最小化ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 要闻刷新ToolStripMenuItem;
        private System.Windows.Forms.Timer get_filter_timer;
        private System.Windows.Forms.Timer gp_sells_timer;
        private System.Windows.Forms.ToolStripMenuItem 交易启动ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 交易停止ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 退出程序ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 测试窗口ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 策略测试ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 演示策略ToolStripMenuItem;
    }
}