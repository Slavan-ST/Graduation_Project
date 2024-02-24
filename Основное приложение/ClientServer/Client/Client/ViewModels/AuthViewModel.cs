using Client.Models;
using Client.Services;
using Helper.Models.Main;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;
using System;
using System.Collections.Generic;
using System.Diagnostics;
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
            Debug.WriteLine("test");
            Auth = ReactiveCommand.Create(async () =>
            {
                User? user = await Authorization.AuthorizationUser(Login, Password);
                if (user != null)
                {
                    HostScreen.Router.Navigate.Execute(new MainMenuViewModel(HostScreen));
                }
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
