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

        public DataTable GetListAccount()
        {
            return DataProvider.Instance.ExecuteQuery("USP_GetListAccount");
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

        public bool InsertAccount(string userName, string displayName, bool type)
        {
            string query = "USP_InsertAccount @userName , @displayName , @type";
            int result = DataProvider.Instance.ExecuteNonQuery(query, new object[] { userName, displayName, type});
            return result > 0;
        }
        public bool EditAccount(string userName, string displayName, bool type)
        {
            string query = "USP_EditAccount @userName , @displayName , @type";
            int result = DataProvider.Instance.ExecuteNonQuery(query, new object[] { userName, displayName, type});
            return result > 0;
        }
        public bool DeleteAccount(string userName)
        {
            string query = "USP_DeleteAccount @userName";
            int result = DataProvider.Instance.ExecuteNonQuery(query, new object[] { userName});
            return result > 0;
        }
        public bool ResetPassWord(string userName)
        {
            string query = "USP_ResetPassWord @userName";
            int result = DataProvider.Instance.ExecuteNonQuery(query, new object[] { userName });
            return result > 0;
        }
    }
}
