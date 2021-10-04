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
        private string _passWord;
        private string _displayName;
        private bool _type;

        public Account(DataRow row)
        {
            this.UserName = Convert.ToString(row["UserName"]);
            this.PassWord = Convert.ToString(row["PassWord"]);
            this.DisplayName = Convert.ToString(row["DisplayName"]);
            this.Type = Convert.ToBoolean(row["Type"]);
        }

        public string UserName { get => _userName; set => _userName = value; }
        public string PassWord { get => _passWord; set => _passWord = value; }
        public string DisplayName { get => _displayName; set => _displayName = value; }
        public bool Type { get => _type; set => _type = value; }
    }
}
