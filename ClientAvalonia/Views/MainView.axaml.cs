using Avalonia.Controls;
using ClientAvalonia.Services;
using ClientAvalonia.ViewModels;
using System.Net.Http.Headers;

namespace ClientAvalonia.Views;

public partial class MainView : UserControl
{
    public MainView()
    {
        InitializeComponent();
        DataContext = Temp.MainViewModel = new MainViewModel();
    }
}
