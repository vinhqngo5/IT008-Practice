using QuanLyQuanCafe.DTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyQuanCafe.DAO
{
    class BillInfoDAO
    {
        private static BillInfoDAO s_instance;

        public static BillInfoDAO Instance
        {
            get => s_instance ?? (s_instance = new BillInfoDAO());
            private set => s_instance = value;
        }

        private BillInfoDAO() { }

        public List<BillInfo> GetListBillInfo(int id)
        {
            List<BillInfo> listBillInfo = new List<BillInfo>();

            DataTable data = DataProvider.Instance.ExecuteQuery("SELECT * FROM dbo.BillInfo WHERE IdBill = " + id);

            foreach (DataRow item in data.Rows)
            {
                BillInfo billInfo = new BillInfo(item);
                listBillInfo.Add(billInfo);
            }

            return listBillInfo;
        }
    }
}
