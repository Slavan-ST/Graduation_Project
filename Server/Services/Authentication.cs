using Helper.Models;
using Server.Data;

namespace Server.Services
{
    public static class Authentication
    {
        public static bool IsAuthentication(string login, string password, ref ApplicationContext db)
        {
            var user = db.Users.Where(x => x.Login == login && x.Password == password).FirstOrDefault();
            if (user == null)
            {
                return false;
            }

            //
            // ВНИМАНИЕ!! данное место необходимо будет переработать после правки контекста БД
            //
            return IsAuthentication(user);
        }
        public static bool IsAuthentication(User? user)
        {
            if (user == null)
            {
                return false;
            }
            return true;
        }
    }
}
