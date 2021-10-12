namespace QuanLyKho.Model
{
    public class DataProvider
    {
        private DataProvider _instance;

        public DataProvider Instance => _instance ?? (_instance = new DataProvider());

        private DataProvider()
        {
            Database = new QUANLYKHOEntities();
        }

        public QUANLYKHOEntities Database { get; set; }
    }
}