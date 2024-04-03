using Avalonia.Markup.Xaml;
using Avalonia.ReactiveUI;
using Client.ViewModels;
using ReactiveUI;

namespace Client.Views
{
    public partial class MainMenuView : ReactiveUserControl<MainMenuViewModel>
    {
        public MainMenuView()
        {
            this.WhenActivated(disposables => { });
            AvaloniaXamlLoader.Load(this);
        }

        private void Grid_PointerPressed(object? sender, Avalonia.Input.PointerPressedEventArgs e)
        {
            var model = (this.DataContext as MainMenuViewModel);
            if (model != null)
            {
                model.IsOpenSideBar = false;
            }
        }
    }
}
