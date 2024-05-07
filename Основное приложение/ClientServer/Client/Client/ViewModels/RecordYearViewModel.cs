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
        public RecordYearViewModel(IScreen? screen = null) : base(screen)
        {
            YearStats = new ObservableCollection<MountStats>() //test data
            {
                new MountStats()
                {
                    Mount = "январь1",
                    CountAll = 155,
                    CountNS = 125,
                    CountNotFound = 12,
                },
                                new MountStats()
                {
                    Mount = "февраль2",
                    CountAll = 122,
                    CountNS = 125,
                    CountNotFound = 12,
                },
                new MountStats()
                {
                    Mount = "апвап3а",
                    CountAll = 155,
                    CountNS = 125,
                    CountNotFound = 12,
                },
                new MountStats()
                {
                    Mount = "пвапва4",
                    CountAll = 155,
                    CountNS = 125,
                    CountNotFound = 12,
                },
                new MountStats()
                {
                    Mount = "январь5",
                    CountAll = 155,
                    CountNS = 125,
                    CountNotFound = 12,
                },
                                new MountStats()
                {
                    Mount = "январь6",
                    CountAll = 155,
                    CountNS = 125,
                    CountNotFound = 12,
                },
                new MountStats()
                {
                    Mount = "январь7",
                    CountAll = 155,
                    CountNS = 125,
                    CountNotFound = 12,
                },
                new MountStats()
                {
                    Mount = "январь8",
                    CountAll = 155,
                    CountNS = 125,
                    CountNotFound = 12,
                },
                new MountStats()
                {
                    Mount = "январь9",
                    CountAll = 155,
                    CountNS = 125,
                    CountNotFound = 12,
                },
                new MountStats()
                {
                    Mount = "январь10",
                    CountAll = 155,
                    CountNS = 125,
                    CountNotFound = 12,
                },
                new MountStats()
                {
                    Mount = "январь11",
                    CountAll = 155,
                    CountNS = 125,
                    CountNotFound = 12,
                },
                new MountStats()
                {
                    Mount = "январь12",
                    CountAll = 155,
                    CountNS = 125,
                    CountNotFound = 12,
                },
            };
            OpenMonthJornal = ReactiveCommand.Create(() =>
            {
                Debug.WriteLine("itwork");
            });
            this.WhenAnyValue(x => x.SelectedMonth).Subscribe(x =>
            {
                Debug.WriteLine("OpenMonthJornal");
            }); 
        }

        [Reactive]
        public MountStats SelectedMonth { get; set; }

        public ObservableCollection<MountStats> YearStats { get; set; }

        [Reactive]
        public ICommand OpenMonthJornal { get; set; }
    }

    public class MountStats
    {
        public string Mount { get; set; } = string.Empty; //месяц
        public int CountAll { get; set; } //человек
        public int CountNS {  get; set; } //несовершеннолетних
        public int CountNotFound { get; set; } //статусных
    }
}
