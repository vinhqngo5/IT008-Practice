using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyQuanCafe.DAO
{
    public class DataProvider
    {
        private static DataProvider _instance;

        public static DataProvider Instance 
        {
            get => _instance ?? (_instance = new DataProvider());

            private set => _instance = value; 
        }

        private DataProvider() { }

        private readonly string _connectionStr = "Data Source=.\\;Initial Catalog=QUANLYQUANCAFE;Integrated Security=True";

        public DataTable ExecuteQuery(string query, object[] parameter = null)
        {
            DataTable data = new DataTable();

            using (SqlConnection connection = new SqlConnection(_connectionStr)) {
                connection.Open();

                SqlCommand command = new SqlCommand(query, connection);

                if (parameter != null)
                {
                    string[] listQueryString = query.Split(' ');
                    int i = 0;
                    foreach (string queryItem in listQueryString)
                    {
                        if (queryItem.Contains('@'))
                        {
                            command.Parameters.AddWithValue(queryItem, parameter[i]);
                            i++;
                        }
                    }
                }

                SqlDataAdapter adapter = new SqlDataAdapter(command);

                adapter.Fill(data);

                connection.Close();
            }

            return data;
        }
        public int ExecuteNonQuery(string query, object[] parameter = null)
        {
            int data = 0;

            using (SqlConnection connection = new SqlConnection(_connectionStr))
            {
                connection.Open();

                SqlCommand command = new SqlCommand(query, connection);

                if (parameter != null)
                {
                    string[] listQueryString = query.Split(' ');
                    int i = 0;
                    foreach (string queryItem in listQueryString)
                    {
                        if (queryItem.Contains('@'))
                        {
                            command.Parameters.AddWithValue(queryItem, parameter[i]);
                            i++;
                        }
                    }
                }

                data = command.ExecuteNonQuery();

                connection.Close();
            }

            return data;
        }
        public object ExecuteScalar(string query, object[] parameter = null)
        {
            object data = new object();

            using (SqlConnection connection = new SqlConnection(_connectionStr))
            {
                connection.Open();

                SqlCommand command = new SqlCommand(query, connection);

                if (parameter != null)
                {
                    string[] listQueryString = query.Split(' ');
                    int i = 0;
                    foreach (string queryItem in listQueryString)
                    {
                        if (queryItem.Contains('@'))
                        {
                            command.Parameters.AddWithValue(queryItem, parameter[i]);
                            i++;
                        }
                    }
                }

                data = command.ExecuteScalar();

                connection.Close();
            }

            return data;
        }
    }
}
