
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Runtime.InteropServices;

using System.Threading;

namespace MdTZ
{
    public partial class Form4 : Form
    {
        public Form4()
        {
            InitializeComponent();
        }

        const int GW_CHILD = 5;//定义窗体关系
        WinAPI.POINTAPI point2 = new WinAPI.POINTAPI();//必须用与之相兼容的结构体，类也可以
       
        private void Form4_Load(object sender, EventArgs e)
        {
            int hDesktop = WinAPI.FindWindow("Progman", null);//获取系统句柄
            hDesktop = WinAPI.GetWindow(hDesktop, GW_CHILD);//获取其子窗口句柄，就是桌面的句柄
            WinAPI.SetParent((int)this.Handle, hDesktop);//设置父窗体，第一个为要被设置的窗口，第二个参数为指定其父窗口句柄
        }      

        private void button1_Click(object sender, EventArgs e)
        {
            WinAPI.mouse_event(WinAPI.MOUSEEVENTF_ABSOLUTE | WinAPI.MOUSEEVENTF_MOVE, Convert.ToInt32(this.textBox1.Text), Convert.ToInt32(this.textBox2.Text), 0, 0);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            //方法测试
            THSAPI.selfSel("000001","add");
        }

        private void button3_Click(object sender, EventArgs e)
        {
            //方法测试
            THSAPI.sellOut("600081", THSAPI.PRICE_OPT_BUY_1, 2.34, THSAPI.NUM_OPT_INPUT, 100);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            THSAPI.storeSell(THSAPI.PRICE_OPT_NOW, 0, THSAPI.NUM_OPT_ALL, 100);
        }

        private void button5_Click(object sender, EventArgs e)
        {
            THSAPI.agentCancel("000821", "a");
        }

        private void button6_Click(object sender, EventArgs e)
        {
            //方法测试
            THSAPI.strategyBuyIn("000001","5", THSAPI.PRICE_OPT_NOW, 0, THSAPI.NUM_OPT_ALL, 0);
        }

        private void button7_Click(object sender, EventArgs e)
        {
            //方法测试
            THSAPI.strategySell("600081", "2", THSAPI.PRICE_OPT_NOW, 0, THSAPI.NUM_OPT_INPUT, 200);
        }

        private void button8_Click(object sender, EventArgs e)
        {


            //List<GuoPiao> gpList = SinaAPI.getGPList("601003,601001");
            //GuoPiao gp = gpList[0];
            //MessageBox.Show(gp.name + "|" + gp.s1_num);

            //List<DaoPan> dpList = SinaAPI.getDPList("");
            //DaoPan dp = dpList[0];
            //MessageBox.Show(dp.name + "|" + dp.zs);

            StoreMng.updateBuyedAmt(10000);
            MessageBox.Show(StoreMng.buyedAmt.ToString());
            
        }

        private void button9_Click(object sender, EventArgs e)
        {

           //Properties.Resources.aa = "123";
           // MessageBox.Show(Properties.Resources.dpcodes.);
            //Properties.Resources.ResourceManager.

        }


    }
}
