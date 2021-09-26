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
        private DateTime? _dateCheckIn;

        private DateTime? _dateCheckOut;

        private bool _status;

        private int _id;


        public Bill(int id, DateTime? dateCheckIn, DateTime? dateCheckOut, bool status)
        {
            this.Id = id;
            this.DateCheckOut = dateCheckOut;
            this.DateCheckIn = dateCheckIn;
            this.Status = status;
        }

        public Bill(DataRow row)
        {
            this.Id = (int)row["Id"];
            var dateCheckOutTemp = row["DateCheckOut"];
            if (dateCheckOutTemp.ToString() != "")
            {
                this.DateCheckOut = (DateTime?)dateCheckOutTemp;
            }
            this.DateCheckIn = (DateTime?)row["DateCheckIn"];
            this.Status = (bool)row["Status"];
        }


        public DateTime? DateCheckIn
        {
            get => _dateCheckIn;
            set => _dateCheckIn = value;
        }

        public DateTime? DateCheckOut
        {
            get => _dateCheckOut;
            set => _dateCheckOut = value;
        }
        public bool Status
        {
            get => _status;
            set => _status = value;
        }
        public int Id
        {
            get => _id;
            set => _id = value;
        }

    }
}
