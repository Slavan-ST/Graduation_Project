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
    public class GroupViewModel : ViewModelBase
    {
        [Reactive]
        public List<Group> Statuses { get; set; }
        [Reactive]
        public Group SelectedGroup { get; set; }

        public ICommand NewGroup { get; set; }
        public ICommand Delete { get; set; }
        public ICommand Save { get; set; }

        public GroupViewModel(IScreen? screen = null) : base(screen)
        {

        }
    }
}
