namespace WebAPI.Models
{
    public class FIOConverter
    {
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
