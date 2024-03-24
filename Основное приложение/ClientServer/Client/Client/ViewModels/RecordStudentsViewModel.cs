using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Client.ViewModels
{
    public class Person
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public bool IsFictitious { get; set; }

        public Person(string firstName, string lastName, bool isFictitious)
        {
            FirstName = firstName;
            LastName = lastName;
            IsFictitious = isFictitious;
        }
    }

    public class RecordStudentsViewModel : ViewModelBase
    { 
        public ObservableCollection<Person> People { get; }

        public RecordStudentsViewModel(IScreen? screen = null) : base(screen)
        {
            var people = new List<Person>
            {
                new Person("Neil", "Armstrong", false),
                new Person("Buzz", "Lightyear", true),
                new Person("James", "Kirk", true)
            };

            People  = new ObservableCollection<Person>(people);
        }

        void MakeTable()
        {
            DataTable dataTable = new DataTable();

            DataColumn dataColumn = new DataColumn();
            for (int i = 0; i < ; i++)
            {
                dataColumn.ColumnName = 
            }

        }
    }
}
