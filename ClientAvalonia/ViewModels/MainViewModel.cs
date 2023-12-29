using ReactiveUI;
using ReactiveUI.Fody.Helpers;
using System.Net.Sockets;
using System;
using System.Windows.Input;
using System.Diagnostics;
using ClientAvalonia.Models;
using System.Net.Http;

namespace ClientAvalonia.ViewModels;

public class MainViewModel : ViewModelBase
{
    TcpClient _client;
    Connection _connection;

    public MainViewModel()
    {
        //порт
        int port = 13400;
        //адрес сервера
        _client = new TcpClient("127.0.0.1", port); // тут просто локальный сервер
        //создаём соединение
        _connection = new Connection(_client);

        //отправка сообщения
        Send = ReactiveCommand.Create(async () =>
        {
            await _connection.SendMessageAsync(TextMessage);
        });



        ManyClientsTests();
    }

    private async void Tests()
    {
        for (int i = 0; i < 100; i++)
        {
            await _connection.SendMessageAsync(i + " test from point " + _client.Client.LocalEndPoint);
        }

    }
    private async void ManyClientsTests()
    {
        //порт
        int port = 13400;
        //адрес сервера
        _client = new TcpClient("127.0.0.1", port); // тут просто локальный сервер
        //создаём соединение
        _connection = new Connection(_client);

        //отправка сообщения
        Send = ReactiveCommand.Create(async () =>
        {
            await _connection.SendMessageAsync(TextMessage);
        });
        for (int i = 0; i < 100; i++)
        {
            await _connection.SendMessageAsync(i + " test from point " + _client.Client.LocalEndPoint);
        }
        Tests();
    }

    [Reactive]
    public string TextMessage { get; set; } = ""; // отправляеммое сообщение

    [Reactive]
    public string Answer { get; set; } = ""; //сообщение, получаемое от сервера, в данном случае простое "эхо"
    public ICommand Send { get; set; }       //команда отправки сообщения серверу 
}
