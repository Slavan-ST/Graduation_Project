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
            // проверить нужен ли ID
            RoleDTO roleDTO = new RoleDTO()
            {
                Id = 0,
                Name = "Admin"
            };
            RoomDTO roomDTO = new RoomDTO()
            {
                Id = 0,
                Number = "309"
            };
            UserDTO userDTO = new UserDTO()
            {
                Id = 0,
                Name = "test",
                Surname = "test",
                Patronymic = "test",
                Login = "test",
                Image = null,
                Role = roleDTO
            };
            MarkerDTO markerDTO = new MarkerDTO()
            {
                Id = 0,
                Char = "+"
            };
            StudentDTO studentDTO = new StudentDTO()
            {
                Id = 0,
                Name = "test",
                Surname = "test",
                Patronymic = "test",
                Room = roomDTO
            };
            UserChangedDTO userChangedDTO = new UserChangedDTO()
            {
                Id = 0,
                Name = "test",
                Surname = "test",
                Patronymic = "test",
                Login = "test",
                Password ="test",
                Image = null,
                Role = roleDTO
            };
            AttendanceLogDTO attendanceLogDTO = new AttendanceLogDTO()
            {
                Id = 0,
                Date = System.DateTime.Now,
                Student = studentDTO,
                Marker = markerDTO
            };

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
        private async void DeleteAttendanceLog_Test(int id)
        {
            Debug.WriteLine("Test start");
            var statusCode = await Client.API.AttendanceLog.DeleteAttendanceLog(id);
            if (statusCode == null)
            {
                Debug.WriteLine("Test stop: null");
                return;
            }
            Debug.WriteLine("Status: " + statusCode.Value);

            Debug.WriteLine("Test stop: end");
        }
        private async void PostAttendanceLog_Test(AttendanceLogDTO attendanceLog)
        {
            Debug.WriteLine("Test start");
            var statusCode = await Client.API.AttendanceLog.PostAttendanceLog(attendanceLog);
            if (statusCode == null)
            {
                Debug.WriteLine("Test stop: null");
                return;
            }
            Debug.WriteLine("Status: " + statusCode.Value);

            Debug.WriteLine("Test stop: end");
        }
        private async void PutAttendanceLog_Test(AttendanceLogDTO attendanceLog)
        {
            Debug.WriteLine("Test start");
            var statusCode = await Client.API.AttendanceLog.PutAttendanceLog(attendanceLog);
            if (statusCode == null)
            {
                Debug.WriteLine("Test stop: null");
                return;
            }
            Debug.WriteLine("Status: " + statusCode.Value);

            Debug.WriteLine("Test stop: end");
        }

        private async void SignIn_Test(string login, string password)
        {
            Debug.WriteLine("Test start");
            var userDTO = await Client.API.Home.SignIn(login, password);
            if (userDTO == null)
            {
                Debug.WriteLine("Test stop: null");
                return;
            }
            Debug.WriteLine(userDTO.Name);
            Debug.WriteLine(userDTO.Login);
            Debug.WriteLine(userDTO.Role?.Name);

            Debug.WriteLine("Test stop: end");
        }
        private async void SignOut_Test()
        {
            Debug.WriteLine("Test start");
            var code = await Client.API.Home.SignOut();
            if (code == null)
            {
                Debug.WriteLine("Test stop: null");
                return;
            }
            Debug.WriteLine(code.Value);

            Debug.WriteLine("Test stop: end");
        }



        #endregion
    }
}
