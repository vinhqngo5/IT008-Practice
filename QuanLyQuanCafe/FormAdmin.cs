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
        private readonly BindingSource _accountList = new BindingSource();
        private Account _loginAccount;
        private event EventHandler<AccountEvent> _updateAccount;
        public event EventHandler<AccountEvent> UpdateAccount
        {
            add { _updateAccount += value; }
            remove { _updateAccount -= value; }
        }

        public Account LoginAccount { get => _loginAccount; set => _loginAccount = value; }

        public FormAdmin()
        {
            InitializeComponent();
            LoadDateTimePickerBill();
            LoadListBillByDate(dtpkFromDate.Value, dtpkToDate.Value);
            LoadCategoryIntoComboBox();
            LoadListFood();
            AddFoodBinding();
            LoadAccount();
            AddAccountBinding();
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

        void LoadAccount()
        {
            dtgvAccount.DataSource = _accountList;
            _accountList.DataSource = AccountDAO.Instance.GetListAccount();
        }

        void AddAccountBinding()
        {
            txbUserName.DataBindings.Add(new Binding("Text", dtgvAccount.DataSource, "UserName", true, DataSourceUpdateMode.Never));
            txbDisplayName.DataBindings.Add(new Binding("Text", dtgvAccount.DataSource, "DisplayName", true, DataSourceUpdateMode.Never));
            chkAccountType.DataBindings.Add(new Binding("Checked", dtgvAccount.DataSource, "Type", true, DataSourceUpdateMode.Never));
        }

        void AddAccount(string userName, string displayName, bool type)
        {
            if (AccountDAO.Instance.InsertAccount(userName, displayName, type))
            {
                MessageBox.Show("Thêm tài khoản thành công");
            }
            else
            {
                MessageBox.Show("Thêm tài khoản thất bại");
            }

            LoadAccount();
        }

        void EditAccount(string userName, string displayName, bool type)
        {
            if (LoginAccount.UserName == userName)
                type = true;

            if (AccountDAO.Instance.UpdateAccount(userName, displayName, type))
            {
                MessageBox.Show("Cập nhật tài khoản thành công");
                if (LoginAccount.UserName == userName)
                    _updateAccount?.Invoke(this, new AccountEvent(AccountDAO.Instance.GetAccountByUserName(userName)));
            }
            else
            {
                MessageBox.Show("Cập nhật tài khoản thất bại");
            }

            LoadAccount();
        }

        void DeleteAccount(string userName)
        {
            if (LoginAccount.UserName == userName)
            {
                MessageBox.Show("Đừng ngu như thế chứ :>");
                return;
            }

            if (AccountDAO.Instance.DeleteAccount(userName))
            {
                MessageBox.Show("Xóa tài khoản thành công");
            }
            else
            {
                MessageBox.Show("Xóa tài khoản thất bại");
            }

            LoadAccount();
        }

        void ResetPassword(string userName)
        {
            if (AccountDAO.Instance.ResetPassword(userName))
            {
                MessageBox.Show("Đặt lại mật khẩu thành công");
            }
            else
            {
                MessageBox.Show("Đặt lại mật khẩu thất bại");
            }

            LoadAccount();
        }
        #endregion

        #region Events

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
                _insertFood?.Invoke(this, new EventArgs());
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
                _updateFood?.Invoke(this, new EventArgs());
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
                _deleteFood?.Invoke(this, new EventArgs());
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

        private void btnShowAccount_Click(object sender, EventArgs e)
        {
            LoadAccount();
        }

        private void btnAddAccount_Click(object sender, EventArgs e)
        {
            string userName = txbUserName.Text;
            string displayName = txbDisplayName.Text;
            bool type = chkAccountType.Checked;

            AddAccount(userName, displayName, type);
        }

        private void btnDeleteAccount_Click(object sender, EventArgs e)
        {
            string userName = txbUserName.Text;

            DeleteAccount(userName);
        }

        private void btnEditAccount_Click(object sender, EventArgs e)
        {
            string userName = txbUserName.Text;
            string displayName = txbDisplayName.Text;
            bool type = chkAccountType.Checked;

            EditAccount(userName, displayName, type);
        }

        private void btnResetPassword_Click(object sender, EventArgs e)
        {
            string userName = txbUserName.Text;

            ResetPassword(userName);
        }
        #endregion

        private void btnFirstBillPage_Click(object sender, EventArgs e)
        {
            txbBillPage.Text = "1";
        }

        private void btnLastBillPage_Click(object sender, EventArgs e)
        {
            int sumRecord = BillDAO.Instance.GetNumBillByDate(dtpkFromDate.Value, dtpkToDate.Value);
            int lastPage = sumRecord / 10;
            if (sumRecord %10 != 0)
            {
                lastPage++;
            }
            txbBillPage.Text = Convert.ToString(lastPage);
        }

        private void txbBillPage_TextChanged(object sender, EventArgs e)
        {
            dtgvBill.DataSource = BillDAO.Instance.GetListBillByDateAndPage(dtpkFromDate.Value, dtpkToDate.Value, Convert.ToInt32(txbBillPage.Text));
        }

        private void btnPreviousBillPage_Click(object sender, EventArgs e)
        {
            if (txbBillPage.Text == "1")
                return;
            txbBillPage.Text = Convert.ToString(Convert.ToInt32(txbBillPage.Text) - 1);
        }

        private void btnNextBillPage_Click(object sender, EventArgs e)
        {
            txbBillPage.Text = Convert.ToString(Convert.ToInt32(txbBillPage.Text) + 1);
        }
    }
}
