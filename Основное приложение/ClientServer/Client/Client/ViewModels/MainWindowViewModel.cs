using Avalonia.Controls;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client.ViewModels
{
    public class MainWindowViewModel: ReactiveObject, IScreen
    {
        public MainWindowViewModel()
        {
            Router.Navigate.Execute(new AuthViewModel(this));
        }
        public RoutingState Router { get; } = new RoutingState();
    }
}
