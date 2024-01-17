using Avalonia.Media.Imaging;
using Helper.Models;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection.PortableExecutable;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace ServerAvalonia.Data
{
    //test -- всё это просто тестовое простраство --
    public class DataBase
    {
        private readonly static string _connectionString = @"Server = Slavanst\slavan; Database = SystemO; User id = sa; Password = 123; TrustServerCertificate = True; ";

        public static IEnumerable<ParametrQuery>? Select(string query, IEnumerable<ParametrQuery>? parametrs)
        {
            return Execute(query, parametrs);
        }
        public static string Delete(string query, IEnumerable<ParametrQuery>? parametrs)
        {
            return ExecuteNonQuery(query, parametrs);
        }
        public static string Update(string query, IEnumerable<ParametrQuery>? parametrs)
        {
            return ExecuteNonQuery(query, parametrs);
        }
        public static string Create(string query, IEnumerable<ParametrQuery>? parametrs)
        {
            return ExecuteNonQuery(query, parametrs);
        }

        private static string ExecuteNonQuery(string query, IEnumerable<ParametrQuery>? parametrs = null)
        {
            string answer = "";
            try
            {
                using (SqlConnection connection = new SqlConnection(_connectionString))
                {
                    connection.Open();
                    SqlCommand command = new SqlCommand(query, connection);
                    SetParamsCommand(ref command, parametrs);
                    int changed = command.ExecuteNonQuery();
                    Debug.WriteLine($"server change line: {changed}");
                }
                answer = "OK";
            }
            catch (Exception e)
            {
                Debug.WriteLine(e);
                answer = "NO";
            }
            return answer;
        }
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
        private static IEnumerable<ParametrQuery>? Execute(string query, IEnumerable<ParametrQuery>? parametrs)
        {
            Debug.WriteLine("server geting query: "+ query);

            List<ParametrQuery> answer = new List<ParametrQuery>();

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand(query, connection);
                SetParamsCommand(ref command, parametrs);

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        for (int i = 0; i < reader.GetColumnSchema().Count; i++)
                        {
                            var value = reader.GetValue(i);
                            string type = value.GetType().Name;
                            string name = reader.GetName(i);
                            byte[]? content = null;


                            if (type == "String")
                            {
                                content = Encoding.UTF8.GetBytes((value as string)!);
                            }
                            if (type == "Byte[]")
                            {
                                content = (byte[]?)value;
                            }
                            if (type == "Int32")
                            {
                                content = Encoding.UTF8.GetBytes(((int)value).ToString());
                            }

                            answer.Add(new ParametrQuery(type, name, content!));


                        }
                    }
                }
            }
            return answer;
        }
        private static void SetParamsCommand(ref SqlCommand command, IEnumerable<ParametrQuery>? parametrs = null)
        {
            if(parametrs != null)
            {
                foreach (var par in parametrs)
                {
                    if (par.Type == "byte[]")
                    {
                        Debug.WriteLine($"server:  paramName= @{par.Name}@; paramType = @{par.Type}@  ");
                        command.Parameters.AddWithValue(par.Name, par.Content);
                    }
                    else if (par.Type == "string")
                    {
                        Debug.WriteLine($"server:  paramName= @{par.Name}@; paramType = @{par.Type}@  ");
                        command.Parameters.AddWithValue(par.Name, Encoding.UTF8.GetString(par.Content!));
                    }
                }
            }
        }
    }

}
