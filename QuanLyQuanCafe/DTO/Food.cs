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
        public Food(int id, string name, int categoryID, float price)
        {
            this.ID = id;
            this.Name = name;
            this.CategoryID = categoryID;
            this.Price = price;
        }
        public Food(DataRow row)
        {
            this.ID = Convert.ToInt32(row["Id"]);
            this.Name = Convert.ToString(row["Name"]);
            this.CategoryID = Convert.ToInt32(row["IdCategory"]);
            this.Price = Convert.ToSingle(row["Price"]);
        }
        private int _iD;
        private string _name;
        private int _categoryID;
        private float _price;

        public int ID { get => _iD; set => _iD = value; }
        public string Name { get => _name; set => _name = value; }
        public int CategoryID { get => _categoryID; set => _categoryID = value; }
        public float Price { get => _price; set => _price = value; }
    }
}
