using Client.ViewModels.Base;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client.ViewModels
{
    public class ProfileViewModel : ViewModelBase
    {
        public ProfileViewModel(IScreen? screen = null) : base(screen)
        {

        }

        [Reactive]
        public string TestText { get; set; } = "test";
        [Reactive]
        public string Fio { get; set; } = "test";
        [Reactive]
        public string NumberRoom { get; set; } = "test";
        [Reactive]
        public string MarkClear { get; set; } = "test";
        [Reactive]
        public string StatusUser { get; set; } = "test";
    }
}
