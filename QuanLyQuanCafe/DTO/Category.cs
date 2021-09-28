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
        private int _id;
        private string _name;

        public Category(int id, string name)
        {
            this.Id = id;
            this.Name = name;
        }
        public Category(DataRow row)
        {
            this.Id = Convert.ToInt32(row["Id"]);
            this.Name = Convert.ToString(row["Name"]);
        }
        public int Id { get => _id; set => _id = value; }
        public string Name { get => _name; set => _name = value; }
    }
}
