using Avalonia.Controls;
using Client.Services;

namespace Client.Views
{
    public partial class EventsListView : UserControl
    {
        public EventsListView()
        {
            InitializeComponent();
            DataContext = Navigation.EventsList;
        }
    }
}
