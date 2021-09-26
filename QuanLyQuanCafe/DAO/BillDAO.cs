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
        /// Thành công: Idbill
        /// Thất bại: -1
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public int  GetUncheckBillIdByTableId(int id)
        {
            bool status = false;
            DataTable data = DataProvider.Instance.ExecuteQuery("USP_GetUncheckBillByTableId @idTable , @status ",new object[] {id,status});
            if (data.Rows.Count > 0)
            {
                Bill bill = new Bill(data.Rows[0]);
                return bill.Id;
            }
            else
                return -1;
        }

    }
}
