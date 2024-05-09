using Client.Models;
using Client.Services;
using Client.ViewModels.Base;
using Helper.Models.DTO;
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
        public AuthViewModel(IScreen? screen = null) : base(screen)
        {
            Debug.WriteLine("test");
            Auth = ReactiveCommand.Create(async () =>
            {
                IsLoading = true;
                //поля не заполнены? заполни!
                if (string.IsNullOrWhiteSpace(Login) || string.IsNullOrWhiteSpace(Password))
                {
                    IsLoading = false;
                    Message.Show("Error", "Заполните все поля!");
                    return;
                }
                //получаем пользователя
                UserDTO? user = await API.HomeAPI.SignIn(Login, Password);
                //если пользователя получили, то переходим на главную
                if (user == null)
                {
                    IsLoading = false;
                    Message.Show("Error", "Пользователь не найден!");
                    return;
                }
                IsLoading = false;
                Services.Authorization.GetAuthorization().IsEmployee = user.Role!.Name == "Сотрудник";
                HostScreen.Router.Navigate.Execute(new MainMenuViewModel(HostScreen));
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

        /// <summary>
        /// Отображение спинера загрузки
        /// </summary>
        [Reactive]
        public bool IsLoading { get; set; } = false;
    }
}
