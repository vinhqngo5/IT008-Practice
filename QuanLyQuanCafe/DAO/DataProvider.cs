using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace QuanLyQuanCafe.DAO
{
    public class DataProvider
    {
        private string _connectionSTR = @"Data Source=.\;Initial Catalog=QUANLYQUANCAFE;Integrated Security=True";
        private static DataProvider s_instance;

        public static DataProvider Instance 
        { 
            get { if (s_instance == null) s_instance = new DataProvider(); return DataProvider.s_instance;}
            private set => s_instance = value; 
        }
        private DataProvider() { }
        public DataTable ExecuteQuery(string query, object[] parameter = null)
        {
            DataTable data = new DataTable();

            using (SqlConnection connection = new SqlConnection(_connectionSTR)) 
            {
                connection.Open();
                SqlCommand command = new SqlCommand(query, connection);

                if (parameter!=null)
                {
                    string[] listPara = query.Split(' ');
                    int i = 0;
                    foreach (string item in listPara)
                    {
                        if (item.Contains('@'))
                        {
                            command.Parameters.AddWithValue(item, parameter[i]);
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

            using (SqlConnection connection = new SqlConnection(_connectionSTR))
            {
                connection.Open();
                SqlCommand command = new SqlCommand(query, connection);

                if (parameter != null)
                {
                    string[] listPara = query.Split(' ');
                    int i = 0;
                    foreach (string item in listPara)
                    {
                        if (item.Contains('@'))
                        {
                            command.Parameters.AddWithValue(item, parameter[i]);
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
            object data = 0;

            using (SqlConnection connection = new SqlConnection(_connectionSTR))
            {
                connection.Open();
                SqlCommand command = new SqlCommand(query, connection);

                if (parameter != null)
                {
                    string[] listPara = query.Split(' ');
                    int i = 0;
                    foreach (string item in listPara)
                    {
                        if (item.Contains('@'))
                        {
                            command.Parameters.AddWithValue(item, parameter[i]);
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
