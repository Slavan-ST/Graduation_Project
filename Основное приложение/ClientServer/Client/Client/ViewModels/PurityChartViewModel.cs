using Client.ViewModels.Base;
using Helper.Models.Main;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Windows.Input;

namespace Client.ViewModels
{
    public class PurityChartViewModel : ViewModelBase
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

        public PurityChartViewModel(IScreen? screen = null) : base(screen)
        {
            MonthString = CultureInfo.CurrentCulture.DateTimeFormat.GetAbbreviatedMonthName(MonthInt);
            MonthNext = ReactiveCommand.Create(() =>
            {
                IsLoading = true;
                if (MonthInt != 12)
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
                if (MonthInt != 1)
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
            PurityRaids = await API.PurityRaidLogAPI.GetPurityRaidLogsMonth(2023, 6);
            IsLoading = false;
        }


        [Reactive]
        public IEnumerable<PurityRaidLog>? PurityRaids { get; set; }

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
