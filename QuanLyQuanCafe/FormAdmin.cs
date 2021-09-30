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

            LoadDateTimePickerBill();

            LoadListBillByDate(dtpkFromDate.Value, dtpkToDate.Value);
        }

        #region Methods

        void LoadListBillByDate(DateTime dateCheckIn, DateTime dateCheckOut)
        {
            dtgvBill.DataSource = BillDAO.Instance.GetListBillByDate(dateCheckIn, dateCheckOut);
        }

        void LoadDateTimePickerBill()
        {
            DateTime today = DateTime.Now;
            dtpkFromDate.Value = new DateTime(today.Year, today.Month, 1);
            dtpkToDate.Value = dtpkFromDate.Value.AddMonths(1).AddDays(-1);
        }

        #endregion

        #region Events

        private void btnViewBill_Click(object sender, EventArgs e)
        {
            LoadListBillByDate(dtpkFromDate.Value, dtpkToDate.Value);
        }

        #endregion
    }
}
