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

        public void InsertBill(int idTable)
        {
            DataProvider.Instance.ExecuteQuery("EXEC USP_InsertBill @idTable", new object[] { idTable });
        }

        public void CheckOut(int idBill, int discount, float totalPrice)
        {
            DataProvider.Instance.ExecuteNonQuery("USP_CheckOut @idBill , @discount , @totalPrice", new object[] { idBill, discount, totalPrice });
        }

        public DataTable GetListBillByDate(DateTime dateCheckIn, DateTime dateCheckOut)
        {
            return DataProvider.Instance.ExecuteQuery("USP_GetListBillByDate @dateCheckIn , @dateCheckOut", new object[] { dateCheckIn, dateCheckOut });
        }

        public DataTable GetListBillByDateAndPage(DateTime dateCheckIn, DateTime dateCheckOut, int pageNum)
        {
            return DataProvider.Instance.ExecuteQuery("USP_GetListBillByDateAndPage @dateCheckIn , @dateCheckOut , @page", new object[] { dateCheckIn, dateCheckOut, pageNum });
        }

        public int GetNumBillByDate(DateTime dateCheckIn, DateTime dateCheckOut)
        {
            return (int)DataProvider.Instance.ExecuteScalar("USP_GetNumBillByDate @dateCheckIn , @dateCheckOut", new object[] { dateCheckIn, dateCheckOut });
        }
    }
}
