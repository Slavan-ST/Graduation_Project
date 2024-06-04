namespace WebAPI.Models
{
    /// <summary>
    /// Для перевода из сокращенного ФИО в полное
    /// </summary>
    public class FIOConverter
    {
        /// <summary>
        /// Для перевода из сокращенного ФИО в полное
        /// </summary>
        /// <param name="fio"></param>
        /// <returns></returns>
        public static (string surname, string name, string? patronymic) GetSurnameNamePatronymicFromFIO(string fio)
        {
            string[] fioWords = fio.Split(" ");
            if (fioWords.Length == 2)
            {
                return (fioWords[0], fioWords[1], null);
            }
            return (fioWords[0], fioWords[1], fioWords[2]);
        }
    }
}
