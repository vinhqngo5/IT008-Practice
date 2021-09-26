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
            set => s_instance = value;
        }

        public List<Menu> GetListMenyByTable(int id)
        {
            List<Menu> listMenu = new List<Menu>();

            string query = "SELECT f.Name, bi.Count, f.Price, f.Price * bi.[Count] AS TotalPrice FROM dbo.BillInfo AS bi, dbo.Bill AS b, dbo.Food AS f WHERE bi.IdBill = b.Id AND bi.IdFood = f.Id AND b.IdTable = " + id;

            DataTable data = DataProvider.Instance.ExecuteQuery(query);

            foreach (DataRow item in data.Rows)
            {
                Menu menu = new Menu(item);
                listMenu.Add(menu);
            }

            return listMenu;
        }
    }
}
