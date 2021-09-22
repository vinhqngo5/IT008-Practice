using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QuanLyQuanCafe
{
    class AccountDAO
    {
        private static AccountDAO s_instance;

        internal static AccountDAO Instance 
        { 
            get { if (s_instance == null) s_instance = new AccountDAO(); return s_instance; }
            private set => s_instance = value; 
        }
        private AccountDAO() { }
        public bool Login(string userName,string Password)
        {
            string query = "SELECT * FROM Account WHERE UserName=N'"+userName+"' AND PassWord=N'"+Password+"'";
            DataTable result = DataProvider.Instance.ExecuteQuery(query);
            return result.Rows.Count > 0;
        }
    }
}
