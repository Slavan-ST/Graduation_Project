using Avalonia.Controls;
using Client.Services;

namespace Client.Views
{
    public partial class NewsView : UserControl
    {
        public NewsView()
        {
            InitializeComponent();
            DataContext = Navigation.News;
        }
    }
}
