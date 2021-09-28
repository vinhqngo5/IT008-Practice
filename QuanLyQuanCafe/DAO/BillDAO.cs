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

        public void Checkout(int id)
        {
            string query = "UPDATE dbo.Bill SET STATUS = 1 WHERE Id = " + id;
            DataProvider.Instance.ExecuteNonQuery(query);
        }

        public void InsertBill(int id)
        {
            DataProvider.Instance.ExecuteNonQuery("USP_InsertBill @idTable", new object[] { id });
        }

        public int GetMaxIdBill()
        {
            try
            {
                return Convert.ToInt32(DataProvider.Instance.ExecuteScalar("SELECT MAX(Id) FROM dbo.Bill"));
            }
            catch (Exception ex)
            {
                return 1;
            }
        }

    }
}
