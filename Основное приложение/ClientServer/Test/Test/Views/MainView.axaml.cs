using Avalonia.Controls;
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