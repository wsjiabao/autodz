using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;

namespace QTP.Infra
{
    public class NLog
    {
          [DllImport( "kernel32.dll" )]   
          private static extern IntPtr _lopen(string lpPathName, int iReadWrite);   
          [DllImport( "kernel32.dll" )]   
          private static extern bool CloseHandle(IntPtr hObject);   

          private const int OF_READWRITE = 2;   
          private const int OF_SHARE_DENY_NONE = 0x40;   
          private readonly IntPtr HFILE_ERROR = new IntPtr( -1 );  
  
          private int VerifyFileIsOpen(string filename)   
          {   
              // 先检查文件是否存在,如果不存在那就不检查了   
              if ( !File.Exists(filename) )   
              {   
                  return -1;   
              }  
 
              // 打开指定文件看看情况   
              IntPtr vHandle = _lopen( filename, OF_READWRITE | OF_SHARE_DENY_NONE );   
              if ( vHandle == HFILE_ERROR )   
              { // 文件已经被打开                   
                  return 1;   
              }   
              CloseHandle( vHandle );   
              return 0;
        }
  
        private StreamWriter sw;
        public bool Open(string name)
        {
            string fileName = name + ".txt";
            if (VerifyFileIsOpen(fileName) == 1)
                return false;

            sw = new StreamWriter(fileName);
            return true;
        }
        public void Close()
        {
            sw.Close();
            sw = null;
        }

        public void WriteInfo(string msg)
        {
            WriteLine("Info", msg);
        }
        public void WriteDebug(string msg)
        {
            WriteLine("DEBUG", msg);
        }
        public void WriteWarning(string msg)
        {
            WriteLine("WARNING", msg);
        }

        public void WriteError(string msg)
        {
            WriteLine("ERROR", msg);
        }

        private void WriteLine(string type, string msg)
        {
            if (sw == null) return;
            sw.WriteLine(string.Format("[{0}.{1}] [{2}] {3}", DateTime.Now.ToLongTimeString(), DateTime.Now.Millisecond, type, msg));
            sw.Flush();
        }
    }
}
