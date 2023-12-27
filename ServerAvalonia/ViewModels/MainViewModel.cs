using ReactiveUI.Fody.Helpers;
using ServerAvalonia.TestFromOld;
using System.Threading;

namespace ServerAvalonia.ViewModels;

public class MainViewModel : ViewModelBase
{
    [Reactive]
    public string Greeting { get; set; } = "Welcome!";

    public MainViewModel()
    {
        new Thread(TestMe).Start();
    }

    void TestMe()
    {
        Test.MainServer();
    }
}
