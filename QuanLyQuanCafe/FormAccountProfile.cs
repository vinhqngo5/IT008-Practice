using QuanLyQuanCafe.DAO;
using QuanLyQuanCafe.DTO;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QuanLyQuanCafe
{
    public partial class FormAccountProfile : Form
    {
        private Account _loginAccount;

        public Account LoginAccount
        { get => _loginAccount; set => _loginAccount = value; }
        public FormAccountProfile(Account account)
        {
            InitializeComponent();
            this.LoginAccount = account;
            CheckAccount(account);
        }
        #region Method
        void CheckAccount(Account account)
        {
            txbDisplayName.Text = Convert.ToString(account.DisplayName);
            txbUserName.Text = Convert.ToString(account.UserName);
        }
        void UpdateAccount()
        {
            string displayName = txbDisplayName.Text;
            string userName = txbUserName.Text;
            string passWord = txbPassWord.Text;
            string newPassWord = txbNewPass.Text;
            string reEnterPass = txbReEnterPass.Text;

            if (!newPassWord.Equals(reEnterPass))
            {
                MessageBox.Show("Vui lòng nhập lại mật khẩu đúng mật khẩu mới!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            else
            {
                if (AccountDAO.Instance.UpdateAccount(userName, displayName, passWord, newPassWord))
                {
                    MessageBox.Show("Cập nhật tài khoản thành công!", "Thông báo", MessageBoxButtons.OK);
                    if (_updateAccountInfo != null)
                    {
                        _updateAccountInfo(this, new AccountEvent(AccountDAO.Instance.GetAccountByUserName(userName)));
                    }   
                }    
                else
                {
                    MessageBox.Show("Vui lòng nhập đúng mật khẩu!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }    
            }    
        }
        #endregion
        #region Events
        private event EventHandler<AccountEvent> _updateAccountInfo;
        public event EventHandler<AccountEvent> UpdateAccountInfo
        {
            add { _updateAccountInfo += value; }
            remove { _updateAccountInfo -= value; }
        }
        private void btnUpdate_Click(object sender, EventArgs e)
        {
            UpdateAccount();
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        #endregion
    }
    public class AccountEvent : EventArgs
    {
        private Account _account;

        public Account Account { get => _account; set => _account = value; }

        public AccountEvent(Account account)
        {
            this.Account = account;
        }
    }

}
