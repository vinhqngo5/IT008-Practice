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
        {
            get => _loginAccount;
            set
            {
                _loginAccount = value;
                ChangeAccount(LoginAccount);
            }
        }

        public FormAccountProfile(Account loginAccount)
        {

            InitializeComponent();

            this.LoginAccount = loginAccount;
        }

        void ChangeAccount(Account loginAccount)
        {
            txbUserName.Text = loginAccount.UserName;
            txbDisplayName.Text = loginAccount.DisplayName;

        }

        void UpdateAccountInfo()
        {
            string displayName = txbDisplayName.Text;
            string password = txbPassWord.Text;
            string newpass = txbNewPass.Text;
            string reenterPass = txbReEnterPass.Text;
            string userName = txbUserName.Text;

            if (!newpass.Equals(reenterPass))
            {
                MessageBox.Show("Vui lòng nhập lại mật khẩu đúng với mật khẩu mới", "Cập nhật");
            }
            else
            {
                if (AccountDAO.Instance.UpdateAccount(userName, displayName, password, newpass))
                {
                    MessageBox.Show("Cập nhật thành công!", "Cập nhật");

                    _updateAccount?.Invoke(this, new AccountEvent(AccountDAO.Instance.GetAccountByUserName(userName)));
                }
                else
                {
                    MessageBox.Show("Vui lòng nhập đúng mật khẩu", "Cập nhật");
                }
            }
        }

        // event and event accessor in c#
        private event EventHandler<AccountEvent> _updateAccount;
        public event EventHandler<AccountEvent> UpdateAccount
        {
            add { _updateAccount += value; }
            remove { _updateAccount -= value; }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            UpdateAccountInfo();
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        public class AccountEvent : EventArgs
        {
            private Account _acc;

            public AccountEvent(Account acc)
            {
                this.Acc = acc;
            }

            public Account Acc { get => _acc; set => _acc = value; }
        }
    }
}
