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
        public FormAdmin()
        {
            InitializeComponent();
            LoadDateTimePickerBill();
            LoadListBillByDate(dtpkFromDate.Value, dtpkToDate.Value);

            LoadCategoryIntoComboBox();

            LoadListFood();

            AddFoodBinding();

            LoadListAccount();
            LoadAccountTypeIntoComboBox();
            AddAccountBinding();
        }
   
        #region Methods
        public void LoadAccount(Account account)
        {
            _loginAccount = account;
        }
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

        void LoadListAccount()
        {
            dtgvAccount.DataSource = _accountList;
            _accountList.DataSource = AccountDAO.Instance.GetListAccount();
            dtgvAccount.Columns["PassWord"].Visible = false;
        }

        void AddFoodBinding()
        {
            txbFoodID.DataBindings.Add(new Binding("Text", dtgvFood.DataSource, "Id",true, DataSourceUpdateMode.Never));
            txbFoodName.DataBindings.Add(new Binding("Text", dtgvFood.DataSource, "Name", true, DataSourceUpdateMode.Never));
            nmFoodPrice.DataBindings.Add(new Binding("Value", dtgvFood.DataSource, "Price", true, DataSourceUpdateMode.Never));
            cbFoodCategory.DataBindings.Add(new Binding("SelectedIndex", dtgvFood.DataSource, "IdCategory", true, DataSourceUpdateMode.Never));
        }
        void AddAccountBinding()
        {
            txbUserName.DataBindings.Add(new Binding("Text", dtgvAccount.DataSource, "UserName", true, DataSourceUpdateMode.Never));
            txbDisplayName.DataBindings.Add(new Binding("Text", dtgvAccount.DataSource, "DisplayName", true, DataSourceUpdateMode.Never));
            cbAccountType.DataBindings.Add(new Binding("SelectedIndex",dtgvAccount.DataSource,"Type", true, DataSourceUpdateMode.Never));
        }

        void LoadCategoryIntoComboBox()
        {
            cbFoodCategory.DataSource = CategoryDAO.Instance.GetListCategories();
            cbFoodCategory.DisplayMember = "Name";
        }
        void LoadAccountTypeIntoComboBox()
        {
            cbAccountType.DataSource = AccountDAO.Instance.GetListTypeAccount();
            cbAccountType.DisplayMember = "Type";
        }

        void SerchFoodByString (string strSerch)
        {
            dtgvFood.DataSource = _foodList;
            _foodList.DataSource = FoodDAO.Instance.SerchFoodByString(strSerch);
            dtgvFood.Columns["IdCategory"].Visible = false;
            dtgvFood.Columns["Status"].Visible = false;
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
        private void btnSearchFood_Click(object sender, EventArgs e)
        {
            SerchFoodByString(Convert.ToString(txbSearchFoodName.Text));
        }
        private void btnShowAccount_Click(object sender, EventArgs e)
        {
            LoadListAccount();
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


        #endregion

        private void btnAddAccount_Click(object sender, EventArgs e)
        {
            string userName = txbUserName.Text;
            string displayName = txbDisplayName.Text;
            bool type = Convert.ToBoolean(cbAccountType.SelectedIndex);
            if (AccountDAO.Instance.GetAccountByUserName(userName)!=null)
            {
                MessageBox.Show("Tên tài khoản đã tồn tại", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }    
            if (AccountDAO.Instance.InsertAccount(userName, displayName, type))
            {
                MessageBox.Show("Thêm tài khoản thành công", "Thông báo", MessageBoxButtons.OK);
            }
            else
                MessageBox.Show("Có lỗi khi thêm tài khoản", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            LoadListAccount();
        }
        private void btnEditAccount_Click(object sender, EventArgs e)
        {
            string userName = txbUserName.Text;
            string displayName = txbDisplayName.Text;
            bool type = Convert.ToBoolean(cbAccountType.SelectedIndex);
            if (AccountDAO.Instance.UpdateAccountInAdminForm(userName, displayName, type))
            {
                MessageBox.Show("Sửa tài khoản thành công", "Thông báo", MessageBoxButtons.OK);
                if (_loginAccount.UserName.Equals(userName))
                    _updateAccount?.Invoke(this, new AccountEvent(AccountDAO.Instance.GetAccountByUserName(userName)));
            }
            else
                MessageBox.Show("Có lỗi khi sửa tài khoản", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            LoadListAccount();
        }
        private void btnDeleteAccount_Click(object sender, EventArgs e)
        {
            string userName = txbUserName.Text;
            if (userName.Equals(_loginAccount.UserName))
            {
                MessageBox.Show("Tài khoản bạn xoá là tài khoản đang sử dụng", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if(AccountDAO.Instance.DeleteAccount(userName))
                MessageBox.Show("Xoá tài khoản thành công", "Thông báo", MessageBoxButtons.OK);
            else
                MessageBox.Show("Có lỗi khi xoá tài khoản", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            LoadListAccount();
        }

        private void btnResetPassword_Click(object sender, EventArgs e)
        {
            string userName = txbUserName.Text; ;
            if (AccountDAO.Instance.ResetAccount(userName))
                MessageBox.Show("Đặt lại mật khẩu thành công", "Thông báo", MessageBoxButtons.OK);
            else
                MessageBox.Show("Có lỗi khi đặt lại mật khẩu", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            LoadListAccount();
        }
    }
}
