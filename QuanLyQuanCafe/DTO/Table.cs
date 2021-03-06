using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyQuanCafe.DTO
{
    public class Table
    {
        private int _id;
        private string _name;
        private bool _status;

        public Table(DataRow row)
        {
            this.Id = Convert.ToInt32(row["Id"]);
            this.Name = Convert.ToString(row["Name"]);
            this.Status = Convert.ToBoolean(row["Status"]);
        }

        public int Id { get => _id; set => _id = value; }
        public string Name { get => _name; set => _name = value; }
        public bool Status { get => _status; set => _status = value; }
    }
}
