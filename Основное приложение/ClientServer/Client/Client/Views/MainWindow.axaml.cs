using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using Avalonia.ReactiveUI;
using Client.Services;
using Client.ViewModels;
using ReactiveUI;

namespace Client.Views;

public partial class MainWindow:Window
{
    public MainWindow()
    {
#if DEBUG
        this.AttachDevTools();
#endif
        AvaloniaXamlLoader.Load(this);
        DataContext = new MainWindowViewModel();
    }
}
