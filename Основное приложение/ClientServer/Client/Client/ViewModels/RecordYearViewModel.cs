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
    public class RecordYearViewModel : ViewModelBaseNavigator
    {
        public RecordYearViewModel(IScreen? screen = null) : base(screen)
        {
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
                Router.Navigate.Execute(new RecordStudentsViewModel(Year, SelectedMonth.Mount, this));
            });
            this.WhenAnyValue(x => x.Year).Subscribe(x =>
            {
                if (Year == 0)
                {
                    return;
                }
                FillStats();
            });
        }

        async void FillStats()
        {
            try
            {
                List<MountStat> mountStats = new List<MountStat>();
                var logs = await API.AttendanceLogAPI.GetAttendanceLogsYear(Year);

                if (logs == null)
                {
                    return;
                }

                for (int i = 1; i <= 12; i++)
                {
                    var sortLogs = logs.Where(x => x.Date.Year == Year && x.Date.Month == i).ToList();
                    var allStudents = logs.DistinctBy(x => x.Student).ToList();
                    int countALl = allStudents.Count();
                    int countNS = allStudents.Where(x => x.Student!.Age > 18).Count();
                    int countNotFound = allStudents.Where(x => x.Student!.Status!.Name != "Нет").Count();

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
