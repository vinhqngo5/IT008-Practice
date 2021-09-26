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
        public static int TableWidth = 95;
        public static int TableHeight = 95;

        private static TableDAO s_instance;

        public static TableDAO Instance 
        { 
            get => s_instance ?? (s_instance = new TableDAO()); 
            private set => s_instance = value; 
        }

        private TableDAO() { }

        public List<Table> LoadTableList()
        {
            List<Table> tableList = new List<Table>();
            DataTable data = DataProvider.Instance.ExecuteQuery("EXEC USP_GetTableList");

            foreach (DataRow row in data.Rows)
            {
                tableList.Add(new Table(row));
            }

            return tableList;
        }
    }
}
