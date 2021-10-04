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
            foreach (DataRow row in accountData.Rows)
                return new Account(row);
            return null;
        }
        public bool UpdateAccount(string userName, string displayName, string passWord, string newPass)
        {
            return DataProvider.Instance.ExecuteNonQuery("USP_UpdateAccount @userName , @displayName , @passWord , @newPassWord", new object[] { userName, displayName, passWord, newPass }) > 0;
        }
        public List<Account> GetListAccount()
        {
            List<Account> listAccount = new List<Account>();
            DataTable data = DataProvider.Instance.ExecuteQuery("USP_GetListAccount");
            foreach(DataRow row in data.Rows)
            {
                Account account = new Account(row);
                listAccount.Add(account);
            }
            return listAccount;
        }
        public DataTable GetListTypeAccount()
        {
            return DataProvider.Instance.ExecuteQuery("USP_GetListTypeAccount");
        }
        public bool InsertAccount(string userName, string displayName, bool type)
        {
            int result = DataProvider.Instance.ExecuteNonQuery("USP_InsertAccount @userName , @displayName , @type  ", new object[] { userName, displayName, type });
            return result > 0;
        }
        public bool UpdateAccountInAdminForm(string userName, string displayName, bool type)
        {
            int result = DataProvider.Instance.ExecuteNonQuery("USP_UpdateAccountInAdminForm @userName , @displayName , @type  ", new object[] { userName, displayName, type });
            return result > 0;
        }
        public bool DeleteAccount(string userName)
        {
            int result = DataProvider.Instance.ExecuteNonQuery("USP_DeleteAccount @userName ", new object[] { userName });
            return result > 0;
        }
        public bool ResetAccount(string userName)
        {
            int result = DataProvider.Instance.ExecuteNonQuery("USP_ResetAccount @userName ", new object[] { userName });
            return result > 0;
        }
    }
}
