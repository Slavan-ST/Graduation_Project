using ReactiveUI.Fody.Helpers;
using ServerAvalonia.Models;
using System.Threading;

namespace ServerAvalonia.ViewModels;

public class MainViewModel : ViewModelBase
{
    Thread ServerThread;

    [Reactive]
    public string Greeting { get; set; } = "Welcome!";

    public MainViewModel()
    {
        ServerThread = new Thread(RunServer);
        ServerThread.Start();
    }


    void RunServer()
    {
         new Server(80);
    }
}
