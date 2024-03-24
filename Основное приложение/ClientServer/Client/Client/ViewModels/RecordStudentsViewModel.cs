using Client.API;
using Helper.Models.DTO;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;
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

    public class RecordStudentsViewModel : ViewModelBase
    {
        [Reactive]
        public DataView People { get; set; }
        public DataTable PeopleTable { get; set; }

        public RecordStudentsViewModel(IScreen? screen = null) : base(screen)
        {
            TestPeoples();
        }

        async void TestPeoples()
        {
            PeopleTable = await MakeTable();
            People = PeopleTable.DefaultView;
        }

        async Task<DataTable> MakeTable()
        {
            IEnumerable<AttendanceLogDTO>? logDTOs = await API.AttendanceLog.GetAttendanceLogs();
            DataTable dataTable = new DataTable();
            dataTable.Columns.Add("FIO");

            foreach (var item in logDTOs)
            {
                if (!dataTable.Columns.Contains(item.Date.Day.ToString()))
                {
                    dataTable.Columns.Add(new DataColumn(item.Date.Day.ToString()));
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
                else
                {
                    day = item.Date.Day.ToString();
                    row[day] = item.Marker?.Char;
                }
                dataTable.Rows.Add(row);
            }

            return dataTable;
        }
    }
}
