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
        private CategoryDAO() { }

        public List<Category> GetListCategory()
        {
            string query = "SELECT * FROM dbo.FoodCategory";
            List<Category> listCategory = new List<Category>();
            DataTable data = DataProvider.Instance.ExecuteQuery(query);
            foreach(DataRow row in data.Rows )
            {
                Category category = new Category(row);
                listCategory.Add(category);
            }
            return listCategory;

        }
    }
}
