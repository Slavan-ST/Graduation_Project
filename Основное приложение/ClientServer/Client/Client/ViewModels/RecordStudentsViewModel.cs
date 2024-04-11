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
            Source.Items = listLogs;
            Source = new FlatTreeDataGridSource<AttendanceLog>(listLogs)
            {
                Columns =
                {
                    new TextColumn<AttendanceLog, string>("id", x => x.Marker, (r,v) => r.Marker = v, new GridLength(6, GridUnitType.Star), new()
                    {
                        IsTextSearchEnabled = true,
                    })
                }
            };
        }

        [Reactive]
        public FlatTreeDataGridSource<AttendanceLog> Source { get; set; }

    }
}
