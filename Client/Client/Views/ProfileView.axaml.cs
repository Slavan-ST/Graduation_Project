using Avalonia.Controls;
using Client.Services;

namespace Client.Views
{
    public partial class ProfileView : UserControl
    {
        public ProfileView()
        {
            InitializeComponent();
            DataContext = Navigation.Profile;
        }
    }
}
