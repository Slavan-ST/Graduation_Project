using Client.API;
using Client.ViewModels.Base;
using Helper.Models.Main;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Client.ViewModels
{
    public class DutyChartViewModel : ViewModelBase
    {
        [Reactive]
        public bool IsLoading { get; set; }
        [Reactive]
        public List<Student>? ListStudents { get; set; }
        [Reactive]
        public List<Room>? ListRooms { get; set; }

        [Reactive]
        public IEnumerable<DutySchedule>? DutyItems { get; set; }

        public ICommand NewSchedule {  get; set; }
        public ICommand Save {  get; set; }
        public ICommand Delete { get; set; }
        /// <summary>
        /// Переключается месяц вперед
        /// </summary>
        public ICommand MonthNext { get; set; }
        /// <summary>
        /// Переключается месяц назад
        /// </summary>
        public ICommand MonthPrev { get; set; }
        [Reactive]
        public string MonthString { get; set; } = string.Empty;
        [Reactive]
        public int MonthInt { get; set; } = DateTime.Now.Month;

        public DutyChartViewModel(IScreen? screen = null) : base(screen)
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
            NewSchedule = ReactiveCommand.Create(() =>
            {
                IsLoading = true;
                // новое дежурство (идеально сделать проверку что в этом месяце уже нет места)
                IsLoading = false;
            });
            Save = ReactiveCommand.Create(() =>
            {
                IsLoading = true;

                IsLoading = false;
            });
            Delete = ReactiveCommand.Create(() =>
            {
                IsLoading = true;

                IsLoading = false;
            });
            FillItemsSource();
        }
        public async void FillItemsSource()
        {
            IsLoading = true;
            var rooms = await RoomAPI.GetRoomsAsync();
            if (rooms == null)
            {
                IsLoading = false;
                return;
            }
            var students = await StudentAPI.GetStudentsAsync();
            if (students == null)
            {
                IsLoading = false;
                return;
            }
            ListStudents = new List<Student>(students);
            ListRooms = new List<Room>(rooms);
            IsLoading = false;
        }
        private async void GetAsync()
        {
            IsLoading = true;
            DutyItems = await API.DutyScheduleAPI.GetDutySchedulesMonth(DateTime.Now.Year, MonthInt);
            IsLoading = false;
        }
    }
}
