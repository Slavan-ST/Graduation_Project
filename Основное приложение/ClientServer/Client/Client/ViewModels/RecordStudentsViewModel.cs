using Avalonia.Controls;
using Avalonia.Controls.Models.TreeDataGrid;
using Client.API;
using Client.Models;
using Client.ViewModels.Base;
using DynamicData;
using Helper.Models.DTO;
using Helper.Models.Main;
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

        public RecordStudentsViewModel(IScreen? screen = null) : base(screen)
        {
            Source = new FlatTreeDataGridSource<MarksOfStudents>(new List<MarksOfStudents>());
            FillJournal(2023,6);
            FillFilter();

            AcceptFilters = ReactiveCommand.Create(()=>
            {

            });
        }
        public async void FillFilter()
        {
            var students = await StudentAPI.GetStudentsAsync();
            if (students == null)
            {
                return;
            }
            ListStudents = new List<Student>(students);
        }
        public async void FillJournal(int year, int month)
        {
            //получаем кол-во дней в текущем месяце
            int countDay = DateTime.DaysInMonth(year, month);

            //тут будет храниться строка
            List<MarksOfStudents> marksOfStudentsList = new List<MarksOfStudents>();

            //получаем студентов из БД
            var students = await StudentAPI.GetStudentsAsync();

            if (students == null)
            {
                return;
            }

            foreach (var student in students)
            {
                if (student.AttendanceLogs == null)
                {
                    return;
                }

                //получаем логи за текущий месяц
                var studLogs = student.AttendanceLogs.Where(x => x.Date.Year == year && x.Date.Month == month).ToList();

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


        [Reactive]
        public Student? ListStudentsSelectedItem { get; set; }
        [Reactive]
        public Room? ListRoomsSelectedItem { get; set; }
        [Reactive]
        public Status? ListStatusesSelectedItem { get; set; }

        [Reactive]
        public List<Student>? ListStudents { get; set; }
        [Reactive]
        public FlatTreeDataGridSource<MarksOfStudents> Source { get; set; }
        public ICommand AcceptFilters { get; set; }

    }
}
