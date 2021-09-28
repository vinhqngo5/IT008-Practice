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
        private string _name;

        private int _id;

        public Category(int id, string name)
        {
            this.Id = id;
            this.Name = name;
        }

        public Category(DataRow row)
        {
            this.Id = Convert.ToInt32(row["id"]);
            this.Name = Convert.ToString(row["name"]);

        }

        public string Name { get => _name; set => _name = value; }
        public int Id { get => _id; set => _id = value; }
    }
}
