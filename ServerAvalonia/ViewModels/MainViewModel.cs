using ReactiveUI;
using ReactiveUI.Fody.Helpers;
using ServerAvalonia.Models;
using System.Diagnostics;
using System.Linq;
using System.Windows.Input;

namespace ServerAvalonia.ViewModels;

public class MainViewModel : ViewModelBase
{
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
