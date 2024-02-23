using Avalonia.Controls;
using Client.Services;
using Client.ViewModels;

namespace Client.Views
{
    public partial class FaqView : UserControl
    {
        public FaqView()
        {
            InitializeComponent();
            DataContext = Navigation.Faq;
        }
    }
}
