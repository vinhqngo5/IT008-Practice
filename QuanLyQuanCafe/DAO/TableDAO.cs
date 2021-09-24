using QuanLyQuanCafe.DTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyQuanCafe.DAO
{
    public class TableDAO
    {
        private static TableDAO s_instance;

        internal static TableDAO Instance 
        {
            get => s_instance ?? (s_instance = new TableDAO());
            private set => s_instance = value; 
        }

        public static int TableWidth = 90;
        public static int TableHeight = 90;
        private TableDAO() { }

        public List<Table> LoadTableList()
        {
            List<Table> tablelist = new List<Table>();
            DataTable data = DataProvider.Instance.ExecuteQuery("USP_GetTableList");
            foreach (DataRow dataRow in data.Rows)
            {
                Table table = new Table(dataRow);
                tablelist.Add(table);
            }
            return tablelist;
        }
    }
}
