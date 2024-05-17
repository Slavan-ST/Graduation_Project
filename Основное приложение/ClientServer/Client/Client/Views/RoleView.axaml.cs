using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using Avalonia.ReactiveUI;
using Client.ViewModels;
using ReactiveUI;

namespace Client.Views
{
    public partial class RoleView : ReactiveUserControl<RoleViewModel>
    {
        public RoleView()
        {
            this.WhenActivated(disposables => { });
            AvaloniaXamlLoader.Load(this);
        }
    }
}
