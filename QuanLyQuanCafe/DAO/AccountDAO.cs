using QuanLyQuanCafe.DTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

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

        private string EncryptPassWord(string passWord)
        {
            byte[] hashPassWord = SHA256.Create().ComputeHash(Encoding.Unicode.GetBytes(passWord));
            string encryptedPass = "";
            foreach (byte item in hashPassWord)
            {
                encryptedPass += item;
            }
            return encryptedPass;
        }

        public bool Login(string userName, string passWord)
        {
            string query = "USP_Login @userName , @passWord";

            DataTable result = DataProvider.Instance.ExecuteQuery(query, new object[] { userName, EncryptPassWord(passWord) });

            return result.Rows.Count > 0;
        }

        public Account GetAccountByUserName(string userName)
        {
            DataTable accountData = DataProvider.Instance.ExecuteQuery("USP_GetAccountByUserName @userName", new object[] { userName });
            return accountData == null ? null : (new Account(accountData.Rows[0]));
        }

        public bool UpdateAccount(string userName, string displayName, string passWord, string newPass)
        {
            return DataProvider.Instance.ExecuteNonQuery("USP_UpdateAccount @userName , @displayName , @passWord , @newPassWord", new object[] { userName, displayName, EncryptPassWord(passWord), EncryptPassWord(newPass) }) > 0;
        }

        public DataTable GetListAccount()
        {
            return DataProvider.Instance.ExecuteQuery("USP_GetListAccount");
        }

        public bool InsertAccount(string userName, string displayName, bool type)
        {
            return DataProvider.Instance.ExecuteNonQuery("USP_InsertAccount @userName , @displayName , @type", new object[] { userName, displayName, type }) > 0;
        }

        public bool UpdateAccount(string userName, string displayName, bool type)
        {
            return DataProvider.Instance.ExecuteNonQuery("USP_EditAccount @userName , @displayName , @type", new object[] { userName, displayName, type }) > 0;
        }
        public bool DeleteAccount(string userName)
        {
            return DataProvider.Instance.ExecuteNonQuery("USP_DeleteAccount @userName", new object[] { userName }) > 0;
        }

        public bool ResetPassword(string userName)
        {
            return DataProvider.Instance.ExecuteNonQuery("USP_ResetPassword @userName", new object[] { userName }) > 0;
        }
    }
}
