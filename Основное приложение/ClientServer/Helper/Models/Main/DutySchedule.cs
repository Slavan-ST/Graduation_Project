namespace Helper.Models.Main
{
    public class DutySchedule : Base
    {
        public int? StudentId { get; set; }
        public DateTime Date { get; set; }
        public string? ShortDate
        {
            get => Date.ToShortDateString();
        }


        Student? _student;
        public Student? Student
        {
            get
            {
                if (_student == null)
                {
                    return null;
                }
                _student.DutySchedules = null;
                return _student;
            }
            set
            {
                _student = value;
            }
        }
    }
}
