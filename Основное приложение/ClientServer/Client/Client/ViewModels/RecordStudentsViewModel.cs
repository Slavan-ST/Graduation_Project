using Avalonia.Controls;
using Avalonia.Controls.Models.TreeDataGrid;
using Client.API;
using Client.ViewModels.Base;
using Helper.Models.DTO;
using Helper.Models.Main;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Data.Common;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Client.ViewModels
{
    public class Person
    {
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public int Age { get; set; }
    }

    public class RecordStudentsViewModel : ViewModelBase
    {
        private ObservableCollection<Person> _people = new()
        {
            new Person { FirstName = "Eleanor", LastName = "Pope", Age = 32 },
            new Person { FirstName = "Jeremy", LastName = "Navarro", Age = 74 },
            new Person { FirstName = "Lailah ", LastName = "Velazquez", Age = 16 },
            new Person { FirstName = "Jazmine", LastName = "Schroeder", Age = 52 },
        };

        public RecordStudentsViewModel(IScreen? screen = null) : base(screen)
        {
            Source = new FlatTreeDataGridSource<Person>(_people)
            {
                Columns =
                {
                    new TextColumn<Person, string>("First Name", x => x.FirstName),
                    new TextColumn<Person, string>("Last Name", x => x.LastName),
                    new TextColumn<Person, int>("Age", x => x.Age),
                },
            };
        }

        public FlatTreeDataGridSource<Person> Source { get; }

    }
}
