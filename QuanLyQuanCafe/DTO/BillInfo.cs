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
        private int _id;
        private int _idBill;
        private int _idFood;
        private int _count;

        public int Id { get => _id; set => _id = value; }
        public int IdBill { get => _idBill; set => _idBill = value; }
        public int IdFood { get => _idFood; set => _idFood = value; }
        public int Count { get => _count; set => _count = value; }

        public BillInfo(int id, int idBill, int idFood, int count)
        {
            Id = id;
            idBill = idBill;
            IdFood = idFood;
            Count = count;
        }
        public BillInfo (DataRow row)
        {
            Id = (int)row["Id"];
            IdBill = (int)row["IdBill"];
            IdFood = (int)row["IdFood"];
            Count = (int)row["Count"];
        }
    }
}
