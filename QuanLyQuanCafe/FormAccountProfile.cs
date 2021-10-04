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
        private event EventHandler<AccountEvent> _updateAccount;
        public event EventHandler<AccountEvent> UpdateAccount
        {
            add {_updateAccount += value; }
            remove { _updateAccount -= value; }
        }


        public FormAccountProfile()
        {
            InitializeComponent();
        }
        #region Method
        public void LoadAccount(Account account)
        {
            _loginAccount = account;
            txbUserName.Text = _loginAccount.UserName;
            txbDisplayName.Text = _loginAccount.DisplayName;
        }

        void UpdateAccountInfo()
        {
            string userName = txbUserName.Text;
            string displayName = txbDisplayName.Text;
            string passWord = txbPassWord.Text;
            string newPass = txbNewPass.Text;
            string reEnterPass = txbReEnterPass.Text;

            if (newPass != reEnterPass)
            {
                MessageBox.Show("Vui lòng nhập lại mật khẩu đúng với mật khẩu mới!", "Không thành công");
            }
            else
            {
                if (AccountDAO.Instance.UpdateAccount(userName, displayName, passWord, newPass))
                {
                    MessageBox.Show("Cập nhật thành công", "Thành công");
                    _updateAccount?.Invoke(this, new AccountEvent(AccountDAO.Instance.GetAccountByUserName(userName)));
                }
                else
                {
                    MessageBox.Show("Vui lòng điền đúng mật khẩu", "Không thành công");
                }
            }
        }

        #endregion

        #region Events

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            UpdateAccountInfo();
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
