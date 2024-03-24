using Avalonia.Markup.Xaml;
using Avalonia.ReactiveUI;
using Client.ViewModels;
using ReactiveUI;
using System.Security.Cryptography.X509Certificates;

namespace Client.Views
{
    public partial class DutyChartView : ReactiveUserControl<DutyChartViewModel>
    {
        public DutyChartView()
        {
            this.WhenActivated(disposables => { });
            AvaloniaXamlLoader.Load(this);
        }

        private void Calendar_PointerWheelChanged(object? sender, Avalonia.Input.PointerWheelEventArgs e)
        {
            e.Handled = true;
            base.OnPointerWheelChanged(e);
        }
    }
}
