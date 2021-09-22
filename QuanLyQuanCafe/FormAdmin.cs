using QuanLyQuanCafe.DAO;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QuanLyQuanCafe
{
    public partial class FormAdmin : Form
    {
        public FormAdmin()
        {
            InitializeComponent();

            LoadAccountList();
        }

        void LoadAccountList()
        {
            //string query = "SELECT DisplayName as [Tên hiển thị] FROM dbo.Account";
            string query = "EXEC dbo.USP_GetAccountByUserName @userName";

            DataProvider provider = new DataProvider();

            dtgvAccount.DataSource = provider.ExecuteQuery(query, new object[] { "staff" });
        }
    }
}
