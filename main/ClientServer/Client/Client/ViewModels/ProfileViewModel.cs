using Client.API;
using Client.ViewModels.Base;
using Helper.Models.Main;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Input;

namespace Client.ViewModels
{
    public class ProfileViewModel : ViewModelBase
    {
        public ProfileViewModel(IScreen? screen = null) : base(screen)
        {
            Initialize();
        }
        public ProfileViewModel(IScreen? screen = null, Student? student = null) : base(screen)
        {
            if (student == null)
            {
                Initialize();
                return;
            }
            Initialize(student);
        }

        [Reactive]
        public Student? Student { get; set; }
        public ICommand? Save { get; set; }
        public ICommand? Delete { get; set; }


        [Reactive]
        public bool IsLoading { get; set; } = false;
        [Reactive]
        public List<Group>? Groups { get; set; }
        [Reactive]
        public bool IsWorker { get; set; } = false;



        void Initialize()
        {

            IsLoading = true;
            IsWorker = true;
            Save = ReactiveCommand.Create(() =>
            {
                IsLoading = true;
                SaveStudentInApi();
                IsLoading = false;
            });
            Delete = ReactiveCommand.Create(async () =>
            {
                if (Student == null)
                {
                    return;
                }
                IsLoading = true;
                await API.StudentAPI.DeleteStudentAsync(Student.Id);
                IsLoading = false;
                this.HostScreen.Router.Navigate.Execute(new ListStudentsViewModel(this.HostScreen));
            });
            FillComboBoxesAsync();
            IsLoading = false;
        }
        void Initialize(Student student)
        {
            IsLoading = true;
            Initialize();
            FillLines(student);
            IsLoading = false;
        }
        void FillLines(Student student)
        {
            this.Student = student;
        }
        async void SaveStudentInApi()
        {
            IsLoading = true;
            if (this.Student == null)
            {
                IsLoading = false;
                return;
            }

            await API.StudentAPI.PostStudentAsync(this.Student);
            IsLoading = false;
            this.HostScreen.Router.Navigate.Execute(new ListStudentsViewModel(this.HostScreen));
        }



        #region Боксы


        [Reactive]
        public Room? SelectedRoom { get; set; }
        [Reactive]
        public Group? SelectedGroup { get; set; }
        [Reactive]
        public Status? SelectedStatus { get; set; }

        [Reactive]
        public List<Room>? Rooms { get; set; }
        [Reactive]
        public List<Status>? Statuses { get; set; }

        public async void FillComboBoxesAsync()
        {
            var statuses = await StatusAPI.GetStatusesAsync();
            if (statuses == null)
            {
                return;
            }
            var rooms = await RoomAPI.GetRoomsAsync();
            if (rooms == null)
            {
                return;
            }
            var students = await StudentAPI.GetStudentsAsync();
            if (students == null)
            {
                return;
            }
            var groups = await GroupAPI.GetGroupsAsync();
            if (groups == null)
            {
                return;
            }

            Statuses = new List<Status>(statuses);
            Rooms = new List<Room>(rooms);
            Groups = new List<Group>(groups);


            SelectedGroup = Groups.FirstOrDefault();
            SelectedRoom = Rooms.FirstOrDefault();
            SelectedStatus = Statuses.FirstOrDefault();

        }
        #endregion
    }
}
