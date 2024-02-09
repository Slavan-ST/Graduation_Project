using ReactiveUI;
using ReactiveUI.Fody.Helpers;
using System.Windows.Input;
using System.Diagnostics;
using Avalonia.Media.Imaging;
using System.Net.Http;
using System.Net;
using System.Collections.Generic;

namespace ClientAvalonia.ViewModels;

public class MainViewModel : ViewModelBase
{

    public MainViewModel()
    {

        //отправка сообщения
        Send = ReactiveCommand.Create(async () =>
        {
            HttpClient client = new HttpClient();


            //var response = await client.PostAsync("http://localhost:5000/login/", content);
            var response = await client.GetAsync(string.Format("http://192.168.0.2:5170/login/?login={0}&password={1}","kola","123"));

            //Client.Start();
            Debug.WriteLine("it's work! " + response.StatusCode);
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
