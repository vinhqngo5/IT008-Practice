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
        private readonly BindingSource _foodList = new BindingSource();
        public Action UpdateFood;

        public FormAdmin()
        {
            InitializeComponent();
            LoadDateTimePickerBill();
            LoadListBillByDate(dtpkFromDate.Value, dtpkToDate.Value);

            LoadCategoryIntoComboBox();

            LoadListFood();

            AddFoodBinding();

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
            dtgvFood.DataSource = _foodList;
            _foodList.DataSource = FoodDAO.Instance.GetListFood();
            dtgvFood.Columns["IdCategory"].Visible = false;
        }

        void AddFoodBinding()
        {
            txbFoodID.DataBindings.Add(new Binding("Text", dtgvFood.DataSource, "Id", true, DataSourceUpdateMode.Never));
            txbFoodName.DataBindings.Add(new Binding("Text", dtgvFood.DataSource, "Name", true, DataSourceUpdateMode.Never));
            nmFoodPrice.DataBindings.Add(new Binding("Value", dtgvFood.DataSource, "Price", true, DataSourceUpdateMode.Never));
            cbFoodCategory.DataBindings.Add(new Binding("SelectedIndex", dtgvFood.DataSource, "IdCategory", true, DataSourceUpdateMode.Never));
        }

        void LoadCategoryIntoComboBox()
        {
            cbFoodCategory.DataSource = CategoryDAO.Instance.GetListCategories();
            cbFoodCategory.DisplayMember = "Name";
        }

        List<Food> SearchFoodByName(string name)
        {
            return FoodDAO.Instance.SearchFoodByName(name);
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

        private void btnAddFood_Click(object sender, EventArgs e)
        {
            string name = txbFoodName.Text;
            int idCategory = (cbFoodCategory.SelectedItem as Category).Id;
            float price = Convert.ToSingle(nmFoodPrice.Value);

            if (FoodDAO.Instance.InsertFood(name, idCategory, price))
            {
                MessageBox.Show("Thêm món thành công!", "Thêm món");
                LoadListFood();
                UpdateFood?.Invoke();
            }
            else
            {
                MessageBox.Show("Thêm món không thành công!", "Thêm món");
            }
        }

        private void btnEditFood_Click(object sender, EventArgs e)
        {
            int id = Convert.ToInt32(txbFoodID.Text);
            string name = txbFoodName.Text;
            int idCategory = (cbFoodCategory.SelectedItem as Category).Id;
            float price = Convert.ToSingle(nmFoodPrice.Value);

            if (FoodDAO.Instance.UpdateFood(id, name, idCategory, price))
            {
                MessageBox.Show("Cập nhật món thành công!", "Cập nhật món");
                LoadListFood();
                UpdateFood?.Invoke();
            }
            else
            {
                MessageBox.Show("Cập nhật món không thành công!", "Cập nhật món");
            }
        }

        private void btnDeleteFood_Click(object sender, EventArgs e)
        {
            int id = Convert.ToInt32(txbFoodID.Text);

            if (FoodDAO.Instance.DeleteFood(id))
            {
                MessageBox.Show("Xoá món thành công!", "Xoá món");
                LoadListFood();
                UpdateFood?.Invoke();
            }
            else
            {
                MessageBox.Show("Xoá món không thành công!", "Xoá món");
            }
        }

        private void btnSearchFood_Click(object sender, EventArgs e)
        {
            _foodList.DataSource = SearchFoodByName(txbSearchFoodName.Text);
        }
        #endregion
    }
}
