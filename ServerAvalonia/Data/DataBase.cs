using Avalonia.Media.Imaging;
using Microsoft.Data.SqlClient;
using System;
using System.Diagnostics;
using System.IO;

namespace ServerAvalonia.Data
{
    //test -- всё это просто тестовое простраство --
    public class DataBase
    {
        private readonly static string _connectionString = @"Server = Slavanst\slavan; Database = SystemO; User id = sa; Password = 123; TrustServerCertificate = True; ";

        public static void ExecuteNonQueryTest(Bitmap? image)
        {
            string query = "update Users set Image=@image where FIO=@fio;";
            using var stream = new MemoryStream();
            image!.Save(stream);
            byte[] buffer = stream.ToArray();

            try
            {
                using (SqlConnection connection = new SqlConnection(_connectionString))
                {
                    connection.Open();
                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@image", buffer);
                    command.Parameters.AddWithValue("@fio", "Guest2");
                    int changed = command.ExecuteNonQuery();
                    Debug.WriteLine($"server change line: {changed}");
                }
                Debug.WriteLine("EXECUTE TRUE");
            }
            catch (Exception e)
            {
                Debug.WriteLine(e);
                Debug.WriteLine("EXECUTE False");
            }
        }
    }

}
