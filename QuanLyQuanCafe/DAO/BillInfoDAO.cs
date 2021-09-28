using QuanLyQuanCafe.DTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyQuanCafe.DAO
{
    public class BillInfoDAO
    {
        private static BillInfoDAO s_instance;

        public static BillInfoDAO Instance 
        { 
            get => s_instance ?? (s_instance = new BillInfoDAO()); 
            private set => s_instance = value; 
        }

        public List<BillInfo> GetListBillInfo(int idBill)
        {
            DataTable dataBillInfo = DataProvider.Instance.ExecuteQuery
                        (
                         "EXEC USP_GetBillInfo @idBill",
                         new object[] { idBill }
                        );
            List<BillInfo> listBillInfo = new List<BillInfo>();
            foreach (DataRow row in dataBillInfo.Rows)
            {
                listBillInfo.Add(new BillInfo(row));
            }
            return listBillInfo;
        }
        public void InsertBillInfo(int idBill, int idFood, int count)
        {
            DataProvider.Instance.ExecuteNonQuery("USP_InsertBillInfo @idBill , @idFood , @count", new object[] { idBill,idFood,count});
        }
    }
}
