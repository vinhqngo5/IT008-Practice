using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyQuanCafe.DTO
{
    public class Account
    {
        private string _userName;

        private string _displayName;

        private string _password;

        private bool _type;

        public Account(string userName, string displayName, bool type, string password = null)
        {
            this.DisplayName = displayName;
            this.UserName = userName;
            this.Password = password;
            this.Type = type;
        }

        public Account(DataRow row)
        {
            this.DisplayName = Convert.ToString(row["displayName"]);
            this.UserName = Convert.ToString(row["userName"]);
            this.Password = Convert.ToString(row["password"]);
            this.Type = Convert.ToBoolean(row["type"]);
        }
        public string UserName { get => _userName; set => _userName = value; }
        public string DisplayName { get => _displayName; set => _displayName = value; }
        public string Password { get => _password; set => _password = value; }
        public bool Type { get => _type; set => _type = value; }
    }
}
