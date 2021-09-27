using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace QuanLyQuanCafe.DTO
{
    public class Menu
    {
        private string _foodName;
        private int _count;
        private float _price;
        private float _totalPrice;
        public string FoodName { get => _foodName; set => _foodName = value; }
        public int Count { get => _count; set => _count = value; }
        public float Price { get => _price; set => _price = value; }
        public float TotalPrice { get => _totalPrice; set => _totalPrice = value; }

        public Menu(DataRow row)
        {
            this.FoodName = Convert.ToString(row["Name"]);
            this.Price = Convert.ToSingle(row["Price"]);
            this.Count = Convert.ToInt32(row["Count"]);
            this.TotalPrice = Convert.ToSingle(row["TotalPrice"]);
        }
    }
}
