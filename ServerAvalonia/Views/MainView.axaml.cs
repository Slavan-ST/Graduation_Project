using Avalonia.Controls;
using ServerAvalonia.ViewModels;
using ServerAvalonia.Services;

namespace ServerAvalonia.Views;

public partial class MainView : UserControl
{
    public MainView()
    {
        InitializeComponent();
        DataContext = Temp.MainViewModel = new MainViewModel();
    }
}
