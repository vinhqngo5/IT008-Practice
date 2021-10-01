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
        private string _passWord;
        private bool _type;
        public Account(DataRow row)
        {
            this.UserName = Convert.ToString(row["UserName"]);
            this.DisplayName = Convert.ToString(row["DisplayName"]);
            this.PassWord = Convert.ToString(row["PassWord"]);
            this.Type = Convert.ToBoolean(row["Type"]);
        }
        public string UserName { get => _userName; set => _userName = value; }
        public string DisplayName { get => _displayName; set => _displayName = value; }
        public string PassWord { get => _passWord; set => _passWord = value; }
        public bool Type { get => _type; set => _type = value; }
    }
}
