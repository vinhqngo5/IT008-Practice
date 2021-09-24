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
        public int Id { get => _id; set => _id = value; }
        public bool Status { get => _status; set => _status = value; }
        public string Name { get => _name; set => _name = value; }
        
        public Table(DataRow row)
        {
            Id = (int)row["Id"];
            Name = (string)row["Name"];
            Status = (bool)row["Status"];
        }
        public Table(int id, string name, bool status)
        {
            Id = id;
            Name = name;
            Status = status;
        }

    }
}
