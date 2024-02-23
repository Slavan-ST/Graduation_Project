using Client.Services;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive;
using System.Text;
using System.Threading.Tasks;

namespace Client.ViewModels
{
    public class AuthViewModel : ViewModelBase
    {
        /// <summary>
        /// Конструктор AuthViewModel
        /// </summary>
        public AuthViewModel(IScreen screen) : base(screen)
        {
            Auth = ReactiveCommand.Create(() =>
            {
                // Пока так
                if(Login == "admin" && Password == "admin")
                {
                    Navigation.WorkPlace = Navigation.News!;
                    Navigation.MainWindow!.Content = Navigation.MainMenu;
                }
            });
        }

        #region Properties

        /// <summary>
        /// Логин пользователя
        /// </summary>
        [Reactive]
        public string? Login {  get; set; }
        /// <summary>
        /// Пароль пользователя
        /// </summary>
        [Reactive]
        public string? Password { get; set; }

        #endregion

        #region Commands

        /// <summary>
        /// Команда авторизации
        /// </summary>
        public ReactiveCommand<Unit, Unit> Auth { get; set; }

        #endregion
    }
}
