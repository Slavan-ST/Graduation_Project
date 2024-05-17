using Client.ViewModels.Base;
using Helper.Models.Main;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
            Initialize();
        }

        /// <summary>
        /// пока так тестовое поле, обращаться через Student. ко всем остальным
        /// </summary>
        [Reactive]
        public Student? Student { get; set; }
        public ICommand? Save { get; set; }

        //Student
        //[Reactive]
        //public string Name { get; set; } = "test";
        //[Reactive]
        //public string Surname { get; set; } = "test";
        //[Reactive]
        //public string Patronymic { get; set; } = "test";
        //[Reactive]
        //public string Phone { get; set; } = "test";
        //[Reactive]
        //public string Gender { get; set; } = "-";
        //[Reactive]
        //public DateTime DateBirthday { get; set; }
        //[Reactive]
        //public Status? Status { get; set; }
        //[Reactive]
        //public Group? Group { get; set; }
        //[Reactive]
        //public Room? Room { get; set; }

        ////Representative
        //[Reactive]
        //public string RepName { get; set; } = "test";
        //[Reactive]
        //public string RepSurname { get; set; } = "test";
        //[Reactive]
        //public string RepPatronymic { get; set; } = "test";
        //[Reactive]
        //public string RepPhone { get; set; } = "test";




        void Initialize()
        {
            IsLoading = true;
            IsWorker = true;
            Save = ReactiveCommand.Create(() =>
            {
                IsLoading = true;
                IsLoading = false;
                SaveStudentInApi();
            });
            IsLoading = false;
        }
        void Initialize(Student student)
        {
            Initialize();
            FillLines(student);
        }
        void FillLines(Student student)
        {
            //this.Name = student.Name;
            //this.Surname = student.Surname;
            //this.Patronymic = student.Patronymic;
            //this.Phone = student.Phone;
            //this.DateBirthday = student.DateBirthday;
            //this.Status = student.Status;
            //this.Group = student.Group;
            //this.Gender = student.Gender;
            //this.RepName = student.RepresentativeName;
            //this.RepSurname = student.RepresentativeSurname;
            //this.RepPatronymic = student.RepresentativePatronymic;
            //this.RepPhone = student.RepresentativePhone;
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
