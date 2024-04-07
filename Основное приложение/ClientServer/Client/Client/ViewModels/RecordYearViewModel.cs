using Client.ViewModels.Base;
using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
        }

        public ObservableCollection<MountStats> YearStats { get; set; }
    }

    public class MountStats
    {
        public string Mount { get; set; } = string.Empty;
        public int CountAll { get; set; }
        public int CountNS {  get; set; }
        public int CountNotFound { get; set; }
    }
}
