using Client.ViewModels.Base;
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
    public class RestorePasswordViewModel : ViewModelBase
    {
        [Reactive]
        public bool IsLoading { get; set; }
        [Reactive]
        public string Login {  get; set; } = string.Empty;
        [Reactive]
        public string NewPassword { get; set; } = string.Empty;
        public ICommand Restore {  get; set; }

        public RestorePasswordViewModel(IScreen? screen = null) : base(screen)
        {
            Restore = ReactiveCommand.Create(async() =>
            {
                IsLoading = true;

                await API.UserAPI.PutUserAsync(Login, NewPassword);

                IsLoading = false;
            });
        }
    }
}
