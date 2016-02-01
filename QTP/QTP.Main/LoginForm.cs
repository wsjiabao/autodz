using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Configuration;


namespace QTP.Main
{
    public partial class LoginForm : Form
    {
        public LoginForm()
        {
            InitializeComponent();
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            // Read config file
            Configuration config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);

            // check password
            Global.ConnectionString =
                config.ConnectionStrings.ConnectionStrings["QTP_DB"].ConnectionString.ToString();

            SqlConnection conn = new SqlConnection(Global.ConnectionString);
            SqlCommand cmd = new SqlCommand("SELECT Password FROM Login", conn);
            conn.Open();
            string password = (string)cmd.ExecuteScalar();
            conn.Close();
            if (password.TrimEnd() != textBoxPassword.Text)
            {
                MessageBox.Show("密码错误!");
                return;
            }
            this.DialogResult = DialogResult.OK;
        }

        private void LoginForm_Load(object sender, EventArgs e)
        {
            // Create MdAdapter 
//            Subject.Instance.MD = MDFactory.Create(Subject.Instance.ES);
        }
    }
}
