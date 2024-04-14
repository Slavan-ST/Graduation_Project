using Avalonia.Controls;
using Client.ViewModels.Base;
using Client.Views;
using Helper.Models.DTO;
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

namespace Client.ViewModels
{
    public class ListStudentsViewModel : ViewModelBase
    {
        public ListStudentsViewModel(IScreen? screen = null) : base(screen)
        {
            this.WhenAnyValue(x => x.SelectedStudent).Subscribe(/* сюда пихнуть обновление UC ProfileStudent т.к. так как он отображается в Popup (см. разметку) */);
            Test();
        }

        [Reactive]
        public Student? SelectedStudent { get; set; }

        [Reactive]
        public UserControl? ProfileStudent { get; set; }
        async void Test()
        {
            var students = await GetStudents();
            if (students != null)
            {
                Students = new List<Student>(students);
            }
            else
            {
                Students = new List<Student>();
            }
        }

        private async Task<IEnumerable<Student>?> GetStudents()
        {
            return await API.StudentAPI.GetStudentsAsync();
        }
        [Reactive]
        public List<Student>? Students { get; set; } 
    }
}
