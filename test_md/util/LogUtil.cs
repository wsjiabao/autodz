using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace MdTZ
{
    class LogUtil
    {
        private static FileStream ostrm;
        private static  StreamWriter writer;
        private static TextWriter oldOut;

        public static void writeLog(string text)
        {            
            if (oldOut == null)
            {
                oldOut = Console.Out;
            }
            try
            {
                if (ostrm == null)
                {
                    ostrm = new FileStream("./Syslog.txt", FileMode.OpenOrCreate, FileAccess.Write);
                    writer = new StreamWriter(ostrm);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Cannot open Syslog.txt for writing");
                Console.WriteLine(e.Message);
                return;
            }

            Console.SetOut(writer);

            Console.WriteLine(text);

            Console.SetOut(oldOut);
        }

        public static void closeLogFile()
        {
            writer.Close();
            ostrm.Close();
        }
    }
}
