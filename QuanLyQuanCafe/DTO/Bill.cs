using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyQuanCafe.DTO
{
    class Bill
    {
        private int _id;
        private DateTime _dateCheckIn;
        private DateTime _dateCheckOut;
        private int _idTable;
        private bool status;

        public int Id { get => _id; set => _id = value; }
        public DateTime DateCheckIn { get => _dateCheckIn; set => _dateCheckIn = value; }
        public DateTime DateCheckOut { get => _dateCheckOut; set => _dateCheckOut = value; }
        public int IdTable { get => _idTable; set => _idTable = value; }
        public bool Status { get => status; set => status = value; }

        public Bill(int id, DateTime dateCheckIn, DateTime dateCheckOut, int idTable, bool status)
        {
            Id = id;
            DateCheckIn = dateCheckIn;
            DateCheckOut = dateCheckOut;
            IdTable = idTable;
            Status = status;
        }
        public Bill (DataRow row)
        {
            Id = (int)row["Id"];
            DateCheckIn = (DateTime)row["DateCheckIn"];
            var dataCheckOutTemp = row["DateCheckOut"];
            if (dataCheckOutTemp.ToString() !="")
                DateCheckOut = (DateTime)dataCheckOutTemp;
            IdTable = (int)row["IdTable"];
            Status = (bool)row["Status"];
        }
    }
}
