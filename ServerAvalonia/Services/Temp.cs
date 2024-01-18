using ServerAvalonia.ViewModels;
using ReactiveUI;

namespace ServerAvalonia.Services
{
    public class Temp:ReactiveObject
    {
        public static MainViewModel MainViewModel { get; set; } = new MainViewModel();
    }
}
