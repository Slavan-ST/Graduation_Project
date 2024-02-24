using Client.Models;
using Client.Services;
using Helper.Models.Main;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Client.ViewModels
{
    public class AuthViewModel : ViewModelBase
    {
        /// <summary>
        /// Конструктор AuthViewModel
        /// </summary>
        public AuthViewModel(IScreen screen) : base(screen)
        {
            Auth = ReactiveCommand.Create(async () =>
            {

            });
        }

        [Reactive]
        public string? Login {  get; set; }

        [Reactive]
        public string? Password { get; set; }


        /// <summary>
        /// Команда авторизации
        /// </summary>
        public ICommand Auth { get; set; }

    }
}
