using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyKho.Model
{
    public class DataProvider
    {
        private static DataProvider s_ins;

        public static DataProvider Ins 
        {
            get => s_ins ?? (s_ins = new DataProvider());
            set => s_ins = value; 
        }
        public QUANLYKHOEntities DB { get; set; }
        private DataProvider()
        {
            DB = new QUANLYKHOEntities();
        }
    }
}
