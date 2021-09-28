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
        private static FoodDAO s_instance;

        public static FoodDAO Instance 
        { 
            get => s_instance ?? (s_instance = new FoodDAO()); 
            private set => s_instance = value; 
        }
        private FoodDAO() { }

        public List<Food> GetListFoodByIdCategory(int idCategory)
        {
            string query = "USP_GetListFoodByIdCategory @idCategory ";
            List<Food> listFood = new List<Food>();
            DataTable data = DataProvider.Instance.ExecuteQuery(query, new object[] { idCategory });
            foreach(DataRow row in data.Rows)
            {
                Food food = new Food(row);
                listFood.Add(food);
            }
            return listFood;
        }
    }

}
