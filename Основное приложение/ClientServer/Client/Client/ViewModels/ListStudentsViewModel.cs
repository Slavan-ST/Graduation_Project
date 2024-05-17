using Client.API;
using Client.ViewModels.Base;
using Helper.Models.Main;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Client.ViewModels
{
    public class ListStudentsViewModel : ViewModelBase
    {
        /// <summary>
        /// Отображение спинера загрузки
        /// </summary>
        [Reactive]
        public bool IsLoading { get; set; } = false;

        public ICommand NewStudent { get; set; }

        public ListStudentsViewModel(IScreen? screen = null) : base(screen)
        {
            this.WhenAnyValue(x => x.SelectedStudent).Subscribe(x =>
            {
                //при клике на студента будет открываться окно профиля с выбранным студентом
                this.HostScreen.Router.Navigate.Execute(new ProfileViewModel(screen, SelectedStudent));
            });

            FillFilter();
            FillListStudents(_noFilters);

            AcceptFilters = ReactiveCommand.Create(() =>
            {
                IsLoading = true;
                FillListStudents(_filters);
                IsLoading = false;
            });
            ClearFilters = ReactiveCommand.Create(() =>
            {
                IsLoading = true;
                FillListStudents(_noFilters);
                IsLoading = false;
            });

            NewStudent = ReactiveCommand.Create(() =>
            {
                this.HostScreen.Router.Navigate.Execute(new ProfileViewModel(screen, new Student()));
            });
        }

        [Reactive]
        public Student? SelectedStudent { get; set; }

        [Reactive]
        public ProfileViewModel? ProfileStudent { get; set; }
        async void FillListStudents(Func<Task<IEnumerable<Student>?>> func)
        {
            IsLoading = true;
            //получаем студентов из БД
            var students = await func();

            if (students == null)
            {
                IsLoading = false;
                return;
            }

            if (students != null)
            {
                Students = new List<Student>(students);
                IsLoading = false;
            }
            else
            {
                Students = new List<Student>();
                IsLoading = false;
            }
            IsLoading = false;
        }


        [Reactive]
        public List<Student>? Students { get; set; }


        #region Фильтры
        [Reactive]
        public Student? ListStudentsSelectedItem { get; set; }
        [Reactive]
        public Room? ListRoomsSelectedItem { get; set; }
        [Reactive]
        public Status? ListStatusesSelectedItem { get; set; }

        [Reactive]
        public List<Student>? ListStudents { get; set; }
        [Reactive]
        public List<Room>? ListRooms { get; set; }
        [Reactive]
        public List<Status>? ListStatuses { get; set; }

        public ICommand AcceptFilters { get; set; }
        public ICommand ClearFilters { get; set; }

        async Task<IEnumerable<Student>?> _noFilters()
        {
            IsLoading = true;
            ListRoomsSelectedItem = null;
            ListStatusesSelectedItem = null;
            ListStudentsSelectedItem = null;
            var students = await StudentAPI.GetStudentsAsync();
            IsLoading = false;
            return students;
        }
        async Task<IEnumerable<Student>?> _filters()
        {
            IsLoading = true;
            var list = await StudentAPI.GetStudentsAsync();
            if (list == null)
            {
                IsLoading = false;
                return null;
            }
            if (ListRoomsSelectedItem != null)
            {
                list = list.Where(x => x.Room!.Number == ListRoomsSelectedItem.Number).ToList();
            }
            if (ListStatusesSelectedItem != null)
            {
                list = list.Where(x => x.Status!.Name == ListStatusesSelectedItem.Name).ToList();
            }
            if (ListStudentsSelectedItem != null)
            {
                list = list.Where(x =>
                    x.Name == ListStudentsSelectedItem.Name &&
                    x.Surname == ListStudentsSelectedItem.Surname &&
                    x.Patronymic == ListStudentsSelectedItem.Patronymic
                ).ToList();
            }
            IsLoading = false;
            return list;
        }

        public async void FillFilter()
        {
            IsLoading = true;
            var statuses = await StatusAPI.GetStatusesAsync();
            if (statuses == null)
            {
                IsLoading = false;
                return;
            }
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
            ListStatuses = new List<Status>(statuses);
            ListRooms = new List<Room>(rooms);
            IsLoading = false;
        }
        #endregion
    }
}
