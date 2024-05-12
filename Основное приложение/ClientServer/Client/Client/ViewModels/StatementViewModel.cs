using Client.API;
using Client.Services;
using Client.ViewModels.Base;
using Helper.Models.Main;
using MsBox.Avalonia;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
using System.Windows.Input;

namespace Client.ViewModels
{
    public class StatementViewModel : ViewModelBase
    {
        /// <summary>
        /// Отображение спинера загрузки
        /// </summary>
        [Reactive]
        public bool IsLoading { get; set; } = false;

        public StatementViewModel(IScreen? screen = null) : base(screen)
        {
            FillStudents();
            ListStudentsSelectedItem = ListStudents.FirstOrDefault();
            IsLoading = true;
            GetEmptyStatement = ReactiveCommand.Create(() =>
            {
                IsLoading = true;
                StatementCreater.CreateStatementEmpty();
                IsLoading = false;
            });
            GetFillStatement = ReactiveCommand.Create(async() =>
            {
                if (ListStudentsSelectedItem == null)
                {
                    await MessageBoxManager.GetMessageBoxStandard("Ошибка", "Выберете студента!").ShowAsync();
                    return;
                }
                IsLoading = true;
                StatementCreater.CreateStatement(ListStudentsSelectedItem,DateOut,DateIn);
                IsLoading = false;
            });
            IsLoading = false;
        }


        public async void FillStudents()
        {
            var students = await StudentAPI.GetStudentsAsync();
            if (students == null)
            {
                return;
            }
            ListStudents = new List<Student>(students);
        }

        [Reactive]
        public Student? ListStudentsSelectedItem { get; set; }
        [Reactive]
        public List<Student> ListStudents { get; set; } = new List<Student>();
        [Reactive]
        public string DateOut { get; set; } = string.Empty;
        [Reactive]
        public string DateIn { get; set; } = string.Empty;
        public ICommand GetEmptyStatement { get; set; }
        public ICommand GetFillStatement { get; set; }
    }
}
