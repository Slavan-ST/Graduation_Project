using Helper.API;
using Helper.Models.DTO;

namespace Test.ViewModels
{
    public class MainViewModel : ViewModelBase
    {
        public MainViewModel()
        {
            AttendanceLogDTO? attendanceLogDTO = Client.API.AttendanceLog.GetAttendanceLog(0).Result;
        }
    }
}
