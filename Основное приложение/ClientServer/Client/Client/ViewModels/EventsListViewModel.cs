using Avalonia.Markup.Xaml.Templates;
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
            FillEventsAsync();
            Save = ReactiveCommand.Create(() =>
            {
                IsLoading = true;
                SaveInAPI();
                IsLoading = false;
            });
            NewEvent = ReactiveCommand.Create(async () =>
            {
                IsLoading = true;
                var temp = new List<EventO>(Events);
                var eventAdd = new EventO();
                temp.Add(eventAdd);
                await EventAPI.PostAsync(eventAdd);
                Events = new List<EventO>(temp);
                IsLoading = false;
            });
            Delete = ReactiveCommand.Create(async (int id) =>
            {
                IsLoading = true;
                var temp = new List<EventO>(Events);
                var eventRemove = temp.Where(x => x.Id == id).FirstOrDefault();

                if (eventRemove == null)
                {
                    return;
                }

                temp.Remove(eventRemove);
                Events = new List<EventO>(temp);

                await EventAPI.DeleteAsync(eventRemove.Id);
            });
        }

        async void FillEventsAsync()
        {
            IsLoading = true;
            var events = await EventAPI.GetsAsync();
            if (events == null)
            {
                return;
            }
            Events = events.ToList();
            IsLoading = false;
        }

        [Reactive]
        public List<EventO> Events { get; set; } = new List<EventO>();
        public ICommand Save { get; set; }
        public ICommand NewEvent { get; set; }
        public ICommand Delete { get; set; }

        async void SaveInAPI()
        {
            IsLoading = true;
            foreach (var eventO in Events)
            {
                await EventAPI.PostAsync(eventO);
            }
            IsLoading = false;
        }
    }
}
