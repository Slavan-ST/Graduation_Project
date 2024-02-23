using Avalonia.Controls;
using Client.Services;

namespace Client.Views
{
    public partial class DutyChartView : UserControl
    {
        public DutyChartView()
        {
            InitializeComponent();
            DataContext = Navigation.DutyChart;
        }
    }
}
