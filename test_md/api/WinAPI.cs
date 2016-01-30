using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;

namespace MdTZ
{
    public class WinAPI
    {


        public const int MOUSEEVENTF_MOVE = 0x0001;     // 移动鼠标 
        public const int MOUSEEVENTF_LEFTDOWN = 0x0002; //模拟鼠标左键按下 
        public const int MOUSEEVENTF_LEFTUP = 0x0004; //模拟鼠标左键抬起 
        public const int MOUSEEVENTF_RIGHTDOWN = 0x0008; //模拟鼠标右键按下 
        public const int MOUSEEVENTF_RIGHTUP = 0x0010; //模拟鼠标右键抬起 
        public const int MOUSEEVENTF_MIDDLEDOWN = 0x0020; //模拟鼠标中键按下 
        public const int MOUSEEVENTF_MIDDLEUP = 0x0040; //模拟鼠标中键抬起 
        public const int MOUSEEVENTF_ABSOLUTE = 0x8000;// 标示是否采用绝对坐标 


        [DllImport("user32.dll", EntryPoint = "FindWindow")]
        public static extern int FindWindow(
            string lpClassName,
            string lpWindowName
        );

        [DllImport("user32.dll", EntryPoint = "GetWindow")]//获取窗体句柄，hwnd为源窗口句柄
        public static extern int GetWindow(
            int hwnd,
            int wCmd
        );

        [DllImport("user32.dll", EntryPoint = "SetParent")]//设置父窗体
        public static extern int SetParent(
            int hWndChild,
            int hWndNewParent
        );
        [DllImport("user32.dll", EntryPoint = "GetCursorPos")]//获取鼠标坐标
        public static extern int GetCursorPos(
            ref POINTAPI lpPoint
        );

        [StructLayout(LayoutKind.Sequential)]//定义与API相兼容结构体，实际上是一种内存转换
        public struct POINTAPI
        {
            public int X;
            public int Y;
        }

        [DllImport("user32.dll", EntryPoint = "WindowFromPoint")]//指定坐标处窗体句柄
        public static extern int WindowFromPoint(
            int xPoint,
            int yPoint
        );

        [DllImport("user32.dll", EntryPoint = "GetWindowText")]
        public static extern int GetWindowText(int hWnd, StringBuilder lpString, int nMaxCount);

        [DllImport("user32.dll", EntryPoint = "GetParent")]
        public static extern int GetParent(int hWnd);


        [DllImport("user32.dll", EntryPoint = "mouse_event")]
        public static extern int mouse_event(int dwFlags, int dx, int dy, int cButtons, int dwExtraInfo);

        [DllImport("user32.dll")]
        public static extern void SetCursorPos(int x, int y);


        [DllImport("user32.dll", EntryPoint = "keybd_event")]
        public static extern void keybd_event(
            byte bVk, //虚拟键值
            byte bScan,// 一般为0
            int dwFlags, //这里是整数类型 0 为按下，2为释放
            int dwExtraInfo //这里是整数类型 一般情况下设成为 0
        );

       

        [DllImport("user32.dll", EntryPoint = "FindWindowEx")]
        public static extern int FindWindowEx(int hwndParent, IntPtr hwndChildAfter, string lpszClass, string lpszWindow);


    }
}
