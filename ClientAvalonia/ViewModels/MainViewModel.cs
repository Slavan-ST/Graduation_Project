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
using ClientAvalonia.Data;

namespace ClientAvalonia.ViewModels;

public class MainViewModel : ViewModelBase
{
    private TcpClient _client;
    private Connection _connection;

    //порт
    private static readonly int _port = 13400;
    //адрес сервера
    private static readonly string _ip = "127.0.0.1";

    public MainViewModel()
    {
        //адрес сервера
        _client = new TcpClient(_ip, _port); // тут просто локальный сервер
        //создаём соединение
        _connection = new Connection(_client);

        //отправка сообщения
        Send = ReactiveCommand.Create(async () =>
        {
            new DataFromServer().AddImageInUser("Guest2", _connection);


        });

    }

    [Reactive]
    public string TextMessage { get; set; } = ""; // отправляеммое сообщение
    [Reactive]
    public Bitmap? Image { get; set; }

    [Reactive]
    public string Answer { get; set; } = ""; //сообщение, получаемое от сервера, в данном случае простое "эхо"
    public ICommand Send { get; set; }       //команда отправки сообщения серверу 
}
