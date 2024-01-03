using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace ServerAvalonia.Data
{
    //test -- всё это просто тестовое простраство --
    public class DataBase
    {
        private readonly static string _connectionString = @"Server = 127.0.0.1\Slavan; Database = SystemO; User id = sa; Password = 123; TrustServerCertificate = True; ";

        public static string Select(string query)
        {
            return Execute(query);
        }
        public static string Delete(string query)
        {
            return ExecuteNonQuery(query);
        }
        public static string Update(string query)
        {
            return ExecuteNonQuery(query);
        }
        public static string Create(string query)
        {
            return ExecuteNonQuery(query);
        }

        private static string ExecuteNonQuery(string query)
        {
            Debug.WriteLine("server geting query: " + query);
            string answer = "";
            try
            {
                using (SqlConnection connection = new SqlConnection(_connectionString))
                {
                    connection.Open();
                    SqlCommand command = new SqlCommand(query, connection);
                    command.ExecuteNonQuery();
                }
                answer = "OK";
            }
            catch
            {
                answer = "NO";
            }
            return answer;
        }
        private static string Execute(string query)
        {
            Debug.WriteLine("server geting query: "+ query);
            string answer = "";

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand(query, connection);

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Debug.WriteLine("server column count = " + reader.GetColumnSchema());
                        for (int i = 0; i < reader.GetColumnSchema().Count; i++)
                        {
                            answer += reader.GetValue(i).ToString() + "  ";
                        }
                        answer += Environment.NewLine; //
                    }
                }
            }
            return answer;
        }
    }

}
