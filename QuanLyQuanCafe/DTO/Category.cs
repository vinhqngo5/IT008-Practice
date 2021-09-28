using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyQuanCafe.DTO
{
    public class Category
    {
        public Category(int id, string name)
        {
            this.ID = id;
            this.Name = name;
        }
        public Category(DataRow row)
        {
            this.ID = Convert.ToInt32(row["Id"]);
            this.Name = Convert.ToString(row["Name"]);
        }
        private string _name;
        private int _iD;

        public string Name { get => _name; set => _name = value; }
        public int ID { get => _iD; set => _iD = value; }
    }
}
