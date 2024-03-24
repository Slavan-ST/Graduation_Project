using Client.API;
using Helper.Models.DTO;
using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Client.ViewModels
{
    public class Person
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public bool IsFictitious { get; set; }

        public Person(string firstName, string lastName, bool isFictitious)
        {
            FirstName = firstName;
            LastName = lastName;
            IsFictitious = isFictitious;
        }
    }

    public class RecordStudentsViewModel : ViewModelBase
    { 
        public DataTable People { get; set; }

        public RecordStudentsViewModel(IScreen? screen = null) : base(screen)
        {
            TestPeoples();
        }

        async void TestPeoples()
        {
            People = await MakeTable();
        }

        async Task<DataTable> MakeTable()
        {
            IEnumerable<AttendanceLogDTO>? logDTOs = await API.AttendanceLog.GetAttendanceLogs();
            DataTable dataTable = new DataTable();
            dataTable.Columns.Add("FIO");

            var prevDate = DateTime.MinValue;
            foreach (var item in logDTOs)
            {
                if (item.Date != prevDate)
                {
                    dataTable.Columns.Add(new DataColumn(item.Date.Day.ToString()));
                    prevDate = item.Date;
                }
            }
            string prevFullName = string.Empty;
            string day;
            DataRow? row = null;
            foreach (var item in logDTOs) 
            {
                string fullName = item.Student?.Name + " " + item.Student?.Surname + " " + item.Student?.Patronymic;
                if (fullName != prevFullName)
                {
                    row = dataTable.NewRow();
                    row["FIO"] = fullName;
                    day = item.Date.Day.ToString();
                    row[day] = item.Marker?.Char;
                    prevFullName = fullName;
                }
                day = item.Date.Day.ToString();
                row[day] = item.Marker?.Char;
            }

            return dataTable;
        }
    }
}
