using ReactiveUI;
using ReactiveUI.Fody.Helpers;
using System.Net.Sockets;
using System;
using System.Windows.Input;
using System.Diagnostics;
using ClientAvalonia.Models;

namespace ClientAvalonia.ViewModels;

public class MainViewModel : ViewModelBase
{
    public MainViewModel()
    {
        //тут будет начата работа клиента
        StartClient();

        Send = ReactiveCommand.Create(() =>
        {
            //тут будет отправка сообщения серверу
        });
    }

    private async void StartClient()
    {
        //ну порт
        int port = 13400;
        Console.WriteLine("Запуск клиента....");
        try
        {
            //создаём клиента
            using TcpClient tcpClient = new TcpClient("127.0.0.1", port);
            using Connection connection = new Connection(tcpClient);

            Console.WriteLine($"Подключен к серверу: {port}");
            while (true)
            {
                string? input = Console.ReadLine();
                if (input!.Length == 0)
                    break;
                //отправляем серверу сообщение 
                await connection.SendMessageAsync(input);
            }
        }
        catch (Exception ex)
        {
            Debug.WriteLine(ex.Message);
        }
    }

    [Reactive]
    public string Greeting { get; set; } = "Welcome!";
    [Reactive]
    public string Text { get; set; } = ""; // отправляеммое сообщение
    [Reactive]
    public string Aswer { get; set; } = ""; // ответ от сервера
    public ICommand Send { get; set; }
}
