using QuanLyQuanCafe.DTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyQuanCafe.DAO
{
    public class AccountDAO
    {
        private static AccountDAO s_instance;

        public static AccountDAO Instance
        {
            get => s_instance ?? (s_instance = new AccountDAO());
            private set => s_instance = value;
        }

        private AccountDAO() { }

        public bool Login(string userName, string passWord)
        {
            string query = "USP_Login @userName , @passWord";

            DataTable result = DataProvider.Instance.ExecuteQuery(query, new object[] { userName, passWord });

            return result.Rows.Count > 0;
        }

        public Account GetAccountByUserName(string userName)
        {
            DataTable accountData = DataProvider.Instance.ExecuteQuery("USP_GetAccountByUserName @userName", new object[] { userName });
            return accountData == null ? null : (new Account(accountData.Rows[0]));
        }

        public bool UpdateAccount(string userName, string displayName, string passWord, string newPass)
        {
            return DataProvider.Instance.ExecuteNonQuery("USP_UpdateAccount @userName , @displayName , @passWord , @newPassWord", new object[] { userName, displayName, passWord, newPass }) > 0;
        }

        public DataTable GetListAccount()
        {
            string query = "SELECT UserName, DisplayName, Type FROM dbo.Account";

            return DataProvider.Instance.ExecuteQuery(query);
        }

        public DataTable GetListAccountType()
        {
            string query = "SELECT DISTINCT Type FROM dbo.Account";

            return DataProvider.Instance.ExecuteQuery(query);
        }

    }
}
