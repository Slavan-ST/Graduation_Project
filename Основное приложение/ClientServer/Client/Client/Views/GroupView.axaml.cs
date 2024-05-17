using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using Avalonia.ReactiveUI;
using Client.ViewModels;
using ReactiveUI;

namespace Client.Views
{
    public partial class GroupView : ReactiveUserControl<GroupViewModel>
    {
        public GroupView()
        {
            this.WhenActivated(disposables => { });
            AvaloniaXamlLoader.Load(this);
        }
    }
}
