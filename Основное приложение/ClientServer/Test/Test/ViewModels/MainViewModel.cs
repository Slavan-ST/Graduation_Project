using Helper.API;
using Helper.Models.DTO;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;
using System;
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

            ClickLogs = ReactiveCommand.Create(() =>
            {
                Response = "";
                GetAttendanceLogs_Test();
            });

            ClickLog = ReactiveCommand.Create(() =>
            {
                Response = "";
                GetAttendanceLogs_Test();
            });
            ClickStudents = ReactiveCommand.Create(() =>
            {
                Response = "";
                GetAttendanceLogs_Test();
            });
            ClickStudent = ReactiveCommand.Create(() =>
            {
                Response = "";
                GetAttendanceLogs_Test();
            });
            ClickUsers = ReactiveCommand.Create(() =>
            {
                Response = "";
                GetAttendanceLogs_Test();
            });
            ClickUser = ReactiveCommand.Create(() =>
            {
                Response = "";
                GetAttendanceLogs_Test();
            });

            ClickSignIn = ReactiveCommand.Create(() =>
            {
                Response = "";
                GetAttendanceLogs_Test();
            });
            ClickSignOut = ReactiveCommand.Create(() =>
            {
                Response = "";
                GetAttendanceLogs_Test();
            });


            ClickLogPost = ReactiveCommand.Create(() =>
            {
                Response = "";
                GetAttendanceLogs_Test();
            });
            ClickLogPut = ReactiveCommand.Create(() =>
            {
                Response = "";
                GetAttendanceLogs_Test();
            });
            ClickUserPost = ReactiveCommand.Create(() =>
            {
                Response = "";
                GetAttendanceLogs_Test();
            });
            ClickUserPut = ReactiveCommand.Create(() =>
            {
                Response = "";
                GetAttendanceLogs_Test();
            });
            ClickStudentPost = ReactiveCommand.Create(() =>
            {
                Response = "";
                GetAttendanceLogs_Test();
            });
            ClickStudentPut = ReactiveCommand.Create(() =>
            {
                Response = "";
                GetAttendanceLogs_Test();
            });


            ClickStudentDel = ReactiveCommand.Create(() =>
            {
                Response = "";
                GetAttendanceLogs_Test();
            });
            ClickUserDel = ReactiveCommand.Create(() =>
            {
                Response = "";
                GetAttendanceLogs_Test();
            });
            ClickLogDel = ReactiveCommand.Create(() =>
            {
                Response = "";
                GetAttendanceLogs_Test();
            });

        }

        //Get
        public ICommand ClickLogs { get; set; }
        public ICommand ClickLog { get; set; }
        public ICommand ClickStudents { get; set; }
        public ICommand ClickStudent { get; set; }
        public ICommand ClickUsers { get; set; }
        public ICommand ClickUser { get; set; }
        public ICommand ClickSignIn { get; set; }
        public ICommand ClickSignOut { get; set; }

        //Post
        public ICommand ClickUserPost { get; set; }
        public ICommand ClickLogPost { get; set; }
        public ICommand ClickStudentPost { get; set; }

        //Put
        public ICommand ClickUserPut { get; set; }
        public ICommand ClickLogPut { get; set; }
        public ICommand ClickStudentPut { get; set; }

        //Delete
        public ICommand ClickUserDel { get; set; }
        public ICommand ClickLogDel { get; set; }
        public ICommand ClickStudentDel { get; set; }

        [Reactive]
        public string? TestData { get; set; } = "";
        [Reactive]
        public string? Response { get; set; } = "";


        private void Output(string? message)
        {
            if (message == null)
            {
                Response += "null" + Environment.NewLine;
            }
            else
            {
                Response += message + Environment.NewLine;
            }
        }



        #region Небольшие тесты для проверки функционала

        //вывод списка Журнала чистоты
        private async void GetAttendanceLogs_Test()
        {
            Output("Test start");
            var attendanceLogDTOs = await Client.API.AttendanceLog.GetAttendanceLogs();
            if (attendanceLogDTOs == null)
            {
                Output("Test stop: null");
                return;
            }
            foreach (var attendanceLogDTO in attendanceLogDTOs)
            {
                Output(attendanceLogDTO.Student!.Name);
                Output(attendanceLogDTO.Date.ToString());
                Output(attendanceLogDTO.Marker!.Char);
            }
            Output("Test stop: end");
        }
        private async void GetAttendanceLog_Test(int id)
        {
            Output("Test start");
            var attendanceLogDTO = await Client.API.AttendanceLog.GetAttendanceLog(id);
            if (attendanceLogDTO == null)
            {
                Output("Test stop: null");
                return;
            }
            Output(attendanceLogDTO.Student!.Name);
            Output(attendanceLogDTO.Date.ToString());
            Output(attendanceLogDTO.Marker!.Char);

            Output("Test stop: end");
        }
        private async void DeleteAttendanceLog_Test(int id)
        {
            Output("Test start");
            var statusCode = await Client.API.AttendanceLog.DeleteAttendanceLog(id);
            if (statusCode == null)
            {
                Output("Test stop: null");
                return;
            }
            Output("Status: " + statusCode.Value);

            Output("Test stop: end");
        }
        private async void PostAttendanceLog_Test(AttendanceLogDTO attendanceLog)
        {
            Output("Test start");
            var statusCode = await Client.API.AttendanceLog.PostAttendanceLog(attendanceLog);
            if (statusCode == null)
            {
                Output("Test stop: null");
                return;
            }
            Output("Status: " + statusCode.Value);

            Output("Test stop: end");
        }
        private async void PutAttendanceLog_Test(AttendanceLogDTO attendanceLog)
        {
            Output("Test start");
            var statusCode = await Client.API.AttendanceLog.PutAttendanceLog(attendanceLog);
            if (statusCode == null)
            {
                Output("Test stop: null");
                return;
            }
            Output("Status: " + statusCode.Value);

            Output("Test stop: end");
        }

        private async void SignIn_Test(string login, string password)
        {
            Output("Test start");
            var userDTO = await Client.API.Home.SignIn(login, password);
            if (userDTO == null)
            {
                Output("Test stop: null");
                return;
            }
            Output(userDTO.Name);
            Output(userDTO.Login);
            Output(userDTO.Role?.Name);

            Output("Test stop: end");
        }
        private async void SignOut_Test()
        {
            Output("Test start");
            var code = await Client.API.Home.SignOut();
            if (code == null)
            {
                Output("Test stop: null");
                return;
            }
            Output(code.Value.ToString());

            Output("Test stop: end");
        }


        private async void GetStudents_Test()
        {
            Output("Test start");
            var studentDTOs = await Client.API.Student.GetStudentsAsync();
            if (studentDTOs == null)
            {
                Output("Test stop: null");
                return;
            }
            foreach (var studentDTO in studentDTOs)
            {
                Output(studentDTO.Name);
                Output(studentDTO.Room?.Number);
            }
            Output("Test stop: end");
        }
        private async void GetStudent_Test(int id)
        {
            Output("Test start");
            var studentDTO = await Client.API.Student.GetStudentAsync(id);
            if (studentDTO == null)
            {
                Output("Test stop: null");
                return;
            }

            Output(studentDTO.Name);
            Output(studentDTO.Room?.Number);

            Output("Test stop: end");
        }
        private async void DeleteStudent_Test(int id)
        {
            Output("Test start");
            var code = await Client.API.Student.DeleteStudentAsync(id);
            if (code == null)
            {
                Output("Test stop: null");
                return;
            }

            Output(code.Value.ToString());

            Output("Test stop: end");
        }
        private async void PostStudent_Test(StudentDTO studentDTO)
        {
            Output("Test start");
            var code = await Client.API.Student.PostStudentAsync(studentDTO);
            if (code == null)
            {
                Output("Test stop: null");
                return;
            }

            Output(code.Value.ToString());

            Output("Test stop: end");
        }
        private async void PutStudent_Test(StudentDTO studentDTO)
        {
            Output("Test start");
            var code = await Client.API.Student.PutPutStudentAsync(studentDTO);
            if (code == null)
            {
                Output("Test stop: null");
                return;
            }

            Output(code.Value.ToString());

            Output("Test stop: end");
        }


        private async void GetUsers_Test()
        {
            Output("Test start");
            var userDTOS = await Client.API.User.GetUsersAsync();
            if (userDTOS == null)
            {
                Output("Test stop: null");
                return;
            }
            foreach (var userDTO in userDTOS)
            {
                Output(userDTO.Name);
                Output(userDTO.Login);
            }
            Output("Test stop: end");
        }
        private async void GetUser_Test(int id)
        {
            Output("Test start");
            var userDTO = await Client.API.User.GetUserAsync(id);
            if (userDTO == null)
            {
                Output("Test stop: null");
                return;
            }
            Output(userDTO.Login);
            Output(userDTO.Name);

            Output("Test stop: end");
        }
        private async void DeleteUser_Test(int id)
        {
            Output("Test start");
            var code = await Client.API.User.DeleteUserAsync(id);
            if (code == null)
            {
                Output("Test stop: null");
                return;
            }

            Output(code.Value.ToString());

            Output("Test stop: end");
        }
        private async void PostUser_Test(UserDTO userDTO)
        {
            Output("Test start");
            var code = await Client.API.User.PostUserAsync(userDTO);
            if (code == null)
            {
                Output("Test stop: null");
                return;
            }

            Output(code.Value.ToString());

            Output("Test stop: end");
        }
        private async void PutUser_Test(UserDTO userDTO)
        {
            Output("Test start");
            var code = await Client.API.User.PutUserAsync(userDTO);
            if (code == null)
            {
                Output("Test stop: null");
                return;
            }

            Output(code.Value.ToString());

            Output("Test stop: end");
        }



        #endregion
    }
}
