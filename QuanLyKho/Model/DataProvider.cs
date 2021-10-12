namespace QuanLyKho.Model
{
    public class DataProvider
    {
        private static DataProvider _instance;

        public static DataProvider Instance => _instance ?? (_instance = new DataProvider());

        private DataProvider()
        {
            Database = new QUANLYKHOEntities();
        }

        public QUANLYKHOEntities Database { get; set; }
    }
}