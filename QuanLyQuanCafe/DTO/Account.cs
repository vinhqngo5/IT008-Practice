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
        private int _type;
        public Account(DataRow row)
        {
            this.UserName = Convert.ToString(row["UserName"]);
            this.DisplayName = Convert.ToString(row["DisplayName"]);
            this.Type = Convert.ToInt32(row["Type"]);
            this.Password = Convert.ToString(row["PassWord"]);
        }
        public string UserName { get => _userName; set => _userName = value; }
        public string DisplayName { get => _displayName; set => _displayName = value; }
        public string Password { get => _password; set => _password = value; }
        public int Type { get => _type; set => _type = value; }
    }
}
