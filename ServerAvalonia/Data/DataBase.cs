using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServerAvalonia.Data
{
    //test -- всё это просто тестовое простраство --
    public class DataBase
    {
        static string _connectionString = @"Server = 127.0.0.1\Slavan; Database = SystemO; User id = sa; Password = 123; TrustServerCertificate = True; ";
        public static string GetTextForMessage()
        {
            string text = "";
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                
                SqlCommand command = new SqlCommand("select * from AttendanceLog;", connection);
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        text += reader.GetValue(0).ToString() + "  ";
                        text += reader.GetValue(1).ToString() + "  ";
                        text += reader.GetValue(2).ToString() + "  ";
                        text += reader.GetValue(3).ToString() + "  ";
                        text += reader.GetValue(4).ToString() + Environment.NewLine;
                    }
                }

            }
            return text;
        }

    }
    public class ApplicationContext
    {

    }
}
