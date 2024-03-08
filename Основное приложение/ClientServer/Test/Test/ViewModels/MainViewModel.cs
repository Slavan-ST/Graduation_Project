using Helper.API;
using Helper.Models.DTO;
using ReactiveUI;
using System.Diagnostics;
using System.Windows.Input;

namespace Test.ViewModels
{
    public class MainViewModel : ViewModelBase
    {
        public MainViewModel()
        {
            Click = ReactiveCommand.Create(async () =>
            {
                Debug.WriteLine("Test start");
                var attendanceLogDTOs = await Client.API.AttendanceLog.GetAttendanceLogs();
                if (attendanceLogDTOs == null)
                {
                    Debug.WriteLine("Test stop: null");
                    return;
                }
                foreach (var attendanceLogDTO in attendanceLogDTOs)
                {
                    Debug.WriteLine(attendanceLogDTO.Student!.Name);
                    Debug.WriteLine(attendanceLogDTO.Date);
                    Debug.WriteLine(attendanceLogDTO.Marker!.Char);
                }
                Debug.WriteLine("Test stop: end");
            }); 
        }
        public ICommand Click { get; set; }
    }
}
