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
        private static CategoryDAO _instance;

        public static CategoryDAO Instance 
        { 
            get => _instance ?? (_instance = new CategoryDAO()); 
            private set => _instance = value; 
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
    }
}
