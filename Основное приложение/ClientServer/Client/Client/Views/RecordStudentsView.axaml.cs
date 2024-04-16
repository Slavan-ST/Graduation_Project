using Avalonia.Controls;
using Avalonia.Data;
using Avalonia.Markup.Xaml;
using Avalonia.ReactiveUI;
using Client.ViewModels;
using ReactiveUI;
using System.Diagnostics;
using System.Linq;

namespace Client.Views
{
    public partial class RecordStudentsView : ReactiveUserControl<RecordStudentsViewModel>
    {
        public RecordStudentsView()
        {
            this.WhenActivated(disposables => { });
            AvaloniaXamlLoader.Load(this);
        }

    }
}
