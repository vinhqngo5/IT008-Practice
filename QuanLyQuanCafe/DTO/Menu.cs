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
        public Menu(string foodName, int count, float price, float totalPrice = 0)
        {
            this.FoodName = foodName;
            this.Count = count;
            this.Price = price;
            this.TotalPrice = totalPrice;
        }

        public Menu(DataRow row)
        {
            this.FoodName = row["Name"].ToString();
            this.Count = (int)row["Count"];
            this.Price = (float)Convert.ToDouble(row["Price"].ToString());
            this.TotalPrice = (float)Convert.ToDouble(row["TotalPrice"].ToString());
        }

        private string _foodName;

        private int _count;

        private float _price;

        private float _totalPrice;

        public string FoodName
        {
            get => _foodName;
            set => _foodName = value;
        }

        public int Count
        {
            get => _count;
            set => _count = value;
        }

        public float Price
        {
            get => _price;
            set => _price = value;
        }

        public float TotalPrice
        {
            get => _totalPrice;
            set => _totalPrice = value;
        }

    }
}
