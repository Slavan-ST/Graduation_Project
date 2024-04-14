using Client.ViewModels.Base;
using Helper.Models.Main;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client.ViewModels
{
    public class DutyChartViewModel : ViewModelBase
    {
        public DutyChartViewModel(IScreen? screen = null) : base(screen)
        {
            GetAsync();
        }

        private async void GetAsync()
        {
            DutyItems = await API.DutyScheduleAPI.GetDutySchedulesMonth(2023,6);
        }

        [Reactive]
        public IEnumerable<DutySchedule>? DutyItems { get; set; }
    }
}
