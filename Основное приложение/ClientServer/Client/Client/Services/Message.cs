using Avalonia;
using Avalonia.Controls;
using Microsoft.Extensions.DependencyInjection;
using MsBox.Avalonia;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            var result = await MessageBoxManager.GetMessageBoxStandard(title, message).ShowAsPopupAsync(_content);
        }
        public static void Show(string title, string message)
        {
            var messageService = App.Current!.Services!.GetService<Message>()!;
            messageService.ShowMessageAsync(title, message);
        }
    }
}
