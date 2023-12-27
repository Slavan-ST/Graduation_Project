using ClientAvalonia.Models.TestFromOld;
using ReactiveUI.Fody.Helpers;

namespace ClientAvalonia.ViewModels;

public class MainViewModel : ViewModelBase
{
    [Reactive]
    public string Greeting { get; set; } = "Welcome!";

    public MainViewModel()
    {
        WorkingWithServer.SendMessage();
    }
}
