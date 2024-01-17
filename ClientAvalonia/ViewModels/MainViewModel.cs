using ReactiveUI;
using ReactiveUI.Fody.Helpers;
using System.Net.Sockets;
using System;
using System.Windows.Input;
using System.Diagnostics;
using System.Net.Http;
using Helper.Models;
using Avalonia.Media.Imaging;
using System.IO;
using System.Text;

namespace ClientAvalonia.ViewModels;

public class MainViewModel : ViewModelBase
{

    //порт
    private static readonly int _port = 13400;
    //адрес сервера
    private static readonly string _ip = "127.0.0.1";

    public MainViewModel()
    {

        //отправка сообщения
        Send = ReactiveCommand.Create( () =>
        {
            Client.Start();
            Debug.WriteLine("it's work!");
        });

    }

    [Reactive]
    public string TextMessage { get; set; } = ""; // отправляемое сообщение
    [Reactive]
    public Bitmap? Image { get; set; }

    [Reactive]
    public string Answer { get; set; } = ""; //сообщение, получаемое от сервера, в данном случае простое "эхо"
    public ICommand Send { get; set; }       //команда отправки сообщения серверу 
}
