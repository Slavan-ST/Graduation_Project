using Client.ViewModels.Base;
using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Client.ViewModels
{
    public class EditableItemViewModel(string name) : ViewModelBase
    {
        private ObservableCollection<EditableItemViewModel>? _items;

        public string Name
        {
            get => name;
            set => this.RaiseAndSetIfChanged(ref name, value);
        }

        public ObservableCollection<EditableItemViewModel>? Items
        {
            get => _items;
            set => this.RaiseAndSetIfChanged(ref _items, value);
        }

        public override string ToString() => name;
    }
}
