using Avalonia.Controls;
using Client.Services;

namespace Client.Views
{
    public partial class AuthView : UserControl
    {
        public AuthView()
        {
            InitializeComponent();
            DataContext = Navigation.Authification;
        }
    }
}
