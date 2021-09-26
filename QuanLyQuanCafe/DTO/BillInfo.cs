using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyQuanCafe.DTO
{
    public class BillInfo
    {
        public BillInfo(int id, int billID, int foodID, int count)
        {
            this.ID = id;
            this.BillID = billID;
            this.FoodID = foodID;
            this.Count = count;
        }
        public BillInfo(DataRow row)
        {
            this.ID = (int)row["Id"];
            this.BillID = (int)row["IdBill"];
            this.FoodID = (int)row["IdFood"];
            this.Count = (int)row["Count"];
        }
        private int _iD;
        private int _billID;
        private int _foodID;
        private int _count;
        public int ID { get => _iD; set => _iD = value; }
        public int BillID { get => _billID; set => _billID = value; }
        public int FoodID { get => _foodID; set => _foodID = value; }
        public int Count { get => _count; set => _count = value; }
    }
}
