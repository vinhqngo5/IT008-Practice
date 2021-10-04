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


        public bool InsertAccount(string userName, string displayName, bool type)
        {
            string query = $"INSERT dbo.Account (Username, DisplayName, Type) VALUES (N'{userName}', N'{displayName}', {Convert.ToInt32(type)})";

            int result = DataProvider.Instance.ExecuteNonQuery(query);

            return result > 0;
        }

        public bool UpdateAccount(string userName, string displayName, bool type)
        {
            string query = $"UPDATE dbo.Account SET DisplayName = N'{displayName}', Type = {Convert.ToInt32(type)} WHERE UserName = N'{userName}'";

            int result = DataProvider.Instance.ExecuteNonQuery(query);

            return result > 0;
        }
        public bool DeleteAccount(string userName)
        {
            string query = $"DELETE dbo.Account WHERE UserName = N'{userName}'";

            int result = DataProvider.Instance.ExecuteNonQuery(query);

            return result > 0;
        }

        public bool ResetPassword(string userName)
        {
            string query = $"UPDATE dbo.Account SET PassWord = N'0' WHERE UserName = N'{userName}'";

            int result = DataProvider.Instance.ExecuteNonQuery(query);

            return result > 0;
        }
    }
}
