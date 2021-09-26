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
        private BillInfoDAO() { }

        public List<BillInfo> GetListBillInfo(int idBill)
        {
            List<BillInfo> listBillInfo = new List<BillInfo>();
            DataTable data = DataProvider.Instance.ExecuteQuery("USP_GetBillInfoByIdBill @idBill ", new object[] { idBill});
            foreach(DataRow dataRow in data.Rows)
            {
                BillInfo billInfo = new BillInfo(dataRow);
                listBillInfo.Add(billInfo);
            }
            return listBillInfo;
        }
    }
}
