using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace MdTZ
{
    class KeyWordAPI
    {

        public static string ext_key = "流出,抛售,涨幅居前,弱势,跌幅居前,流入";

        public static Dictionary<String, String> keyData = null;

       

        public static List<string> gpNameList = new List<string>();

        /**
         ** 加载关键字
        **/
        public static void loadKeyWord()
        {

            keyData = new Dictionary<string, string>();

            DataTable dataTable = GPUtil.helper.
                 ExecuteDataTable("SELECT gn,gnname from gn order by gn", GPUtil.parms);
            StringBuilder codes = new StringBuilder();

            string keystr = "";
            if (dataTable != null)
            {
                foreach (DataRow row in dataTable.Rows)
                {
                    keystr = row[1].ToString();

                    foreach (string key in keystr.Split(",".ToCharArray()))
                    {
                        if (!keyData.Keys.Contains(key))
                        {
                            keyData.Add(key, row[0].ToString());
                        }
                    }

                }
            }


            dataTable = GPUtil.helper.ExecuteDataTable("SELECT name from gp", GPUtil.parms);
            StringBuilder names = new StringBuilder();
            if (dataTable != null)
            {
                foreach (DataRow row in dataTable.Rows)
                {
                    gpNameList.Add(row[0].ToString());
                }
            }

        }
    }
}
