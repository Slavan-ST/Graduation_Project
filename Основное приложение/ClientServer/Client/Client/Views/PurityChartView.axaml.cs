using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using Avalonia.ReactiveUI;
using Client.Services;
using Client.ViewModels;
using ReactiveUI;

namespace Client.Views
{
    public partial class PurityChartView : ReactiveUserControl<PurityChartViewModel>
    {
        public PurityChartView()
        {
            this.WhenActivated(disposables => { });
            AvaloniaXamlLoader.Load(this);
        }
    }
}
