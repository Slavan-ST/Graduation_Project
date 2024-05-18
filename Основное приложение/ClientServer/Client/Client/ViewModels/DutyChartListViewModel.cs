using Client.ViewModels.Base;
using Helper.Models.Main;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Client.ViewModels
{
    public class DutyChartListViewModel : ViewModelBase
    {
        /// <summary>
        /// Отображение спинера загрузки
        /// </summary>
        [Reactive]
        public bool IsLoading { get; set; } = false;

        [Reactive]
        public string MonthString { get; set; } = string.Empty;
        [Reactive]
        public int MonthInt { get; set; } = DateTime.Now.Month;

        public DutyChartListViewModel(IScreen? screen = null) : base(screen)
        {
            MonthString = CultureInfo.CurrentCulture.DateTimeFormat.GetAbbreviatedMonthName(MonthInt);
            MonthNext = ReactiveCommand.Create(() =>
            {
                IsLoading = true;
                if(MonthInt != 12)
                {
                    MonthInt += 1;
                    MonthString = CultureInfo.CurrentCulture.DateTimeFormat.GetAbbreviatedMonthName(MonthInt);
                    GetAsync();
                }
                IsLoading = false;
            });
            MonthPrev = ReactiveCommand.Create(() =>
            {
                IsLoading = true;
                if(MonthInt != 1)
                {
                    MonthInt -= 1;
                    MonthString = CultureInfo.CurrentCulture.DateTimeFormat.GetAbbreviatedMonthName(MonthInt);
                    GetAsync();
                }
                IsLoading = false;
            });
            GetAsync();
        }

        private async void GetAsync()
        {
            IsLoading = true;
            DutyItems = await API.DutyScheduleAPI.GetDutySchedulesMonth(DateTime.Now.Year,MonthInt);
            IsLoading = false;
        }

        [Reactive]
        public IEnumerable<DutySchedule>? DutyItems { get; set; }

        /// <summary>
        /// Переключается месяц вперед
        /// </summary>
        public ICommand MonthNext { get; set; }
        /// <summary>
        /// Переключается месяц назад
        /// </summary>
        public ICommand MonthPrev { get; set; }
    }
}
