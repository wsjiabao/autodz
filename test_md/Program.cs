﻿using System;
using System.Collections.Generic;
using System.Linq;
using GMSDK;
using System.Windows.Forms;
using System.Threading;

namespace MdTZ
{
    static class Program
    {
       
      
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main()
        {
           
            

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new MdTZ.MainFrm());
        }

    }
}





