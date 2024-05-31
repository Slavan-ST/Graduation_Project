using Helper.Models.Main;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;
using System.Windows.Input;

namespace Client.Models
{
    public class StudentInRoom : ReactiveObject
    {
        public Student? Student { get; set; }
        [Reactive]
        public string Mark { get; set; } = "-";

        public ICommand MarkExist { get; set; }
        public ICommand MarkNotExist { get; set; }
        public ICommand MarkClaimed { get; set; }
        public ICommand MarkLate { get; set; }

        public StudentInRoom()
        {
            MarkExist = ReactiveCommand.Create(() =>
            {
                Mark = "+";
            });

            MarkClaimed = ReactiveCommand.Create(() =>
            {
                Mark = "З";
            });

            MarkNotExist = ReactiveCommand.Create(() =>
            {
                Mark = "H";
            });

            MarkLate = ReactiveCommand.Create(() =>
            {
                Mark = "O";
            });
        }
    }
}
