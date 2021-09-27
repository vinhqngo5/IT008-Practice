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
        public Table(int id, string name, string status)
        {
            this.Id = id;
            this.Name = name;
            this.Status = status;
        }

        

        public Table(DataRow row)
        {
            this.Id = (int) row["id"];
            this.Name = row["nameTable"].ToString();
            this.Status = row["statusTable"].ToString();
        }

        private string status;

        public string Status
        {
            get { return status; }
            set { status = value; }
        }

        private string name;

        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        private int Id;
        public int ID
        {
            get { return Id; }
            set { Id = value; }
        }
    }
}
