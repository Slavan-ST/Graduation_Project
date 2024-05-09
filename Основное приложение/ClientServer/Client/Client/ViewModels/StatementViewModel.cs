using Client.Services;
using Client.ViewModels.Base;
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
            IsLoading = true;
            GetEmptyStatement = ReactiveCommand.Create(() =>
            {
                IsLoading = true;
                StatementCreater.CreateStatementEmpty();
                IsLoading = false;
            });
            GetFillStatement = ReactiveCommand.Create(() =>
            {
                IsLoading = true;
                StatementCreater.CreateStatement(
                    Name, 
                    Surname, 
                    Patronymic, 
                    Phone, 
                    Room, 
                    DateOut, 
                    DateIn, 
                    Address, 
                    NameRepresentative, 
                    SurnameRepresentative, 
                    PatronymicRepresentative, 
                    PhoneRepresentative);
                IsLoading = false;
            });
            IsLoading = false;
        }
        [Reactive]
        public string Address { get; set; } = string.Empty;
        [Reactive]
        public string Surname { get; set; } = string.Empty;
        [Reactive]
        public string Name { get; set; } = string.Empty;
        [Reactive]
        public string Patronymic { get; set; } = string.Empty;
        [Reactive]
        public string Room { get; set; } = string.Empty;
        [Reactive]
        public string Phone { get; set; } = string.Empty;
        [Reactive]
        public string DateOut { get; set; } = string.Empty;
        [Reactive]
        public string DateIn { get; set; } = string.Empty;
        [Reactive]
        public string SurnameRepresentative { get; set; } = string.Empty;
        [Reactive]
        public string NameRepresentative { get; set; } = string.Empty;
        [Reactive]
        public string PatronymicRepresentative { get; set; } = string.Empty;
        [Reactive]
        public string PhoneRepresentative { get; set; } = string.Empty;

        public ICommand GetEmptyStatement { get; set; }
        public ICommand GetFillStatement { get; set; }
    }
}
