using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using QTP.Domain;
using System.Diagnostics;
//using QTP.MDWrapper;

namespace QTP.Main
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            LoginForm form = new LoginForm();

            if (form.ShowDialog() == DialogResult.Cancel)
            {
                this.Close();
            }

        }

        private void MainForm_FormClosed(object sender, FormClosedEventArgs e)
        {
        }

        #region menus
        private void 退出ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Close();
        }

        #endregion

        private void 行情ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ProcessStartInfo start = new ProcessStartInfo("cmd.exe");//设置运行的命令行文件问ping.exe文件，这个文件系统会自己找到

            //如果是其它exe文件，则有可能需要指定详细路径，如运行winRar.exe

            //start.Arguments = "www.126.com";    //设置命令参数

            start.CreateNoWindow = false;    //不显示dos命令行窗口

            //start.RedirectStandardOutput = true;//

            //start.RedirectStandardInput = true;//

            start.UseShellExecute = true;//是否指定操作系统外壳进程启动程序

            Process p= Process.Start(start);


            p.WaitForExit();//等待程序执行完退出进程

            p.Close();//关闭进程

        }

    }
}
