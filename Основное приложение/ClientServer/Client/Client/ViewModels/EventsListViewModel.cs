using Avalonia.Markup.Xaml.Templates;
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
using System.Windows.Input;

namespace Client.ViewModels
{
    public class EventsListViewModel : ViewModelBase
    {
        /// <summary>
        /// Отображение спинера загрузки
        /// </summary>
        [Reactive]
        public bool IsLoading { get; set; } = false;

        public EventsListViewModel(IScreen? screen = null) : base(screen)
        {
            IsLoading = true;
            FillEventsAsync();
            Save = ReactiveCommand.Create(() =>
            {
                IsLoading = true;
                SaveInAPI();
                IsLoading = false;
            });
            NewEvent = ReactiveCommand.Create(() =>
            {
                var temp = new List<EventO>(Events);
                temp.Add(new EventO());

                Events = new List<EventO>(temp);
            });
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
        public ICommand Save { get; set; }
        public ICommand NewEvent { get; set; }

        async void SaveInAPI()
        {
            foreach (var eventO in Events)
            {
                await EventAPI.PostAsync(eventO);
            }
        }
    }
}
