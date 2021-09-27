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

        public BillInfo(DataRow row)
        {
            this.Id = Convert.ToInt32(row["Id"]);
            this.IdBill = Convert.ToInt32(row["IdBill"]);
            this.IdFood = Convert.ToInt32(row["IdFood"]);
            this.Count = Convert.ToInt32(row["Count"]);
        }

        public int Id { get => _id; set => _id = value; }
        public int IdBill { get => _idBill; set => _idBill = value; }
        public int IdFood { get => _idFood; set => _idFood = value; }
        public int Count { get => _count; set => _count = value; }
    }
}
