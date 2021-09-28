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

        private int _categoryId;

        private float _price;

        public Food(int id, string name, int categoryId, float price)
        {
            this.Id = id;
            this.Name = name;
            this.CategoryId = categoryId;
            this.Price = price;
        }

        public Food(DataRow row)
        {
            this.Id = Convert.ToInt32(row["id"]);
            this.Name = Convert.ToString(row["name"]);
            this.CategoryId = Convert.ToInt32(row["IdCategory"]);
            this.Price = (float)Convert.ToDouble(row["price"]);
        }

        public int Id { get => _id; set => _id = value; }
        public string Name { get => _name; set => _name = value; }
        public int CategoryId { get => _categoryId; set => _categoryId = value; }
        public float Price { get => _price; set => _price = value; }
    }
}
