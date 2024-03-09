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
            Click = ReactiveCommand.Create(() =>
            {
                GetAttendanceLogs_Test();
            }); 
        }
        public ICommand Click { get; set; }








        #region Небольшие тесты для проверки функционала

        //вывод списка Журнала чистоты
        private async void GetAttendanceLogs_Test()
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
        }
        //вывод лога Журнала чистоты
        private async void GetAttendanceLog_Test(int id)
        {
            Debug.WriteLine("Test start");
            var attendanceLogDTO = await Client.API.AttendanceLog.GetAttendanceLog(id);
            if (attendanceLogDTO == null)
            {
                Debug.WriteLine("Test stop: null");
                return;
            }
            Debug.WriteLine(attendanceLogDTO.Student!.Name);
            Debug.WriteLine(attendanceLogDTO.Date);
            Debug.WriteLine(attendanceLogDTO.Marker!.Char);

            Debug.WriteLine("Test stop: end");
        }

        #endregion
    }
}
