namespace QuanLyKho.Model
{
    public class DataProvider
    {
        private static DataProvider s_instance;

        public static DataProvider Instance => s_instance ?? (s_instance = new DataProvider());

        private DataProvider()
        {
            Database = new QUANLYKHOEntities();
        }

        public QUANLYKHOEntities Database { get; set; }
    }
}
