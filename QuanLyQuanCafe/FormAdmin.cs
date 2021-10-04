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
            dtgvFood.Columns["Status"].Visible = false;
        }

        void AddFoodBinding()
        {
            txbFoodID.DataBindings.Add(new Binding("Text", dtgvFood.DataSource, "Id",true, DataSourceUpdateMode.Never));
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


        #endregion

        private void btnAddFood_Click(object sender, EventArgs e)
        {
            string name = txbFoodName.Text;
            int idCategory = cbFoodCategory.SelectedIndex + 1;
            float price = Convert.ToSingle(nmFoodPrice.Value);    
            if (FoodDAO.Instance.InsertFood(name, idCategory, price))
            {
                MessageBox.Show("Thêm thức ăn thành công", "Thông báo", MessageBoxButtons.OK);
                _insertFood?.Invoke(this, new EventArgs());
            }    
            else
                MessageBox.Show("Có lỗi khi thêm thức ăn", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            LoadListFood();
        }

        private void btnEditFood_Click(object sender, EventArgs e)
        {
            int id = Convert.ToInt32(txbFoodID.Text);
            string name = txbFoodName.Text;
            int idCategory = cbFoodCategory.SelectedIndex + 1;
            float price = Convert.ToSingle(nmFoodPrice.Value);
            if (FoodDAO.Instance.UpdateFood(id, name, idCategory, price))
            {
                MessageBox.Show("Sửa thức ăn thành công", "Thông báo", MessageBoxButtons.OK);
                _updateFood?.Invoke(this, new EventArgs());
            }    
            else
                MessageBox.Show("Có lỗi khi sửa thức ăn", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            LoadListFood();
        }

        private void btnDeleteFood_Click(object sender, EventArgs e)
        {
            int id = Convert.ToInt32(txbFoodID.Text);
            string name = txbFoodName.Text;
            int idCategory = cbFoodCategory.SelectedIndex + 1;
            float price = Convert.ToSingle(nmFoodPrice.Value);
            if (FoodDAO.Instance.DeleteFood(id))
            {
                MessageBox.Show("Xoá ăn thành công", "Thông báo", MessageBoxButtons.OK);
                _deleteFood?.Invoke(this, new EventArgs());
            }    
                
            else
                MessageBox.Show("Có lỗi khi xoá thức ăn", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            LoadListFood();
        }
        private event EventHandler _insertFood;
        public event EventHandler InsertFood
        {
            add { _insertFood += value; }
            remove { _insertFood -= value; }
        }
        private event EventHandler _updateFood;
        public event EventHandler UpdateFood
        {
            add { _updateFood += value; }
            remove { _updateFood -= value; }
        }
        private event EventHandler _deleteFood;
        public event EventHandler DeleteFood
        {
            add { _deleteFood += value; }
            remove { _deleteFood -= value; }
        }


    }
}
