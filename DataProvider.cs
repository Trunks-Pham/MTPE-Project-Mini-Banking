using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Mini_Banking
{
    public class DataProvider
    {  
        //Singleton (cóa thể thay thế DataProvider cho phù hợp với các class)
        
        private static DataProvider instance;

        private string  connStr = "server=localhost;user=root;Database=minibankingdata;port=3306;password=";

        public static DataProvider Instance 
        { 
            get { if  (instance == null) { instance = new DataProvider(); } return instance; }
            private set { instance = value; }
        }
        private DataProvider() { }

        public DataTable ExecuteQuery(string querry)
        { 
            DataTable data = new DataTable();

            using (MySqlConnection connection = new MySqlConnection(connStr))
            {
                connection.Open();
                MySqlCommand cmd = new MySqlCommand(querry, connection);
                MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);
                adapter.Fill(data);
                connection.Close();
            }
            return data;
        }

          public int ExecuteNonQuery(string querry) 
          {
              int data = 0;
              using (MySqlConnection connection = new MySqlConnection(connStr))
              {
                  connection.Open();
                  MySqlCommand cmd = new MySqlCommand(querry, connection);
                  data = cmd.ExecuteNonQuery();
                  connection.Close();
              }
              return data;
          }
        public object ExecuteScalar(string querry)
        {
            object data = 0;
            using (MySqlConnection connection = new MySqlConnection(connStr))
            {
                connection.Open();
                MySqlCommand cmd = new MySqlCommand(querry, connection);
                data = cmd.ExecuteScalar();
                connection.Close();
            }
            return data;
        }
    }
}
