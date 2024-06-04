using Avalonia.Input;
using Avalonia.Markup.Xaml;
using Avalonia.ReactiveUI;
using Client.ViewModels;
using ReactiveUI;

namespace Client.Views
{
    public partial class EventsListView : ReactiveUserControl<EventsListViewModel>
    {
        public EventsListView()
        {
            this.WhenActivated(disposables => { });
            AvaloniaXamlLoader.Load(this);
        }

        private void Calendar_PointerWheelChanged(object? sender, PointerWheelEventArgs e)
        {
            e.Handled = true;
        }
    }
}
