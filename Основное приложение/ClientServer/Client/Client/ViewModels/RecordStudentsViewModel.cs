using Client.API;
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

    public class RecordStudentsViewModel : ViewModelBase
    {
        [Reactive]
        public DataTable People { get; set; } = null!;

        public RecordStudentsViewModel(IScreen? screen = null) : base(screen)
        {
            TestPeoples();
        }

        async void TestPeoples()
        {
            People = await MeVariant(2023, 11);
        }

        async Task<DataTable> MakeTable()
        {
            IEnumerable<AttendanceLogDTO>? logDTOs = await API.AttendanceLog.GetAttendanceLogs();
            DataTable dataTable = new DataTable();
            dataTable.Columns.Add("FIO");

            var prevDate = DateTime.MinValue;
            foreach (var item in logDTOs!)
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
                day = item.Date.Day.ToString();
                row![day] = item.Marker?.Char;
            }

            return dataTable;
        }
        async Task<DataTable> MeVariant(int year, int month)
        {
            IEnumerable<AttendanceLogDTO>? logDTOs = await API.AttendanceLog.GetAttendanceLogs();
            DataTable dataTable = new DataTable();


            dataTable.Columns.Add("FIO");
            for (int i = 0; i < month; i++)
            {
                dataTable.Columns.Add(new DataColumn((i + 1).ToString()));
            }

            if (logDTOs != null)
            {
                var logs = logDTOs.Where(x => x.Date.Year == year && x.Date.Month == month).OrderBy(x => x.Date).ToList(); //логи текущего месяца, отсортированные 

                DataRow? row = dataTable.NewRow();

                for (int i = 1; i < dataTable.Columns.Count; i++)
                {
                    row[dataTable.Columns[i].ColumnName] = logs.Where(x => x.Student!.Surname + " " + x.Student!.Name + " " + x.Student!.Patronymic == row["FIO"].ToString() && x.Date.Day.ToString() == dataTable.Columns[i].ColumnName).FirstOrDefault();
                }
                dataTable.Rows.Add(row);

            }

            


            return dataTable;
        }
    }
}
