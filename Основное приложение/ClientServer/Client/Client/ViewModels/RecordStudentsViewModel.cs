using Avalonia.Controls;
using Avalonia.Controls.Models.TreeDataGrid;
using Client.API;
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
    public class MarksOfStudents
    {
        public Student Student { get; set; } = new Student();
        public List<AttendanceLog> Logs { get; set; } = new List<AttendanceLog>();
    }

    public class RecordStudentsViewModel : ViewModelBase
    {
        public RecordStudentsViewModel(IScreen? screen = null) : base(screen)
        {
            Source = new FlatTreeDataGridSource<MarksOfStudents>(new List<MarksOfStudents>());
            TestStudent(2023,6);
        }
        public async void TestStudent(int year, int month)
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
                Debug.WriteLine("list count =      " + marksOfStudents.Logs.Count);
            }

            //массив столбцов
            ColumnList<MarksOfStudents> columns = new Avalonia.Controls.Models.TreeDataGrid.ColumnList<MarksOfStudents>();
            
            //столбец ФИО студента
            columns.Add(new TextColumn<MarksOfStudents, string>("Студент", x => $"{x.Student.Name} {x.Student.Surname} {x.Student.Patronymic}"));
            
            //тестовый столбец с маркерами - так работает
            columns.Add(new TextColumn<MarksOfStudents, string>(11, x => $"{x.Logs[1].Marker}"));

            //а вот через цикл заполняться не хотят
            for (int i = 1; i <= countDay; i++)
            {
                columns.Add(new TextColumn<MarksOfStudents, string>(i, x => $"{x.Logs[i-1].Marker}"));
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

        [Reactive]
        public FlatTreeDataGridSource<MarksOfStudents> Source { get; set; }

    }
}
