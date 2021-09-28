using QuanLyQuanCafe.DTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyQuanCafe.DAO
{
    public class FoodDAO
    {
        private static FoodDAO _instance;

        public static FoodDAO Instance
        {
            get => _instance ?? (_instance = new FoodDAO());
            private set => _instance = value;
        }

        public List<Food> GetFoodByCategoryId(int idCategory)
        {
            List<Food> listFood = new List<Food>();
            DataTable dataFood = DataProvider.Instance.ExecuteQuery("SELECT * FROM Food WHERE IdCategory = " + idCategory);
            foreach (DataRow row in dataFood.Rows)
            {
                listFood.Add(new Food(row));
            }
            return listFood;
        }
    }
}
