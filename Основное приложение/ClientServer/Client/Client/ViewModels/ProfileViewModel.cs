using Client.ViewModels.Base;
using Helper.Models.Main;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;
using System.Collections.Generic;
using System.Windows.Input;

namespace Client.ViewModels
{
    public class ProfileViewModel : ViewModelBase
    {
        /// <summary>
        /// Отображение спинера загрузки
        /// </summary>
        [Reactive]
        public bool IsLoading { get; set; } = false;

        /// <summary>
        /// Группа студента
        /// </summary>
        [Reactive]
        public Group SelectedGroup { get; set; }

        /// <summary>
        /// Все группы
        /// </summary>
        [Reactive]
        public List<Group> Groups { get; set; }
        /// <summary>
        /// Все комнаты
        /// </summary>
        [Reactive]
        public List<Room> Rooms { get; set; }

        /// <summary>
        /// Все статусы
        /// </summary>
        [Reactive]
        public List<Status> Statuses { get; set; }

        /// <summary>
        /// Комната студента
        /// </summary>
        [Reactive]
        public Room SelectedRoom { get; set; }

        /// <summary>
        /// Статус студента
        /// </summary>
        [Reactive]
        public Status SelectedStatus { get; set; }

        /// <summary>
        /// bool view border with buttons on profileView
        /// </summary>
        [Reactive]
        public bool IsWorker { get; set; } = false;

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

        /// <summary>
        /// пока так тестовое поле, обращаться через Student. ко всем остальным
        /// </summary>
        [Reactive]
        public Student? Student { get; set; }
        public ICommand? Save { get; set; }



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
            if (this.Student == null)
            {
                return;
            }
            await API.StudentAPI.PostStudentAsync(this.Student);
        }
    }
}
