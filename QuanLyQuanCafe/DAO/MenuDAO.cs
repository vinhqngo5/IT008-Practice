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

        public List<Menu> GetListMenuByTable(int idTable)
        {
            List<Menu> listMenu = new List<Menu>();
            DataTable dataMenu = DataProvider.Instance.ExecuteQuery("EXEC USP_GetMenu @idTable", new object[] { idTable });
            foreach (DataRow row in dataMenu.Rows)
            {
                listMenu.Add(new Menu(row));
            }
            return listMenu;
        }
    }
}
