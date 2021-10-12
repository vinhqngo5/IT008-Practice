using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyKho.Model
{
    public class DataProvider
    {
        private static DataProvider s_instance;

        public static DataProvider Instance 
        { 
            get => s_instance ?? (s_instance = new DataProvider()); 
            private set => s_instance = value; 
        }
        private DataProvider() 
        {
            DB = new QUANLYKHOEntities();
        }
        public QUANLYKHOEntities DB { get; set; }
    }
}
