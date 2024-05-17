using Helper.Models.Main;
using ReactiveUI.Fody.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Client.ViewModels
{
    public class StatusViewModel
    {
        [Reactive]
        public List<Status> Statuses { get; set; }
        [Reactive]
        public Status SelectedStatus { get; set; }

        public ICommand NewStatus { get; set; }
        public ICommand Delete { get; set; }
        public ICommand Save { get; set; }
    }
}
