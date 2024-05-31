using Avalonia.Controls;
using Avalonia.Controls.Templates;
using Avalonia.Media;
using Client.ViewModels.Base;
using System;

namespace Client.Services
{
    public class ViewLocator : IDataTemplate
    {
        public bool SupportsRecycling => false;

        public Control Build(object? data)
        {
            var name = data!.GetType().FullName!.Replace("ViewModel", "View");
            var type = Type.GetType(name);

            if (type != null)
            {
                return (Control)Activator.CreateInstance(type)!;
            }
            else
            {
                return new TextBlock { Text = "Not Found: " + name, Background = Brushes.Red };
            }
        }

        public bool Match(object? data)
        {
            return data is ViewModelBase;
        }
    }
}
