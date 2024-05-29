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
using System.Threading;
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
            ThreadPool.QueueUserWorkItem(FillEventsAsync);
            Save = ReactiveCommand.Create(() =>
            {
                IsLoading = true;
                SaveInAPIAsync();
                IsLoading = false;
            });
            NewEvent = ReactiveCommand.Create(async () =>
            {
                try
                {
                    IsLoading = true;
                    var eventAdd = new EventO();
                    int? id = await EventAPI.PostAsync(eventAdd);
                    if (id == null)
                    {
                        IsLoading = false;
                        return;
                    }
                    eventAdd.Id = (int)id;
                    var temp = new List<EventO>(Events)
                    {
                        eventAdd
                    };
                    Events = new List<EventO>(temp);
                    IsLoading = false;
                }
                catch (Exception ex)
                {
                    Debug.WriteLine(ex);
                }
            });
            Delete = ReactiveCommand.Create(async (int id) =>
            {
                IsLoading = true;
                var temp = new List<EventO>(Events);
                var eventRemove = temp.Where(x => x.Id == id).FirstOrDefault();

                if (eventRemove == null)
                {
                    IsLoading = false;
                    return;
                }


                await EventAPI.DeleteAsync(eventRemove.Id);
                temp.Remove(eventRemove);
                Events = new List<EventO>(temp);
                IsLoading = false;
            });
        }


        [Reactive]
        public List<EventO> Events { get; set; } = new List<EventO>();
        public ICommand Save { get; set; }
        public ICommand NewEvent { get; set; }
        public ICommand Delete { get; set; }

        async void SaveInAPIAsync()
        {
            IsLoading = true;
            foreach (var eventO in Events)
            {
                await EventAPI.PostAsync(eventO);
            }
            IsLoading = false;
        }

        async void FillEventsAsync(object? state)
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
    }
}
