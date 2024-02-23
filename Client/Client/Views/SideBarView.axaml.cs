using Avalonia.Controls;
using maket.ViewModels;

namespace maket.Views
{
    public partial class SideBarView : UserControl
    {
        public SideBarView()
        {
            InitializeComponent();
            DataContext = new SideBarViewModel();
        }
    }
}
