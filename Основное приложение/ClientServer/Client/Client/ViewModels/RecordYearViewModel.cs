using Client.ViewModels.Base;
using iText.StyledXmlParser.Node;
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
    public class RecordYearViewModel : ViewModelBase
    {
        /// <summary>
        /// Отображение спинера загрузки
        /// </summary>
        [Reactive]
        public bool IsLoading { get; set; } = false;

        public RecordYearViewModel(IScreen? screen = null) : base(screen)
        {
            IsLoading = true;
            Year = 2023;
            OpenMonthJornal = ReactiveCommand.Create(() =>
            {
                Debug.WriteLine("itwork");
            });

            this.WhenAnyValue(x => x.SelectedMonth).Subscribe(x =>
            {
                if (SelectedMonth == null)
                {
                    return;
                }
                if (screen == null)
                {
                    return;
                }
                screen.Router.Navigate.Execute(new RecordStudentsViewModel(Year, SelectedMonth.Mount, screen));
            });
            this.WhenAnyValue(x => x.Year).Subscribe(x =>
            {
                if (Year == 0)
                {
                    return;
                }
                FillStats();
            });

            IsLoading = false;
        }

        async void FillStats()
        {
            try
            {
                List<MountStat> mountStats = new List<MountStat>();

                var allStudents = await API.StudentAPI.GetStudentsAsync();

                if (allStudents == null)
                {
                    return;
                }

                for (int i = 1; i <= 12; i++)
                {
                    int countALl = allStudents.Count();
                    int countNS = allStudents.Where(x => x.Age > 18).Count();
                    int countNotFound = allStudents.Where(x => x.Status!.Name != "Нет").Count();

                    MountStat mount = new MountStat()
                    {
                        Mount = i,
                        CountAll = countALl,
                        CountNotFound = countNotFound,
                        CountNS = countNS
                    };
                    mountStats.Add(mount);
                }
                YearStats = new List<MountStat>(mountStats);
            }
            catch
            {
                Debug.WriteLine("Error 404 in RecordYear");
            }
        }

        [Reactive]
        public int Year { get; set; } 

        [Reactive]
        public MountStat? SelectedMonth { get; set; }
        [Reactive]
        public List<MountStat>? YearStats { get; set; }

        [Reactive]
        public ICommand OpenMonthJornal { get; set; }
    }

    public class MountStat:ReactiveObject
    {
        public MountStat()
        {

        }
        [Reactive]
        public int Mount { get; set; }

        public int CountAll { get; set; } //человек
        public int CountNS {  get; set; } //несовершеннолетних
        public int CountNotFound { get; set; } //статусных
    }
}
