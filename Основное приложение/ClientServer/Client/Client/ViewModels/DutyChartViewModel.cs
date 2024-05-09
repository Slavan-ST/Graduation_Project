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
    public class DutyChartViewModel : ViewModelBase
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

        public DutyChartViewModel(IScreen? screen = null) : base(screen)
        {
            IsLoading = true;
            MonthString = CultureInfo.CurrentCulture.DateTimeFormat.GetAbbreviatedMonthName(MonthInt);
            MonthNext = ReactiveCommand.Create(() =>
            {
                if(MonthInt != 12)
                {
                    MonthInt += 1;
                    MonthString = CultureInfo.CurrentCulture.DateTimeFormat.GetAbbreviatedMonthName(MonthInt);
                    GetAsync();
                }
            });
            MonthPrev = ReactiveCommand.Create(() =>
            {
                if(MonthInt != 1)
                {
                    MonthInt -= 1;
                    MonthString = CultureInfo.CurrentCulture.DateTimeFormat.GetAbbreviatedMonthName(MonthInt);
                    GetAsync();
                }
            });
            GetAsync();
            IsLoading = false;
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
