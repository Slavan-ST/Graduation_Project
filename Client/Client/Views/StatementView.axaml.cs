using Avalonia.Controls;
using Client.Services;

namespace Client.Views
{
    public partial class StatementView : UserControl
    {
        public StatementView()
        {
            InitializeComponent();
            DataContext = Navigation.Statement;
        }
    }
}
