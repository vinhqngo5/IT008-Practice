using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyQuanCafe.DTO
{
    public class Food
    {
        private int _id;
        private int _idCategory;
        private string _name;
        private float _price;
        private bool _status;

        public Food(DataRow row)
        {
            this.Id = Convert.ToInt32(row["Id"]);
            this.IdCategory = Convert.ToInt32(row["IdCategory"]);
            this.Name = Convert.ToString(row["Name"]);
            this.Price = Convert.ToSingle(row["Price"]);
            this.Status = Convert.ToBoolean(row["Status"]);
        }

        public Food(int id, string name, int idCategory, float price, bool status)
        {
            this.Id = id;
            this.Name = name;
            this.IdCategory = idCategory;
            this.Price = price;
            this.Status = status;
        }

        public int Id { get => _id; set => _id = value; }
        public int IdCategory { get => _idCategory; set => _idCategory = value; }
        public string Name { get => _name; set => _name = value; }
        public float Price { get => _price; set => _price = value; }
        public bool Status { get => _status; set => _status = value; }
    }
}
