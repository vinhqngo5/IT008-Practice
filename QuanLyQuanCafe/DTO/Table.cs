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
        private string _name;
        private bool _status;
        private int _id;

        public Table(int id, string name, bool status)
        {
            this.Id = id;
            this.Name = name;
            this.Status = status;
        }

        public Table(DataRow row)
        {
            this.Id = (int)row["Id"];
            this.Name = row["Name"].ToString();
            this.Status = Convert.ToBoolean(row["Status"]);
        }

        public string Name { get => _name; set => _name = value; }
        public bool Status { get => _status; set => _status = value; }
        public int Id { get => _id; set => _id = value; }
    }
}
