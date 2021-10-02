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
        
        public bool UpdateAccount(string userName, string displayName, string pass, string newPass)
        {
            int result = DataProvider.Instance.ExecuteNonQuery("USP_UpdateAccount @userName , @displayName , @pass , @newPass", new object[] { userName, displayName, pass, newPass });
            return result > 0;
        }
        public Account GetAccountByUserName(string userName)
        {
            DataTable dataAccount = DataProvider.Instance.ExecuteQuery("USP_GetAccountByUserName @userName", new object[] { userName });
            foreach (DataRow row in dataAccount.Rows)
            {
                return new Account(row);
            }
            return null;
        }
    }
}
