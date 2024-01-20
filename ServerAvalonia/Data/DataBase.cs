using Avalonia.Media.Imaging;
using Microsoft.Data.SqlClient;
using ServerAvalonia.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
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
                using SqlConnection connection = new SqlConnection(_connectionString);
                connection.Open();
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@image", buffer);
                command.Parameters.AddWithValue("@fio", "Guest2");
                int changed = command.ExecuteNonQuery();
            }
            catch
            {
                Debug.WriteLine("EXECUTE False");
            }
        }
        public static Bitmap? ExecuteQueryTest()
        {
            Bitmap? image = null;
            string query = "select Image from Users where FIO=@fio;";

            try
            {
                using SqlConnection connection = new SqlConnection(_connectionString);
                connection.Open();
                SqlCommand command = new SqlCommand(query, connection);

                command.Parameters.AddWithValue("@fio", "Guest2");


                using SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    byte[] content = (byte[])reader["Image"];
                    using var stream = new MemoryStream(content);
                    image = new Bitmap(stream);
                }
            }
            catch
            {
                Debug.WriteLine("EXECUTE False <Image>");
            }
            return image;
        }









        /// <summary>
        /// запрос к БД. Пример: select * from Users where Name =@name; 
        /// где name - название параметра, content которого будет подставляться
        /// </summary>
        /// <param name="query">запрос</param>
        /// <param name="parameters">коллекция параметров с содержимым(byte[],string, text...)</param>
        public static bool ExecuteNonQuery(string query, IEnumerable<ParameterQuery> parameters)
        {
            try
            {
                using SqlConnection connection = new SqlConnection(_connectionString);
                connection.Open();
                SqlCommand command = new SqlCommand(query, connection);

                AddParametersInCommand(ref command, parameters);

                command.ExecuteNonQuery();
                return true;
            }
            catch(Exception ex)
            {
                Debug.WriteLine(ex);
                return false;
            }
        }
        /// <summary>
        /// Добавление в команду необходимых параметров
        /// </summary>
        /// <param name="command"></param>
        /// <param name="parameters"></param>
        private static void AddParametersInCommand(ref SqlCommand command, IEnumerable<ParameterQuery> parameters)
        {
            foreach (var p in parameters)
            {
                command.Parameters.AddWithValue(p.Name, p.Content);
            }
        }
    }

}
