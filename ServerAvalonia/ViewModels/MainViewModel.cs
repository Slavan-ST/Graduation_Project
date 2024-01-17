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

    public MainViewModel()
    {
        ServerStart();

        //остановка сервера
        Stop = ReactiveCommand.Create(() =>
        {
            Server.Stop();
        });
    }
    private void ServerStart()
    {
        Server.Start();
    }

    [Reactive]
    public string Answer { get; set; } = ""; //  от клиента
    public ICommand Stop { get; set; }

}
