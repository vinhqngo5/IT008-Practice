using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyQuanCafe.DTO
{
    public class Food
    {
        private int _id;
        private string _name;
        private int _idCategory;
        private float _price;

        public Food(int id, string name, int idCategory, float price)
        {
            this.Id = id;
            this.Name = name;
            this.IdCategory = idCategory;
            this.Price = price;
        }
        public Food(DataRow row)
        {
            this.Id = Convert.ToInt32(row["Id"]);
            this.Name = Convert.ToString(row["Name"]);
            this.IdCategory = Convert.ToInt32(row["IdCategory"]);
            this.Price = Convert.ToSingle(row["Price"]);
        }

        public int Id { get => _id; set => _id = value; }
        public string Name { get => _name; set => _name = value; }
        public int IdCategory { get => _idCategory; set => _idCategory = value; }
        public float Price { get => _price; set => _price = value; }
    }
}
