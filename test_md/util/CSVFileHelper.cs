using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.IO;
using System.Windows.Forms;

namespace MdTZ
{
    public class CSVFileHelper
    {

        /// <summary>
        /// 将DataTable中数据写入到CSV文件中
        /// </summary>
        /// <param name="dt">提供保存数据的DataTable</param>
        /// <param name="fileName">CSV的文件路径</param>
        public static void SaveCSV(DataTable dt, string fullPath)
        {
            FileInfo fi = new FileInfo(fullPath);
            if (!fi.Directory.Exists)
            {
                fi.Directory.Create();
            }
            FileStream fs = new FileStream(fullPath, System.IO.FileMode.Create, System.IO.FileAccess.Write);
            //StreamWriter sw = new StreamWriter(fs, System.Text.Encoding.Default);
            StreamWriter sw = new StreamWriter(fs, System.Text.Encoding.UTF8);
            string data = "";
            //写出列名称
            for (int i = 0; i < dt.Columns.Count; i++)
            {
                data += dt.Columns[i].ColumnName.ToString();
                if (i < dt.Columns.Count - 1)
                {
                    data += ",";
                }
            }
            sw.WriteLine(data);
            //写出各行数据
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                data = "";
                for (int j = 0; j < dt.Columns.Count; j++)
                {
                    string str = dt.Rows[i][j].ToString();
                    str = str.Replace("\"", "\"\"");//替换英文冒号 英文冒号需要换成两个冒号
                    if (str.Contains(',') || str.Contains('"')
                        || str.Contains('\r') || str.Contains('\n')) //含逗号 冒号 换行符的需要放到引号中
                    {
                        str = string.Format("\"{0}\"", str);
                    }

                    data += str;
                    if (j < dt.Columns.Count - 1)
                    {
                        data += ",";
                    }
                }
                sw.WriteLine(data);
            }
            sw.Close();
            fs.Close();
            DialogResult result = MessageBox.Show("CSV文件保存成功！");
            if (result == DialogResult.OK)
            {
                //System.Diagnostics.Process.Start("explorer.exe", Common.PATH_LANG);
            }
        }

        /// <summary>
        /// 将CSV文件的数据读取到DataTable中
        /// </summary>
        /// <param name="fileName">CSV文件路径</param>
        /// <returns>返回读取了CSV数据的DataTable</returns>
        public static DataTable OpenCSV(string filePath)
        {
            FileStream fs = new FileStream(filePath, System.IO.FileMode.Open, System.IO.FileAccess.Read);
            return OpenCSVFromSteam("",fs);
        }


        /// <summary>
        /// 将CSV文件的数据读取到DataTable中
        /// </summary>
        /// <param name="fileName">CSV文件路径</param>
        /// <returns>返回读取了CSV数据的DataTable</returns>
        public static DataTable OpenCSVFromSteam(string code, Stream stream)
        {
            Encoding encoding = Encoding.Default; //Encoding.ASCII;//
            DataTable dt = new DataTable();

            code = code.ToLower();
            bool isFromYaoh = true;
            if (code.IndexOf(".ss") != -1)
            {
                code = code.Replace(".ss", "");
                code = "sh" + code;
            }
            else if (code.IndexOf(".sz") != -1)
            {
                code = code.Replace(".sz", "");
                code = "sz" + code;
            }
            else
            {
                isFromYaoh = false;
            }

            //StreamReader sr = new StreamReader(stream, Encoding.UTF8);
            StreamReader sr = new StreamReader(stream, encoding);
            //string fileContent = sr.ReadToEnd();
            //encoding = sr.CurrentEncoding;
            //记录每次读取的一行记录
            string strLine = "";
            //记录每行记录中的各字段内容
            string[] aryLine = null;
            string[] tableHead = null;
            //标示列数
            int columnCount = 0;
            //标示是否是读取的第一行
            bool IsFirst = true;

            double sum_cjl = 0;
            double sum_cje = 0;
            //逐行读取CSV中的数据
            while ((strLine = sr.ReadLine()) != null)
            {
                //strLine = Common.ConvertStringUTF8(strLine, encoding);
                //strLine = StringUtil.convertStrUTF8(strLine);
                if (IsFirst == true)
                {
                    if (isFromYaoh)
                    {
                        strLine = strLine.Replace("Adj Close", "AdjClose");
                        tableHead = strLine.Split(',');
                        IsFirst = false;
                        columnCount = tableHead.Length;
                        //创建列
                        for (int i = 0; i < columnCount; i++)
                        {
                            DataColumn dc = new DataColumn(tableHead[i]);
                            dt.Columns.Add(dc);
                        }

                        //补充 ID, CODE
                        if (!String.IsNullOrEmpty(code))
                        {
                            DataColumn dc_id = new DataColumn("id");
                            dt.Columns.Add(dc_id);
                            DataColumn dc_code = new DataColumn("Code");
                            dt.Columns.Add(dc_code);
                        }
                    }
                    else
                    {
                        tableHead = strLine.Split('\t');
                        columnCount = tableHead.Length;
                        IsFirst = false;
                        tableHead[0] = "jytime";
                        tableHead[1] = "cjj";
                        tableHead[2] = "jgbd";
                        tableHead[3] = "cjl";
                        tableHead[4] = "cje";
                        tableHead[5] = "xz";
                        DataColumn dc = new DataColumn("jytime");
                        dt.Columns.Add(dc);
                        DataColumn dc1 = new DataColumn("cjj");
                        dt.Columns.Add(dc1);
                        DataColumn dc2 = new DataColumn("jgbd");
                        dt.Columns.Add(dc2);
                        DataColumn dc3 = new DataColumn("cjl");
                        dt.Columns.Add(dc3);
                        DataColumn dc4 = new DataColumn("cje");
                        dt.Columns.Add(dc4);
                        DataColumn dc5 = new DataColumn("xz");
                        dt.Columns.Add(dc5);
                        DataColumn dc6 = new DataColumn("id");
                        dt.Columns.Add(dc6);
                        DataColumn dc7 = new DataColumn("code");
                        dt.Columns.Add(dc7);
                        DataColumn dc8 = new DataColumn("sumcjl");
                        dt.Columns.Add(dc8);
                        DataColumn dc9 = new DataColumn("sumcje");
                        dt.Columns.Add(dc9);
                        DataColumn dc10 = new DataColumn("jj");
                        dt.Columns.Add(dc10);
                    }                   
                }
                else
                {
                    if (isFromYaoh)
                    {
                        aryLine = strLine.Split(',');
                    }
                    else
                    {
                        aryLine = strLine.Split('\t');
                    }
                   
                    DataRow dr = dt.NewRow();
                    for (int j = 0; j < columnCount; j++)
                    {
                        dr[j] = aryLine[j];
                    }

                    //补充 code 和 id
                    if (!String.IsNullOrEmpty(code))
                    {
                        dr[columnCount] = 0;                       
                        dr[columnCount + 1] = code;
                     
                        sum_cjl += Convert.ToDouble(dr[3]);
                        sum_cje += Convert.ToDouble(dr[4]);
                        dr[columnCount + 2] = sum_cjl;
                        dr[columnCount + 3] = sum_cje;
                    }
                    dt.Rows.Add(dr);                  
                }
            }

            if (aryLine != null && aryLine.Length > 0)
            {
                dt.DefaultView.Sort = tableHead[0] + " " + "asc";
            }

            stream.Close();
            sr.Close();
            return dt;
        }



        /// <summary>
        /// 导出报表为Csv
        /// </summary>
        /// <param name="dt">DataTable</param>
        /// <param name="strFilePath">物理路径</param>
        /// <param name="tableheader">表头</param>
        /// <param name="columname">字段标题,逗号分隔</param>
        public static string dt2csv(DataTable dt, string strFilePath, string tableheader, string columname)
        {
            try
            {
                string strBufferLine = "";
                StreamWriter strmWriterObj = new StreamWriter(strFilePath, false, System.Text.Encoding.UTF8);
                //strmWriterObj.WriteLine(tableheader);
                strmWriterObj.WriteLine(columname);
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    strBufferLine = "";
                    for (int j = 0; j < dt.Columns.Count; j++)
                    {
                        if (j > 0)
                            strBufferLine += ",";
                        strBufferLine += dt.Rows[i][j].ToString();
                    }
                    strmWriterObj.WriteLine(strBufferLine);
                }
                strmWriterObj.Close();
                return "备份成功";
            }
            catch (Exception ex)
            {
                return "备份失败 " + ex.ToString();
            }
        }

        /// <summary>
        /// 将Csv读入DataTable
        /// </summary>
        /// <param name="filePath">csv文件路径</param>
        /// <param name="n">表示第n行是字段title,第n+1行是记录开始</param>
        public static DataTable csv2dt(string filePath, int n, DataTable dt)
        {

            StreamReader reader = new StreamReader(filePath, System.Text.Encoding.UTF8, false);
            int i = 0, m = 0;
            reader.Peek();
            while (reader.Peek() > 0)
            {
                m = m + 1;
                string str = reader.ReadLine();
                if (m >= n + 1)
                {
                    string[] split = str.Split(',');

                    System.Data.DataRow dr = dt.NewRow();
                    for (i = 0; i < split.Length; i++)
                    {
                        dr[i] = split[i];
                    }
                    dt.Rows.Add(dr);
                }
            }
            return dt;
        }
    }



}
