using Client.ViewModels.Base;
using Helper.Models.Main;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;
using System.Collections.Generic;

namespace Client.ViewModels
{
    public class CleanLineRaidViewModel : ViewModelBase
    {
        [Reactive]
        public Room? SelectedRoom { get; set; } // для comboBox
        [Reactive]
        public List<Room>? Rooms { get; set; } // для comboBox, загружается только при старте страницы

        public CleanLineRaidViewModel(IScreen? screen = null) : base(screen) 
        {

        }
    }
}