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
    public class RegistrationViewModel : ViewModelBase
    {
        [Reactive]
        public bool IsLoading { get; set; } = false;
        [Reactive]
        public User NewUser { get; set; } = new User();
        [Reactive]
        public List<Role> Roles { get; set; }
        [Reactive]
        public Role SelectedRole { get; set; }
        public ICommand Create {  get; set; }
        public RegistrationViewModel(IScreen? screen = null) : base(screen)
        {
            Create = ReactiveCommand.Create(() =>
            {
                IsLoading = true;
                // сохранение пользователя в бд
                IsLoading = false;
            })
        }
    }
}
