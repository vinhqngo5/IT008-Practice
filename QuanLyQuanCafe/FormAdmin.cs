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

            //LoadFoodList();
        }

        private void LoadFoodList()
        {
            string query = "SELECT * FROM dbo.Food";

            dtgvAccount.DataSource = DataProvider.Instance.ExecuteQuery(query);
        }

        private void LoadAccountList()
        {
            string query = "EXEC USP_GetAccountByUserName @userName";

            dtgvAccount.DataSource = DataProvider.Instance.ExecuteQuery(query, new object[] {"admin"});
        } 
    }
}
