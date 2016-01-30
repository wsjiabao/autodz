using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace MdTZ
{
    class GpSelectUtl
    {

        /**
       * 分时统计
       * */
        public static void gpSelfSel(string flag)
        {
            MySqlHelper helper = new MySqlHelper();
            MySqlParameter[] parms = new MySqlParameter[] { };
            DataTable dataTable = helper.ExecuteDataTable("SELECT code FROM gpbuy", parms);
            string code = "";
            if (dataTable != null)
            {
                foreach (DataRow row in dataTable.Rows)
                {
                    code = row["code"].ToString().Replace("sh", "").Replace("sz", "");
                    Console.WriteLine("code:" + code);
                    THSAPI.selfSel(code,flag);
                }
            }
        }
    }
}
