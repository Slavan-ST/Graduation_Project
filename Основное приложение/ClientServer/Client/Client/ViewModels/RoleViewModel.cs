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
    public class RoleViewModel : ViewModelBase
    {
        [Reactive]
        public List<Role> Role { get; set; }
        [Reactive]
        public Role SelectedRole { get; set; }

        public ICommand NewRole { get; set; }
        public ICommand Delete { get; set; }
        public ICommand Save { get; set; }

        public RoleViewModel(IScreen? screen = null) : base(screen)
        {

        }
    }
}
