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
        static private FoodDAO s_instance;

        public static FoodDAO Instance
        {
            get => s_instance ?? (s_instance = new FoodDAO());
            set => s_instance = value;
        }

        private FoodDAO() { }

        public List<Food> GetFoodByCategoryId(int id)
        {
            List<Food> list = new List<Food>();

            string query = "SELECT * FROM Food WHERE IdCategory = " + id;

            DataTable data = DataProvider.Instance.ExecuteQuery(query);

            foreach (DataRow item in data.Rows)
            {
                Food food = new Food(item);
                list.Add(food);
            }

            return list;
        }
    }
}
