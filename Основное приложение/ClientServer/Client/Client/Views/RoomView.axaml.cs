using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using Avalonia.ReactiveUI;
using Client.ViewModels;
using ReactiveUI;

namespace Client.Views
{
    public partial class RoomView : ReactiveUserControl<RoomViewModel>
    {
        public RoomView()
        {
            this.WhenActivated(disposables => { });
            AvaloniaXamlLoader.Load(this);
        }
    }
}
