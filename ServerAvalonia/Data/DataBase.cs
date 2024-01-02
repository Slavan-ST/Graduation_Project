using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace ServerAvalonia.Data
{
    //test -- всё это просто тестовое простраство --
    public class DataBase
    {
        //1 - вывод журнала
        //2 - вывод всех студентов
        //3 - вывод инфомрациии о мероприятиях
        //4 - вывод рейтинга
        //5 - вывод инф. чистоты


        private readonly static string _connectionString = @"Server = 127.0.0.1\Slavan; Database = SystemO; User id = sa; Password = 123; TrustServerCertificate = True; ";
        
        /// <summary>
        /// Учет нахождение проживающего в общежитии в ночное время суток; - вывод журнала:
        /// Id, RoomId, StudentId, MarkerId, Date
        /// </summary>
        /// <returns>возращает данные из таблицы в текстовом виде, 
        /// где пробел - разделитель между стобцами, новая строка - разделителб между строками</returns>
        public static string SelectAllAttendanceLog()
        {
            string query = "select * from AttendanceLog;";
            return CommandReturn(query,5);              //в AttendanceLog 5 столбов
        }
        /// <summary>
        /// Вывод информациии о студентах
        /// Id, Name, Surname, Patronymic, IdRoom
        /// </summary>
        /// <returns></returns>
        public static string SelectAllStudents()
        {
            string query = "select * from Students;";
            return CommandReturn(query, 5);              //в Students 5 столбов
        }
        /// <summary>
        /// шаблон команды sql
        /// </summary>
        /// <param name="countColumn">количество столбцов в таблице, по умолчанию = 1 </param>
        /// <param name="query">запрос</param>
        /// <returns>возращает данные из таблицы в текстовом виде, 
        /// где пробел - разделитель между стобцами, новая строка - разделителб между строками</returns>
        private static string CommandReturn(string query, int countColumn = 1)
        {
            string answer = "";

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand(query, connection);

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        for (int i = 0; i < countColumn; i++)
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
