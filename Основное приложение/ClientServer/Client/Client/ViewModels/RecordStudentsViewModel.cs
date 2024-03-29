using Client.API;
using Client.ViewModels.Base;
using Helper.Models.DTO;
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

    public class People
    {
        public List<string> Line { get; set; } = new List<string>();
    }

    public class RecordStudentsViewModel : ViewModelBase
    {
        [Reactive]
        public List<People>? People { get; set; } = null!;

        public RecordStudentsViewModel(IScreen? screen = null) : base(screen)
        {
            TestPeoples();
        }

        async void TestPeoples()
        {
            People = await MeVariant(2023, 11);
        }

        async Task<List<People>?> MeVariant(int year, int month)
        {
            IEnumerable<AttendanceLogDTO>? logDTOs = await API.AttendanceLog.GetAttendanceLogs();
            var logs = logDTOs!.Where(x => x.Date.Year == year && x.Date.Month == month).OrderBy(x => x.Date).ToList(); //логи текущего месяца, отсортированные 

            List<People> lines = new List<People>();

            var students = await API.Student.GetStudentsAsync();

            if (students == null)
            {
                Debug.WriteLine("null");
                return null;
            }
            Debug.WriteLine(students.ToList().Count);

            foreach (var student in students)
            {
                List<string> line = new List<string>();
                var logsStudent = logs.Where(x => x.Student!.Id == student.Id).ToList();
                line.Add(student.Name);
                Debug.WriteLine(student.Name);
                foreach (var log in logsStudent)
                {
                    line.Add(log.Marker!.Char);
                    Debug.WriteLine(log.Marker!.Char);
                }
                People people = new People() { Line = line };
                lines.Add(people);


            }


            return lines;


        }
    }
}
