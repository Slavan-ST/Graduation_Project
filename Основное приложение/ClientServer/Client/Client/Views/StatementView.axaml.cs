using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using Avalonia.ReactiveUI;
using Client.Services;
using Client.ViewModels;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;

namespace Client.Views
{
    public partial class StatementView : ReactiveUserControl<StatementViewModel>
    {
        public StatementView()
        {
            this.WhenActivated(disposables => { });
            AvaloniaXamlLoader.Load(this);
        }
    }
}
