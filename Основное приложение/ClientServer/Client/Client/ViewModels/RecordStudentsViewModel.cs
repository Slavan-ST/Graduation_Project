using Avalonia.Controls;
using Avalonia.Controls.Models.TreeDataGrid;
using Client.API;
using Client.ViewModels.Base;
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
        public string FullName { get; set; } = string.Empty;
        public Dictionary<string, string>? Marks { get; set; }
    }

    public class RecordStudentsViewModel : ViewModelBase
    {
        public RecordStudentsViewModel(IScreen? screen = null) : base(screen)
        {
            Source = new FlatTreeDataGridSource<AttendanceLog>(new List<AttendanceLog>());
            TestGet();
        }


        public async void TestGet()
        {
            var listLogs = await AttendanceLogAPI.GetAttendanceLogsMonth(2023, 6);
            var students = await StudentAPI.GetStudentsAsync();

            //List<MarksOfStudents> marksOfStudents = new List<MarksOfStudents>();

            Dictionary<string, Dictionary<int,string>> marksOfStudents = new Dictionary<string, Dictionary<int, string>>();

            foreach(var student in students)
            {
                if (!marksOfStudents.ContainsKey($"{student.Name} {student.Surname} {student.Patronymic}"))
                {
                    marksOfStudents.Add($"{student.Name} {student.Surname} {student.Patronymic}", new Dictionary<int, string>());
                }
            }

            foreach (var log in listLogs)
            {
                var studentInDictionary = marksOfStudents[$"{log.Student.Name} {log.Student.Surname} {log.Student.Patronymic}"];

                if (studentInDictionary.ContainsKey(log.Date.Day))
                {
                    studentInDictionary[log.Date.Day] = log.Marker;
                }
                else
                {
                    studentInDictionary.Add(log.Date.Day, log.Marker);
                }
            }

            foreach( var student in marksOfStudents)
            {
                Debug.WriteLine(student.Key);
                foreach( var mark in student.Value)
                {
                    Debug.Write($"{mark.Key} {mark.Value}");
                }
            }


            /*
            Source.Items = listLogs;
            Source = new FlatTreeDataGridSource<AttendanceLog>(listLogs)
            {
                Columns =
                {
                    new TextColumn<AttendanceLog, string>("Студент", x => $"{x.Student.Name} {x.Student.Surname} {x.Student.Patronymic}")
                }
            };
            */
        }

        [Reactive]
        public FlatTreeDataGridSource<AttendanceLog> Source { get; set; }

    }
}
