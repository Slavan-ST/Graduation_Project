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
    public class EventsViewModel : ViewModelBase
    {
        /// <summary>
        /// Отображение спинера загрузки
        /// </summary>
        [Reactive]
        public bool IsLoading { get; set; } = false;

        public EventsViewModel(IScreen? screen = null) : base(screen)
        {
            IsLoading = true;
            FillEventsAsync();
            IsLoading = false;
        }
        async void FillEventsAsync()
        {
            var events = await EventAPI.GetsAsync();
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
