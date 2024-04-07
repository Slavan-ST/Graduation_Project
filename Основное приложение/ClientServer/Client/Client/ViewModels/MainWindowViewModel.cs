using Avalonia.Controls;
using Client.ViewModels.Base;
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
            Router.Navigate.Execute(new RecordYearViewModel(this));
        }
        public RoutingState Router { get; } = new RoutingState();
    }
}
