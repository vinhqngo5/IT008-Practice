using QuanLyQuanCafe.DTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyQuanCafe.DAO
{
    public class MenuDAO
    {
        private static MenuDAO s_instance;

        public static MenuDAO Instance 
        { 
            get => s_instance ?? (s_instance = new MenuDAO()); 
            private set => s_instance = value; 
        }
        private MenuDAO() { }
        public List<Menu> GetListMenuByTable(int id)
        {
            List<Menu> listMenu = new List<Menu>();
            string query = "select f.Name, bi.count, f.Price, bi.Count*f.Price as TotalPrice from Bill b, BillInfo bi, Food f where b.Id = bi.IdBill and bi.IdFood = f.Id and b.Status=0 and b.IdTable =" + Convert.ToString(id);
            DataTable data = DataProvider.Instance.ExecuteQuery(query);
            foreach(DataRow dataRow in data.Rows)
            {
                Menu menu = new Menu(dataRow);
                listMenu.Add(menu);
            }    
            return listMenu;
        }
    }
}
