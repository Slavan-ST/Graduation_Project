using Avalonia.Controls;
using Client.Services;

namespace Client.Views
{
    public partial class EventsView : UserControl
    {
        public EventsView()
        {
            InitializeComponent();
            DataContext = Navigation.Events;
        }
    }
}
