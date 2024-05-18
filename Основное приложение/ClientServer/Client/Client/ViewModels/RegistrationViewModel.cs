using Client.API;
using Client.ViewModels.Base;
using Helper.Models.DTO;
using Helper.Models.Main;
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
    public class RegistrationViewModel : ViewModelBase
    {
        [Reactive]
        public bool IsLoading { get; set; } = false;
        [Reactive]
        public User? NewUser { get; set; } = new User();
        [Reactive]
        public List<Role>? Roles { get; set; }
        public ICommand Create {  get; set; }
        public RegistrationViewModel(IScreen? screen = null) : base(screen)
        {
            Fill();
            Create = ReactiveCommand.Create(async () =>
            {
                IsLoading = true;

                if (NewUser.Role != null)
                {
                    NewUser.RoleId = NewUser.Role.Id;
                }
                else
                {
                    NewUser.RoleId = 2;
                }
                await API.UserAPI.PostUserAsync(NewUser);

                IsLoading = false;
            });
        }
        async void Fill()
        {
            IsLoading = true;
            var roles = await RolesAPI.GetAsync();
            if (roles == null)
            {
                IsLoading = false;
                return;
            }
            Roles = new List<Role>(roles);
            IsLoading = false;
        }
    }
}
