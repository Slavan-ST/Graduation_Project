using Client.API;
using Client.ViewModels.Base;
using Helper.Models.Main;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client.ViewModels
{
    public class EventsListViewModel : ViewModelBase
    {
        public EventsListViewModel(IScreen? screen = null) : base(screen)
        {
            FillEventsAsync();
        }
        async void FillEventsAsync()
        {
            var events = await EventAPI.GetEventsAsync();
            if (events == null)
            {
                return;
            }
            Events = events.ToList();
        }

        [Reactive]
        public List<EventO> Events { get; set; } = new List<EventO>();
    }
}
