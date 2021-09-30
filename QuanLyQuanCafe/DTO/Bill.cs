using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyQuanCafe.DTO
{
    public class Bill
    {
        private int _id;
        private DateTime? _dateCheckIn;
        private DateTime? _dateCheckOut;
        private int _idTable;
        private bool _status;
        private int _discount;

        public Bill(DataRow row)
        {
            this.Id = Convert.ToInt32(row["Id"]);
            this.DateCheckIn = (DateTime?)row["DateCheckIn"];
            if (row["DateCheckOut"].ToString() != "")
            {
                this.DateCheckOut = (DateTime?)row["DateCheckOut"];
            }
            else
            {
                this.DateCheckOut = null;
            }
            this.IdTable = Convert.ToInt32(row["IdTable"]);
            this.Status = Convert.ToBoolean(row["Status"]);
            this.Discount = Convert.ToInt32(row["Discount"]);
        }

        public int Id { get => _id; set => _id = value; }
        public DateTime? DateCheckIn { get => _dateCheckIn; set => _dateCheckIn = value; }
        public DateTime? DateCheckOut { get => _dateCheckOut; set => _dateCheckOut = value; }
        public int IdTable { get => _idTable; set => _idTable = value; }
        public bool Status { get => _status; set => _status = value; }
        public int Discount { get => _discount; set => _discount = value; }
    }
}
