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
    public class RoomViewModel : ViewModelBase
    {
        [Reactive]
        public List<Room> Rooms { get; set; }
        [Reactive]
        public Room SelectedRoom { get; set; }
        
        public ICommand NewRoom {  get; set; }
        public ICommand Delete {  get; set; }
        public ICommand Save { get; set; }
        public RoomViewModel(IScreen? screen = null) : base(screen) 
        {

        }
    }
}
