using QuanLyQuanCafe.DTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyQuanCafe.DAO
{
    public class BillDAO
    {
        private static BillDAO s_instance;

        public static BillDAO Instance 
        { 
            get => s_instance ?? (s_instance = new BillDAO()); 
            private set => s_instance = value; 
        }

        private BillDAO() { }

        /// <summary>
        /// Return Bill Id if success else -1
        /// </summary>
        /// <param name="idTable"></param>
        /// <returns></returns>
        public int GetBillIdByTableId(int idTable)
        {
            DataTable dataBill = DataProvider.Instance.ExecuteQuery
                                    (
                                     "EXEC USP_GetBill @idTable", 
                                     new object[] { idTable }
                                    );
            return (dataBill.Rows.Count > 0) ? new Bill(dataBill.Rows[0]).Id : -1;
        }
        public void CheckOut(int id)
        {
            string query = "UPDATE dbo.Bill SET Status = 1 WHERE Id = " + id;
            DataProvider.Instance.ExecuteNonQuery(query);
        }
        public int GetUnCheckBillIdByTableId(int idTable)
        {
            DataTable dataBill = DataProvider.Instance.ExecuteQuery
                                    (
                                     "EXEC USP_GetUnCheckBill @idTable",
                                     new object[] { idTable }
                                    );
            return (dataBill.Rows.Count > 0) ? new Bill(dataBill.Rows[0]).Id : -1;
        }
        public void InsertBill(int id)
        {
            string query = "EXEC USP_InsertBill @idTable";
            DataProvider.Instance.ExecuteNonQuery(query, new object[] { id });
        }
        public int GetMaxIDBill()
        {
            try
            {
                return (int)DataProvider.Instance.ExecuteScalar("SELECT MAX(Id) FROM dbo.Bill");
            }
            catch
            {
                return 1;
            }
        }
    }
}
