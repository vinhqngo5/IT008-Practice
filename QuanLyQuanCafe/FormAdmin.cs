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
            int idCategpry = (cbFoodCategory.SelectedItem as Category).Id;
            float price = Convert.ToSingle(nmFoodPrice.Value);

            if (FoodDAO.Instance.InsertFood(name, idCategpry, price))
            {
                MessageBox.Show("Thêm món thành công", "thông báo");
                LoadListFood();
                this._insertFood?.Invoke(this, new EventArgs());
            }
            else
            {
                MessageBox.Show("Có lỗi khi thêm thức ăn");
            }

        }

        private void btnEditFood_Click(object sender, EventArgs e)
        {
            string name = txbFoodName.Text;
            int idCategpry = (cbFoodCategory.SelectedItem as Category).Id;
            float price = Convert.ToSingle(nmFoodPrice.Value);
            int foodId = Convert.ToInt32(txbFoodID.Text);

            if (FoodDAO.Instance.UpdateFood(foodId, name, idCategpry, price))
            {
                MessageBox.Show("Sửa món thành công", "thông báo");
                LoadListFood();
                this._updateFood?.Invoke(this, new EventArgs());
            }
            else
            {
                MessageBox.Show("Có lỗi khi sửa thức ăn");
            }
        }

        private void btnDeleteFood_Click(object sender, EventArgs e)
        {
            int idFood = Convert.ToInt32(txbFoodID.Text);

            if (FoodDAO.Instance.ChangeFoodStatusById(idFood, false))
            {
                MessageBox.Show("Xóa món thành công", "thông báo");
                LoadListFood();
                this._deleteFood?.Invoke(this, new EventArgs());
            }
            else
            {
                MessageBox.Show("Có lỗi khi xóa thức ăn");
            }
        }

        private event EventHandler _insertFood;
        public event EventHandler InsertFood
        {
            add { _insertFood += value; }
            remove { _insertFood -= value; }
        }

        private event EventHandler _deleteFood;
        public event EventHandler DeleteFood
        {
            add { _deleteFood += value; }
            remove { _deleteFood -= value; }
        }

        private event EventHandler _updateFood;
        public event EventHandler UpdateFood
        {
            add { _updateFood += value; }
            remove { _updateFood -= value; }
        }

        #endregion
    }
}
