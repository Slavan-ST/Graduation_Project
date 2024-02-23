using Avalonia.Controls;
using Client.Services;

namespace Client.Views
{
    public partial class ListStudentsView : UserControl
    {
        public ListStudentsView()
        {
            InitializeComponent();
            DataContext = Navigation.ListStudents;
        }
    }
}
