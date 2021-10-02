using QuanLyQuanCafe.DTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyQuanCafe.DAO
{
    public class CategoryDAO
    {
        private static CategoryDAO s_instance;

        public static CategoryDAO Instance 
        { 
            get => s_instance ?? (s_instance = new CategoryDAO()); 
            private set => s_instance = value; 
        }

        public List<Category> GetListCategories()
        {
            List<Category> listCategories = new List<Category>();
            DataTable dataCategories = DataProvider.Instance.ExecuteQuery("SELECT * FROM FoodCategory");
            foreach (DataRow row in dataCategories.Rows)
            {
                listCategories.Add(new Category(row));
            }
            return listCategories;
        }
        public Category GetCategoryById (int id)
        {
            Category category = null;
            DataTable dataCategories = DataProvider.Instance.ExecuteQuery("USP_GetListFoodById @id", new object[] { id});
            foreach (DataRow row in dataCategories.Rows)
            {
                category = new Category(row);
                return category;
            }
            return category;
        }
    }
}
