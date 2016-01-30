using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MdTZ
{
    class StringUtil
    {
        public static string convertStrUTF8(string str)
        {

           // string utf8_string = Encoding.UTF8.GetString(Encoding.Default.GetBytes(str));
            string utf8_string = Encoding.UTF8.GetString(System.Text.Encoding.Convert(Encoding.ASCII, Encoding.UTF8, Encoding.ASCII.GetBytes(str)));
            return utf8_string;
        }
        
    }
}
