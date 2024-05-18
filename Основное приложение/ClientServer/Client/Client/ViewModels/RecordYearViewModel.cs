using Client.ViewModels.Base;
using iText.StyledXmlParser.Node;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Globalization;
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
        [Reactive]
        public int CurrentYear { get; set; } = DateTime.Now.Year;

        public ICommand NextYear { get; set; }

        public RecordYearViewModel(IScreen? screen = null) : base(screen)
        {
            NextYear = ReactiveCommand.Create(() =>
            {
                IsLoading = true;
                CurrentYear += 1;
                IsLoading = false;
            });

            this.WhenAnyValue(x => x.SelectedMonth).Subscribe(x =>
            {
                IsLoading = true;
                if (SelectedMonth == null)
                {
                    IsLoading = false;
                    return;
                }
                if (screen == null)
                {
                    IsLoading = false;
                    return;
                }
                screen.Router.Navigate.Execute(new RecordStudentsViewModel(CurrentYear, SelectedMonth.Month, screen));
                IsLoading = false;
            });
            this.WhenAnyValue(x => x.CurrentYear).Subscribe(x =>
            {
                IsLoading = true;
                if (CurrentYear == 0)
                {
                    IsLoading = false;
                    return;
                }
                FillStats();
                IsLoading = false;
            });
        }

        async void FillStats()
        {
            IsLoading = true;
            try
            {
                List<MountStat> mountStats = new List<MountStat>();

                var allStudents = await API.StudentAPI.GetStudentsAsync();

                if (allStudents == null)
                {
                    IsLoading = false;
                    return;
                }

                for (int i = 1; i <= 12; i++)
                {
                    int countALl = allStudents.Count();
                    int countNS = allStudents.Where(x => x.Age > 18).Count();
                    int countNotFound = allStudents.Where(x => x.Status!.Name != "Нет").Count();

                    MountStat mount = new MountStat()
                    {
                        Month = i,
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
            IsLoading = false;
        }

        [Reactive]
        public MountStat? SelectedMonth { get; set; }
        [Reactive]
        public List<MountStat>? YearStats { get; set; }
    }

    public class MountStat:ReactiveObject
    {
        public MountStat()
        {

        }
        [Reactive]
        public int Month { get; set; }

        public int CountAll { get; set; } //человек
        public int CountNS {  get; set; } //несовершеннолетних
        public int CountNotFound { get; set; } //статусных

        public string Name
        {
            get => CultureInfo.CurrentCulture.DateTimeFormat.GetAbbreviatedMonthName(Month);
        }
    }
}
