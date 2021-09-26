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
        private static MenuDAO _instance;

        public static MenuDAO Instance 
        { 
            get => _instance ?? (_instance = new MenuDAO()); 
            private set => _instance = value; 
        }
        private MenuDAO() { }
        public List<Menu> GetListMenuByTable(int id)
        {
            List<Menu> listMenu = new List<Menu>();
            DataTable data = DataProvider.Instance.ExecuteQuery("SELECT f.Name, BI.Count, f.Price, f.Price*BI.Count AS totalPrice FROM dbo.BillInfo AS BI, dbo.Bill AS b, dbo.Food AS f WHERE BI.IdBill = b.Id AND BI.IdFood = f.Id AND b.IdTable = "+ id);
            foreach (DataRow item in data.Rows)
            {
                Menu menu = new Menu(item);
                listMenu.Add(menu);
            }
            return listMenu;
        }
    }
}
