using Client.ViewModels.Base;
using Helper.Models.DTO;
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
            Test();

        }

        async void Test()
        {
            var students = await GetStudents();
            if (students != null)
            {
                Students = new List<StudentDTO>(students);
            }
            else
            {
                Students = new List<StudentDTO>();
            }
        }

        private async Task<IEnumerable<StudentDTO>?> GetStudents()
        {
            return await API.Student.GetStudentsAsync();
        }
        [Reactive]
        public List<StudentDTO>? Students { get; set; } 
    }
}
