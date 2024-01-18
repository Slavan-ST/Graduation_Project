using ReactiveUI;
using ReactiveUI.Fody.Helpers;
using System.Windows.Input;
using System.Diagnostics;
using Avalonia.Media.Imaging;

namespace ClientAvalonia.ViewModels;

public class MainViewModel : ViewModelBase
{

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
