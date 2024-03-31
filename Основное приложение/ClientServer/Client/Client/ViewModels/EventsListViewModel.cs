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
    public class Event : ReactiveObject // test class for test data
    {
        
        public string? Name { get; set;}
        public string? Description { get; set;}
        public string? Date {  get; set;}
        public string? Employes { get; set;}
        public string? Place { get; set;}
        public Event()
        {

        }

        public override string ToString() => Name.ToString();

        public Event(string name) 
        {
            Name = name;
        }
    }

    public class EventsListViewModel : ViewModelBase
    {
        public EventsListViewModel(IScreen? screen = null) : base(screen)
        {
        
        }
    }
}
