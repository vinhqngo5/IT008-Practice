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
        public Bill(int id, DateTime? dateCheckIn, DateTime? dateCheckOut, bool status)
        {
            this.ID = id;
            this.DateCheckIn = DateCheckIn;
            this.DateCheckOut = DateCheckOut;
            this.Status = status;
        }
        public Bill(DataRow row)
        {
            this.ID = (int)row["Id"];
            this.DateCheckIn = (DateTime?)row["DateCheckIn"];
            this.DateCheckOut = (DateTime?)row["DateCheckOut"];
            var dateCheckOutTemp = row["DateCheckOut"];
            if (dateCheckOutTemp.ToString() != "")
                this.DateCheckOut = (DateTime?)dateCheckOutTemp;
            this.Status = (bool)row["Status"];
        }
        private bool _status;
        private DateTime? _dateCheckOut;
        private DateTime? _dateCheckIn; 
        private int _iD;

        public int ID { get => _iD; set => _iD = value; }
        public DateTime? DateCheckIn { get => _dateCheckIn; set => _dateCheckIn = value; }
        public bool Status { get => _status; set => _status = value; }
        public DateTime? DateCheckOut { get => _dateCheckOut; set => _dateCheckOut = value; }
    }
}
