using QuanLyQuanCafe.DAO;
using QuanLyQuanCafe.DTO;
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
        BindingSource foodList = new BindingSource();
        public FormAdmin()
        {
            InitializeComponent();
            Load();

        }
        void Load()
        {
            LoadDateTimePickerBill();
            LoadListBillByDate(dtpkFromDate.Value, dtpkToDate.Value);
            LoadListFood();
            dtgvFood.DataSource = foodList;
            AddFoodBiding();
            LoadCategoryIntoCombobox(cbFoodCategory);
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
        void LoadListFood()
        {
            foodList.DataSource = FoodDAO.Instance.GetListFood();
        }
        void AddFoodBiding()
        {
            txbFoodName.DataBindings.Add(new Binding("Text", dtgvFood.DataSource, "Tên món ăn"));
            txbFoodID.DataBindings.Add(new Binding("Text", dtgvFood.DataSource, "ID"));
            nmFoodPrice.DataBindings.Add(new Binding("Value", dtgvFood.DataSource, "Giá"));
        }
        void LoadCategoryIntoCombobox (ComboBox cb)
        {
            cb.DataSource = CategoryDAO.Instance.GetListCategories();
            cb.DisplayMember = "Name";
        }
        #endregion

        #region Events

        private void btnViewBill_Click(object sender, EventArgs e)
        {
            LoadListBillByDate(dtpkFromDate.Value, dtpkToDate.Value);
        }
        private void btnShowFood_Click(object sender, EventArgs e)
        {
            LoadListFood();
        }

        private void txbFoodID_TextChanged(object sender, EventArgs e)
        {
            int id = (int)dtgvFood.SelectedCells[0].OwningRow.Cells["Loại món ăn"].Value;
            cbFoodCategory.SelectedIndex = id-1;
        }

        #endregion


    }
}
