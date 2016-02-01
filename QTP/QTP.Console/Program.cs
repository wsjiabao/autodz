using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;

using QTP.Infra;
using QTP.DBAccess;
using QTP.Domain;
using GMSDK;

namespace QTP.Console
{
    class Program
    {
        private static StrategyQTP currentStrategy;

        // An enumerated type for the control messages sent to the handler routine.
        enum CtrlTypes
        {
            CTRL_C_EVENT = 0,
            CTRL_BREAK_EVENT,
            CTRL_CLOSE_EVENT,
            CTRL_LOGOFF_EVENT = 5,
            CTRL_SHUTDOWN_EVENT
        }

        // Declare the SetConsoleCtrlHandler function as external and receiving a delegate.
        [DllImport("Kernel32")]
        static extern bool SetConsoleCtrlHandler(HandlerRoutine Handler, bool Add);

        // A delegate type to be used as the handler routine for SetConsoleCtrlHandler.
        delegate bool HandlerRoutine(CtrlTypes CtrlType);

        static bool ConsoleCtrlCheck(CtrlTypes ctrlType)
        {
            // Put your own handler here
            if (currentStrategy != null) currentStrategy.Stop();
            return true;
        }

        static void Main(string[] args)
        {
            // process args of program
            if (args.Length == 0)
            {
                System.Console.WriteLine("[错误] 无运行参数");
                System.Console.Read();
                return;
            }

            // Open NLog file
            NLog nlog = new NLog();
            if (!nlog.Open(args[0]))
            {
                System.Console.WriteLine("[错误] 打开日志文件失败");
                System.Console.Read();
                return;
            }

            // 设置中断命令处理程序(即清理并停止策略循环运行)。
            SetConsoleCtrlHandler(new HandlerRoutine(ConsoleCtrlCheck), true);

            // Create Strategy
            currentStrategy = StrategyFactory.CreateFromDB(args[0], nlog);
            if (currentStrategy == null)
            {
                System.Console.WriteLine("[错误] 未能创建策略类");
                System.Console.Read();
                return;
            }

            // Start it
            currentStrategy.Start();

            System.Console.WriteLine("策略已停止运行");
            System.Console.Read();
        }
    }
}
