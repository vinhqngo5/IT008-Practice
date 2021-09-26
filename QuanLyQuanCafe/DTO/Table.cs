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
        public Table (int id, string name, string status)
        {
            this.ID = id;
            this.Name = name;
            this.Status = status;
        }
        public Table(DataRow row)
        {
            this.ID = (int)row["Id"];
            this.Name = row["Name"].ToString();
            this.Status = row["status"].ToString();

        }
        private int _iD;

        public int ID 
        { 
            get => _iD; 
            set => _iD = value; 
        }
        private string _name;
        public string Name 
        { 
            get => _name; 
            set => _name = value; 
        }
        private string _status;
        public string Status 
        { 
            get => _status; 
            set => _status = value; 
        }

        

        
    }
}
