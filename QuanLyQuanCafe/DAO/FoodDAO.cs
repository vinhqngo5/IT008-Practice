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

        public List<Food> GetFoodByCategoryId(int idCategory)
        {
            string query = "USP_GetFoodByCategoryId @idCategory ";
            List<Food> listFood = new List<Food>();
            DataTable data = DataProvider.Instance.ExecuteQuery(query, new object[] { idCategory });
            foreach (DataRow row in data.Rows)
            {
                listFood.Add(new Food(row));
            }
            return listFood;
        }

        public List<Food> GetListFood()
        {
            List<Food> listFood = new List<Food>();
            DataTable data = DataProvider.Instance.ExecuteQuery("USP_GetListFood");
            foreach (DataRow row in data.Rows)
            {
                Food food = new Food(row);
                food.IdCategory--;
                listFood.Add(food);
            }
            return listFood;
        }

        public bool InsertFood(string name, int idCategory, float price)
        {
            string query = $"INSERT dbo.Food (Name, IdCategory, Price) VALUES (N'{name}', {idCategory}, {price})";

            int result = DataProvider.Instance.ExecuteNonQuery(query);

            return result > 0;
        }

        public bool UpdateFood(int idFood, string name, int idCategory, float price)
        {
            string query = $"UPDATE dbo.Food SET Name = N'{name}', IdCategory = {idCategory}, Price = {price} WHERE Id = {idFood}";

            int result = DataProvider.Instance.ExecuteNonQuery(query);

            return result > 0;
        }

        public bool ChangeFoodStatusById(int idFood, bool status)
        {
            string query = $"UPDATE dbo.Food SET Status = {Convert.ToInt32(status)} WHERE Id = {idFood}";

            int result = DataProvider.Instance.ExecuteNonQuery(query);

            return result > 0;
        }
    }
}
