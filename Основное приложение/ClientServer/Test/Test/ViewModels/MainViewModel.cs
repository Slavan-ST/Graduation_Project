using WebAPI.Models.DTO;
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

            string loginUser = "1";
            string password = "1";

            string fio = "1";
            string roomForTest = "1";

            ClickLogs = ReactiveCommand.Create(() =>
            {
                Response = "";
                GetAttendanceLogs_Test();
            });

            ClickLog = ReactiveCommand.Create(() =>
            {
                Response = "";
                GetAttendanceLog_Test(100);
            });
            ClickStudents = ReactiveCommand.Create(() =>
            {
                Response = "";
                GetStudents_Test();
            });
            ClickStudent = ReactiveCommand.Create(() =>
            {
                Response = "";
                GetStudent_Test(fio, roomForTest);
            });
            ClickUsers = ReactiveCommand.Create(() =>
            {
                Response = "";
                GetUsers_Test();
            });
            ClickUser = ReactiveCommand.Create(() =>
            {
                Response = "";
                GetUser_Test(loginUser);
            });

            ClickSignIn = ReactiveCommand.Create(() =>
            {
                Response = "";
                SignIn_Test(loginUser,password);
            });
            ClickSignOut = ReactiveCommand.Create(() =>
            {
                Response = "";
                SignOut_Test();
            });


            ClickLogPost = ReactiveCommand.Create(() =>
            {
                Response = "";
                PostAttendanceLog_Test(attendanceLogDTO);
            });
            ClickLogPut = ReactiveCommand.Create(() =>
            {
                Response = "";
                PutAttendanceLog_Test(attendanceLogDTO);
            });
            ClickUserPost = ReactiveCommand.Create(() =>
            {
                Response = "";
                PostUser_Test(userDTO);
            });
            ClickUserPut = ReactiveCommand.Create(() =>
            {
                Response = "";
                PutUser_Test(userDTO);
            });
            ClickStudentPost = ReactiveCommand.Create(() =>
            {
                Response = "";
                PostStudent_Test(studentDTO);
            });
            ClickStudentPut = ReactiveCommand.Create(() =>
            {
                Response = "";
                PutStudent_Test(studentDTO);
            });


            ClickStudentDel = ReactiveCommand.Create(() =>
            {
                Response = "";
                DeleteStudent_Test(fio, roomForTest);
            });
            ClickUserDel = ReactiveCommand.Create(() =>
            {
                Response = "";
                DeleteUser_Test(loginUser);
            });
            ClickLogDel = ReactiveCommand.Create(() =>
            {
                Response = "";
                DeleteAttendanceLog_Test(100);
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
            
            var attendanceLogDTOs = await Client.API.AttendanceLog.GetAttendanceLogs();
            if (attendanceLogDTOs == null)
            {
                
                return;
            }
            foreach (var attendanceLogDTO in attendanceLogDTOs)
            {
                Output(attendanceLogDTO.Student!.Name);
                Output(attendanceLogDTO.Date.ToString());
                Output(attendanceLogDTO.Marker!.Char);
            }
            
        }
        private async void GetAttendanceLog_Test(int id)
        {
            
            var attendanceLogDTO = await Client.API.AttendanceLog.GetAttendanceLog(id);
            if (attendanceLogDTO == null)
            {
                
                return;
            }
            Output(attendanceLogDTO.Student!.Name);
            Output(attendanceLogDTO.Date.ToString());
            Output(attendanceLogDTO.Marker!.Char);

            
        }
        private async void DeleteAttendanceLog_Test(int id)
        {
            
            var statusCode = await Client.API.AttendanceLog.DeleteAttendanceLog(id);
            if (statusCode == null)
            {
                
                return;
            }
            Output("Status: " + statusCode.Value);

            
        }
        private async void PostAttendanceLog_Test(AttendanceLogDTO attendanceLog)
        {
            
            var statusCode = await Client.API.AttendanceLog.PostAttendanceLog(attendanceLog);
            if (statusCode == null)
            {
                
                return;
            }
            Output("Status: " + statusCode.Value);

            
        }
        private async void PutAttendanceLog_Test(AttendanceLogDTO attendanceLog)
        {
            
            var statusCode = await Client.API.AttendanceLog.PutAttendanceLog(attendanceLog);
            if (statusCode == null)
            {
                
                return;
            }
            Output("Status: " + statusCode.Value);

            
        }

        private async void SignIn_Test(string login, string password)
        {
            
            var userDTO = await Client.API.Home.SignIn(login, password);
            if (userDTO == null)
            {
                
                return;
            }
            Output(userDTO.Name);
            Output(userDTO.Login);
            Output(userDTO.Role?.Name);

            
        }
        private async void SignOut_Test()
        {
            
            var code = await Client.API.Home.SignOut();
            if (code == null)
            {
                return;
            }
            Output(code.Value.ToString());

            
        }


        private async void GetStudents_Test()
        {
            
            var studentDTOs = await Client.API.Student.GetStudentsAsync();
            if (studentDTOs == null)
            {
                
                return;
            }
            foreach (var studentDTO in studentDTOs)
            {
                Output(studentDTO.Name);
                Output(studentDTO.Room?.Number);
            }
            
        }
        private async void GetStudent_Test(string fio, string room)
        {
            
            var studentDTO = await Client.API.Student.GetStudentAsync(fio, room);
            if (studentDTO == null)
            {
                
                return;
            }

            Output(studentDTO.Name);
            Output(studentDTO.Room?.Number);

            
        }
        private async void DeleteStudent_Test(string fio, string room)
        {
            
            var code = await Client.API.Student.DeleteStudentAsync(fio, room);
            if (code == null)
            {
                
                return;
            }

            Output(code.Value.ToString());

            
        }
        private async void PostStudent_Test(StudentDTO studentDTO)
        {
            
            var code = await Client.API.Student.PostStudentAsync(studentDTO);
            if (code == null)
            {
                
                return;
            }

            Output(code.Value.ToString());

            
        }
        private async void PutStudent_Test(StudentDTO studentDTO)
        {
            
            var code = await Client.API.Student.PutPutStudentAsync(studentDTO);
            if (code == null)
            {
                
                return;
            }

            Output(code.Value.ToString());

            
        }


        private async void GetUsers_Test()
        {
            
            var userDTOS = await Client.API.User.GetUsersAsync();
            if (userDTOS == null)
            {
                
                return;
            }
            foreach (var userDTO in userDTOS)
            {
                Output(userDTO.Name);
                Output(userDTO.Login);
            }
            
        }
        private async void GetUser_Test(string login)
        {
            
            var userDTO = await Client.API.User.GetUserAsync(login);
            if (userDTO == null)
            {
                
                return;
            }
            Output(userDTO.Login);
            Output(userDTO.Name);

            
        }
        private async void DeleteUser_Test(string login)
        {
            
            var code = await Client.API.User.DeleteUserAsync(login);
            if (code == null)
            {
                
                return;
            }

            Output(code.Value.ToString());

            
        }
        private async void PostUser_Test(UserDTO userDTO)
        {
            
            var code = await Client.API.User.PostUserAsync(userDTO);
            if (code == null)
            {
                
                return;
            }

            Output(code.Value.ToString());

            
        }
        private async void PutUser_Test(UserDTO userDTO)
        {
            
            var code = await Client.API.User.PutUserAsync(userDTO);
            if (code == null)
            {
                
                return;
            }

            Output(code.Value.ToString());

            
        }



        #endregion
    }
}
