using Avalonia.Controls;
using Microsoft.Extensions.DependencyInjection;
using MsBox.Avalonia;

namespace Client.Services
{
    public class Message
    {
        ContentControl _content;
        public Message(ContentControl control)
        {
            _content = control;
        }
        public async void ShowMessageAsync(string title, string message)
        {
            var messageBox = MessageBoxManager.GetMessageBoxStandard(title, message);
            var result = await messageBox.ShowAsPopupAsync(_content);
        }
        public static void Show(string title, string message)
        {
            var messageService = App.Current!.Services!.GetService<Message>()!;
            messageService.ShowMessageAsync(title, message);
        }
    }
}
