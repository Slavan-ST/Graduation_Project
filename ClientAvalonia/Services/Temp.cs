using ClientAvalonia.ViewModels;
using ReactiveUI;

namespace ClientAvalonia.Services
{
    public class Temp:ReactiveObject
    {
        //Временное свойство для изменения поля, при получении ответа от сервера
        public static MainViewModel MainViewModel { get; set; } = new MainViewModel();
        public static int CountAnswer { get; set; } = 0;
    }
}
