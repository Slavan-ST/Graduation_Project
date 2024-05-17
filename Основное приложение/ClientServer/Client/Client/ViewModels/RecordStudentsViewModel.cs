using Avalonia.Controls;
using Avalonia.Controls.Models.TreeDataGrid;
using Client.API;
using Client.Models;
using Client.ViewModels.Base;
using DynamicData;
using Helper.Models.DTO;
using Helper.Models.Main;
using iText.Layout.Element;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Data.Common;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Client.ViewModels
{

    public class RecordStudentsViewModel : ViewModelBase
    {
        /// <summary>
        /// Отображение спинера загрузки
        /// </summary>
        [Reactive]
        public bool IsLoading { get; set; } = false;

        public RecordStudentsViewModel(int year, int month, IScreen? screen = null) : base(screen)
        {
            Year = year;
            Month = month;
            Source = new FlatTreeDataGridSource<MarksOfStudents>(new List<MarksOfStudents>());
            FillJournal(year, month, _noFilters);
            FillFilter();

            AcceptFilters = ReactiveCommand.Create(() =>
            {
                IsLoading = true;
                FillJournal(year, month, _filters);
                IsLoading = false;
            });
            ClearFilters = ReactiveCommand.Create(() =>
            {
                IsLoading = true;
                FillJournal(year, month, _noFilters);
                IsLoading = false;
            });
        }
        public async void FillFilter()
        {
            IsLoading = true;
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
            IsLoading = false ;
        }
        public async void FillJournal(int year, int month, Func<Task<IEnumerable<Student>?>> func )
        {
            IsLoading = true;
            Source.Items = new List<MarksOfStudents>();

            //получаем кол-во дней в текущем месяце
            int countDay = DateTime.DaysInMonth(year, month);

            //тут будет храниться строка
            List<MarksOfStudents> marksOfStudentsList = new List<MarksOfStudents>();

            //получаем студентов из БД
            var students = await func();

            if (students == null)
            {
                IsLoading = false;
                return;
            }

            foreach (var student in students)
            {
                if (student.AttendanceLogs == null)
                {
                    IsLoading = false;
                    return;
                }

                //получаем логи за текущий месяц
                var studLogs = student.AttendanceLogs.Where(x => x.Date.Year == Year && x.Date.Month == Month).ToList();

                if (studLogs == null)
                {
                    IsLoading = false;
                    return;
                }

                //добавляем новую строку
                MarksOfStudents marksOfStudents = new MarksOfStudents()
                {
                    Student = student,
                    Logs = studLogs
                };
                marksOfStudentsList.Add(marksOfStudents);
            }

            //массив столбцов
            ColumnList<MarksOfStudents> columns =
            [
                //столбец ФИО студента
                new TextColumn<MarksOfStudents, string>("Студент", x => $"{x.Student.FIO}"),
                                //тестовый столбец с маркерами - так работает
                new TextColumn<MarksOfStudents, string>(1, x => ReturnMarker(x.Logs, 1)),
                new TextColumn<MarksOfStudents, string>(2, x => ReturnMarker(x.Logs, 2)),
                new TextColumn<MarksOfStudents, string>(3, x => ReturnMarker(x.Logs, 3)),
                new TextColumn<MarksOfStudents, string>(4, x => ReturnMarker(x.Logs, 4)),
                new TextColumn<MarksOfStudents, string>(5, x => ReturnMarker(x.Logs, 5)),
                new TextColumn<MarksOfStudents, string>(6, x => ReturnMarker(x.Logs, 6)),
                new TextColumn<MarksOfStudents, string>(7, x => ReturnMarker(x.Logs, 7)),
                new TextColumn<MarksOfStudents, string>(8, x => ReturnMarker(x.Logs, 8)),
                new TextColumn<MarksOfStudents, string>(9, x => ReturnMarker(x.Logs, 9)),
                new TextColumn<MarksOfStudents, string>(10, x => ReturnMarker(x.Logs, 10)),
                new TextColumn<MarksOfStudents, string>(11, x => ReturnMarker(x.Logs, 11)),
                new TextColumn<MarksOfStudents, string>(12, x => ReturnMarker(x.Logs, 12)),
                new TextColumn<MarksOfStudents, string>(13, x => ReturnMarker(x.Logs, 13)),
                new TextColumn<MarksOfStudents, string>(14, x => ReturnMarker(x.Logs, 14)),
                new TextColumn<MarksOfStudents, string>(15, x => ReturnMarker(x.Logs, 15)),
                new TextColumn<MarksOfStudents, string>(16, x => ReturnMarker(x.Logs, 16)),
                new TextColumn<MarksOfStudents, string>(17, x => ReturnMarker(x.Logs, 17)),
                new TextColumn<MarksOfStudents, string>(18, x => ReturnMarker(x.Logs, 18)),
                new TextColumn<MarksOfStudents, string>(19, x => ReturnMarker(x.Logs, 19)),
                new TextColumn<MarksOfStudents, string>(20, x => ReturnMarker(x.Logs, 20)),
                new TextColumn<MarksOfStudents, string>(21, x => ReturnMarker(x.Logs, 21)),
                new TextColumn<MarksOfStudents, string>(22, x => ReturnMarker(x.Logs, 22)),
                new TextColumn<MarksOfStudents, string>(23, x => ReturnMarker(x.Logs, 23)),
                new TextColumn<MarksOfStudents, string>(24, x => ReturnMarker(x.Logs, 24)),
                new TextColumn<MarksOfStudents, string>(25, x => ReturnMarker(x.Logs, 25)),
                new TextColumn<MarksOfStudents, string>(26, x => ReturnMarker(x.Logs, 26)),
                new TextColumn<MarksOfStudents, string>(27, x => ReturnMarker(x.Logs, 27)),
                new TextColumn<MarksOfStudents, string>(28, x => ReturnMarker(x.Logs, 28)),
            ];
            if (countDay > 28)
            {
                columns.Add(new TextColumn<MarksOfStudents, string>(29, x => ReturnMarker(x.Logs, 29)));
            }
            if (countDay > 29)
            {
                columns.Add(new TextColumn<MarksOfStudents, string>(30, x => ReturnMarker(x.Logs, 30)));
            }
            if (countDay > 30)
            {
                columns.Add(new TextColumn<MarksOfStudents, string>(31, x => ReturnMarker(x.Logs, 31)));
            }

            //присваиваем датасоурсе
            Source = new FlatTreeDataGridSource<MarksOfStudents>(marksOfStudentsList)
            {
                Columns =
                {
                    columns
                }
            };
            Source.Items = marksOfStudentsList;
            IsLoading = false;
        }

        string ReturnMarker(List<AttendanceLog> logs, int day)
        {
            var log = logs.Where(_ => _.Date.Day == day).FirstOrDefault();
            if (log == null)
            {
                return "";
            }
            return log.Marker;
        }

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
            //а нафига? МБ тогда остальные фильтры блочить??
            //if (ListStudentsSelectedItem != null)
            //{
            //    return new List<Student>() { ListStudentsSelectedItem };
            //}
            IsLoading = true;
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
            IsLoading = false;
            return list;
        }


        [Reactive]
        public int Year { get; set; } = 2023;
        [Reactive]
        public int Month { get; set; } = 6;
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
        [Reactive]
        public FlatTreeDataGridSource<MarksOfStudents> Source { get; set; }
        public ICommand AcceptFilters { get; set; }
        public ICommand ClearFilters { get; set; }

    }
}
