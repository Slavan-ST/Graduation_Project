using Client.ViewModels;
using ReactiveUI;

namespace Client.ViewModels;

public class MainViewModel : ViewModelBase
{
    public string Greeting => "Welcome to Avalonia!";
    public MainViewModel(IScreen screen) : base(screen)
    {

    }
}
