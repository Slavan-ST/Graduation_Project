using Avalonia.Media.Imaging;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;
using System;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;

namespace ServerAvalonia.ViewModels;

public class MainViewModel : ViewModelBase
{

    int port = 13400;

    TcpServer Server;
    public MainViewModel()
    {
        Server = new TcpServer(port);
        ServerStart();

        Console.WriteLine("Нажмите любую клавишу для выхода...");


        Stop = ReactiveCommand.Create(() =>
        {
            Server.Stop();
        });
    }
    private async void ServerStart()
    {
        Task serverTask = Server.ListenAsync();
        await serverTask;
    }

    [Reactive]
    public string Answer { get; set; } = ""; //  от клиента
    public ICommand Stop { get; set; }

}
