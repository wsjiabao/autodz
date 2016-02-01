using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace MdTZ
{
    class RegexDao
    {

       public static bool isDpContionDown()
        {
            string his = GPUtil.getDPHis();           
            return Regex.IsMatch(his, "^.*[0-4]{2,5}$")
                   || Regex.IsMatch(his, "^.*[0-3]+$"); 
        }


    }
}
