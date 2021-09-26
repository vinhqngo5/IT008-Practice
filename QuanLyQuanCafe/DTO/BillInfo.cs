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
        public BillInfo(int id, int billId, int foodId, int count)
        {
            this.Id = id;
            this.BillId = billId;
            this.FoodId = foodId;
            this.Count = count;
        }

        public BillInfo(DataRow row)
        {
            this.Id = (int)row["Id"];
            this.BillId = (int)row["IdBill"];
            this.FoodId = (int)row["IdFood"];
            this.Count = (int)row["Count"];
        }

        private int _id;

        private int _billId;

        private int _foodId;

        private int _count;

        public int Id
        {
            get => _id;
            set => _id = value;
        }

        public int BillId
        {
            get => _billId;
            set => _billId = value;
        }

        public int FoodId
        {
            get => _foodId;
            set => _foodId = value;
        }

        public int Count
        {
            get => _count;
            set => _count = value;
        }
    }
}
