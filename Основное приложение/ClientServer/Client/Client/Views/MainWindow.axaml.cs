using Avalonia.Controls;
using Client.Services;

namespace Client.Views;

public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();
        DataContext = Navigation.MainMenu;
    }
}
