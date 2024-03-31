using Client.ViewModels.Base;
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
        public EventsViewModel(IScreen? screen = null) : base(screen)
        {
        
        }

        [Reactive] //test data
        public ObservableCollection<Event> Events { get; set; } = new ObservableCollection<Event>()
        {
            new Event()
            {
                Name = "event1",
                Description = "desc1",
                Date = "12.12.2024",
                Employes = "worker1",
                Place = "plc1"
            },

            new Event()
            {
                Name = "event2",
                Description = "desc2",
                Date = "12.02.2024",
                Employes = "worker1",
                Place = "plc2"
            },

            new Event()
            {
                Name = "event3",
                Description = "desc3",
                Date = "05.02.2024",
                Employes = "worker2",
                Place = "plc3"
            },

            new Event()
            {
                Name = "event4",
                Description = "desc4",
                Date = "05.02.2023",
                Employes = "worker3",
                Place = "plc1"
            },

                        new Event()
            {
                Name = "event1",
                Description = "desc1",
                Date = "12.12.2024",
                Employes = "worker1",
                Place = "plc1"
            },

            new Event()
            {
                Name = "event2",
                Description = "desc2",
                Date = "12.02.2024",
                Employes = "worker1",
                Place = "plc2"
            },

            new Event()
            {
                Name = "event3",
                Description = "desc3",
                Date = "05.02.2024",
                Employes = "worker2",
                Place = "plc3"
            },

            new Event()
            {
                Name = "event4",
                Description = "desc4",
                Date = "05.02.2023",
                Employes = "worker3",
                Place = "plc1"
            },
        };
    }

}
