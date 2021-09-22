using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

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
            string query = "EXEC dbo.USP_GetAccountByUserName @userName";
            dtgvAccount.DataSource = DataProvider.Instance.ExecuteQuery(query,new object[] {"staff"});
        }
        void LoadFoodList()
        {
            string query = "SELECT * FROM FOOD";
            dtgvFood.DataSource = DataProvider.Instance.ExecuteQuery(query);
        }

    }
}
