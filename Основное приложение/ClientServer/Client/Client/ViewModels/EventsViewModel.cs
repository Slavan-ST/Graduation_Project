using Client.API;
using Client.ViewModels.Base;
using Helper.Models.Main;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading;
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
            FillEventsAsync();
        }
        
        async void FillEventsAsync()
        {
            IsLoading = true;
            var events = await EventAPI.GetsAsync();
            if (events == null)
            {
                IsLoading = false;
                return;
            }
            Events = events.ToList();
            IsLoading = false;
        }

        [Reactive]
        public List<EventO> Events { get; set; } = new List<EventO>();
    }

}
