using MessageConverter.Models;
using System.Globalization;

namespace MessageConverter
{

    //тут будет находиться преобразование сообщений от сервера и клиента в нужный вид 
    //з.ы. т.е. string in Student..обратно и тд.

    public static class MessageConverter
    {
        #region Student
        /// <summary>
        /// из сообщения где описан/зашифрован один студент получаем студента
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        public static Student StringToStudent(string text)
        {
            return new Student();
        }
        /// <summary>
        /// преобразуем студента в его описание текстом, т.е. шифруем
        /// </summary>
        /// <param name="student"></param>
        /// <returns></returns>
        public static string StudentToString(Student student)
        {
            return "";
        }
        /// <summary>
        /// из сообщения где описан/зашифрован список студентов получаем список студентов
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        public static IEnumerable<Student> StringToStudents(string text)
        {
            return new List<Student>();
        }
        public static string StudentsToString(List<Student> students)
        {
            return "";
        }


        #endregion
        #region AttendanceLog - журнал учёта студентов в ночное время суток
        /// <summary>
        /// из сообщения где описан/зашифрован одна запись журнала - получаем запись
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        public static AttendanceLog StringToAttendanceLog(string text)
        {
            return new AttendanceLog();
        }
        /// <summary>
        /// запись журнала шифруем в тектс
        /// </summary>
        /// <param name="log"></param>
        /// <returns></returns>
        public static string AttendanceLogToString(AttendanceLog log)
        {
            return "";
        }
        /// <summary>
        /// много записей шифруем в текст
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        public static List<AttendanceLog> StringToAttendanceLogs(string text)
        {
            return new List<AttendanceLog>();
        }
        /// <summary>
        /// текст в много записей
        /// </summary>
        /// <param name="logs"></param>
        /// <returns></returns>
        public static string AttendanceLogsToString(List<AttendanceLog> logs)
        {
            return "";
        }
        #endregion
        #region OrganizedEvent          я устал, дальше тоже самое, но с мероприятиями
        public static OrganizedEvent StringToOrganizedEvent(string text)
        {
            return new OrganizedEvent();
        }
        public static string OrganizedEventToString(OrganizedEvent organizedEvent)
        {
            return "";
        }
        public static List<OrganizedEvent>StringToOrganizedEvents(string text)
        {
            return new List<OrganizedEvent>();
        }
        public static string OrganizedEventsToString(List<OrganizedEvent> organizedEvent)
        {
            return "";
        }
        #endregion
    }
}