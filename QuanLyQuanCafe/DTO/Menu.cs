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

        public Menu(string foodName, int count, float price, float totalPrice=0)
        {
            FoodName = foodName;
            Count = count;
            Price = price;
            TotalPrice = totalPrice;    
        }
        public Menu(DataRow row)
        {
            FoodName = (string)row["Name"];
            Count = (int)row["count"];
            Price = (float)Convert.ToDouble(row["Price"].ToString());
            TotalPrice = (float)Convert.ToDouble(row["TotalPrice"].ToString());
        }
    }
}
