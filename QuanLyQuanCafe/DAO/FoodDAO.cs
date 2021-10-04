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
            return DataProvider.Instance.ExecuteNonQuery("USP_InsertFood @name , @idCategory , @price", new object[] { name, idCategory, price }) > 0;
        }

        public bool UpdateFood(int id, string name, int idCategory, float price)
        {
            return DataProvider.Instance.ExecuteNonQuery("USP_UpdateFood @id , @name , @idCategory , @price", new object[] { id, name, idCategory, price }) > 0;
        }

        public bool DeleteFood(int id)
        {
            return DataProvider.Instance.ExecuteNonQuery("USP_DeleteFood @id", new object[] { id }) > 0;
        }

        public List<Food> SearchFoodByName(string name)
        {
            List<Food> list = new List<Food>();

            DataTable data = DataProvider.Instance.ExecuteQuery("USP_SearchFood @name", new object[] { name });

            foreach (DataRow row in data.Rows)
            {
                list.Add(new Food(row));
            }
            return list;
        }
    }
}
