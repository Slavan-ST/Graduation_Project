using Avalonia.Controls;
using Client.Services;
using Client.ViewModels;
using System.Diagnostics;

namespace Client.Views
{
    public partial class MainMenuView : UserControl
    {
        public MainMenuView()
        {
            InitializeComponent();
            DataContext = Navigation.MainMenu; 
        }
    }
}
