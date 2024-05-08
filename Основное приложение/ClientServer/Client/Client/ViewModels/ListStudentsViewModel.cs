using Avalonia.Controls;
using Client.API;
using Client.Models;
using Client.ViewModels.Base;
using Client.Views;
using Helper.Models.DTO;
using Helper.Models.Main;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Client.ViewModels
{
    public class ListStudentsViewModel : ViewModelBase
    {
        public ListStudentsViewModel(IScreen? screen = null) : base(screen)
        {
            this.WhenAnyValue(x => x.SelectedStudent).Subscribe(x =>
            {
                ProfileStudent = new ProfileViewModel(screen, SelectedStudent);
            });

            FillFilter();
            FillListStudents(_noFilters);

            AcceptFilters = ReactiveCommand.Create(() =>
            {
                FillListStudents(_filters);
            });
            ClearFilters = ReactiveCommand.Create(() =>
            {
                FillListStudents(_noFilters);
            });
        }

        [Reactive]
        public Student? SelectedStudent { get; set; }

        [Reactive]
        public ProfileViewModel? ProfileStudent { get; set; }
        async void FillListStudents(Func<Task<IEnumerable<Student>?>> func)
        {
            //получаем студентов из БД
            var students = await func();

            if (students == null)
            {
                return;
            }

            if (students != null)
            {
                Students = new List<Student>(students);
            }
            else
            {
                Students = new List<Student>();
            }
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
            ListRoomsSelectedItem = null;
            ListStatusesSelectedItem = null;
            ListStudentsSelectedItem = null;

            return await StudentAPI.GetStudentsAsync();
        }
        async Task<IEnumerable<Student>?> _filters()
        {
            //а нафига? МБ тогда остальные фильтры блочить??
            //if (ListStudentsSelectedItem != null)
            //{
            //    return new List<Student>() { ListStudentsSelectedItem };
            //}

            var list = await StudentAPI.GetStudentsAsync();
            if (list == null)
            {
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
            return list;
        }

        public async void FillFilter()
        {
            var statuses = await StatusAPI.GetStatusesAsync();
            if (statuses == null)
            {
                return;
            }
            var rooms = await RoomAPI.GetRoomsAsync();
            if (rooms == null)
            {
                return;
            }
            var students = await StudentAPI.GetStudentsAsync();
            if (students == null)
            {
                return;
            }
            ListStudents = new List<Student>(students);
            ListStatuses = new List<Status>(statuses);
            ListRooms = new List<Room>(rooms);
        }
        #endregion
    }
}
