using Avalonia.Controls;
using Client.Services;

namespace Client.Views
{
    public partial class PurityChartView : UserControl
    {
        public PurityChartView()
        {
            InitializeComponent();
            DataContext = Navigation.PurityChart;
        }
    }
}
