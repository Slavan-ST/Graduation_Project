using Avalonia.Controls;
using System.Diagnostics;
using Test.ViewModels;

namespace Test.Views
{
    public partial class MainView : UserControl
    {
        public MainView()
        {
            InitializeComponent();
            DataContext = new MainViewModel();
        }
    }
}