using Client.API;
using Client.Models;
using Client.ViewModels.Base;
using Helper.Models.Main;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Client.ViewModels
{
    public class DailyCheckViewModel : ViewModelBase
    {
        /// <summary>
        /// Отображение спинера загрузки
        /// </summary>
        [Reactive]
        public bool IsLoading { get; set; } = false;

        public DailyCheckViewModel(IScreen? screen = null) : base(screen)
        {
            this.WhenAnyValue(x => x.SelectedRoom).Subscribe(x => FillStudents());

            Next = ReactiveCommand.Create(() =>
            {
                IsLoading = true;
                if (Rooms == null)
                {
                    IsLoading = false;
                    return;
                }

                if (SelectedRoom == null)
                {
                    IsLoading = false;
                    return;
                }

                SelectedRoom = Rooms.Where(x => (x.Id + 1) == SelectedRoom.Id).FirstOrDefault();
            });
            Save = ReactiveCommand.Create(() =>
            {
                IsLoading = true;
                if (Students != null)
                {
                    SaveChanges(Students);
                }
                IsLoading = false;
            });
            FillRooms();
            FillAllStudents();
        }


        public ICommand Next { get; set; }
        public ICommand Save { get; set; }
        [Reactive]
        public Room? SelectedRoom { get; set; } // для comboBox
        [Reactive]
        public List<Room>? Rooms { get; set; } // для comboBox, загружается только при старте страницы
        [Reactive]
        public List<StudentInRoom>? Students { get; set; } //Студенты в комнате
        [Reactive]
        public List<Student>? AllStudents { get; set; } //Все студенты (для первоначальной загрузки)

        async void FillRooms()
        {
            IsLoading = true;
            var rooms = await RoomAPI.GetRoomsAsync();
            if (rooms != null)
            {
                Rooms = new List<Room>(rooms);
                SelectedRoom = Rooms.First();
            }
            IsLoading = false;
        }

        async void FillAllStudents()
        {
            IsLoading = true;
            var students = await StudentAPI.GetStudentsAsync();
            if (students != null)
            {
                AllStudents = new List<Student>(students);
            }
            IsLoading = false;
        }
        void FillStudents()
        {
            IsLoading = true;
            if (SelectedRoom == null)
            {
                IsLoading = false;
                return;
            }
            if (AllStudents == null)
            {
                IsLoading = false;
                return;
            }
            var students = AllStudents.Where(x => x.Room!.Number == SelectedRoom.Number).ToList();
            var studentsInRoom = new List<StudentInRoom>();
            foreach (var i in students)
            {
                studentsInRoom.Add(new StudentInRoom()
                {
                    Student = i
                });
            }

            Students = studentsInRoom;
            IsLoading = false;
        }
        async void SaveChanges(List<StudentInRoom> logs)
        {
            IsLoading = true;
            foreach (var log in logs)
            {
                if (log.Student == null)
                {
                    continue;
                }
                AttendanceLog attendanceLog = new()
                {
                    StudentId = log.Student.Id,
                    Date = DateTime.Now,
                    Marker = log.Mark
                };
                await API.AttendanceLogAPI.PostAttendanceLog(attendanceLog);
            }
            IsLoading = false;
        }
    }
}
