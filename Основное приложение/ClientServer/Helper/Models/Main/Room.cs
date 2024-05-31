namespace Helper.Models.Main
{
    public class Room : Base
    {
        public string Number { get; set; } = "";

        public int CountStudents
        {
            get
            {
                if (Students == null)
                {
                    return 0;
                }
                return Students.Count;
            }
        }

        List<Student>? _students = [];
        public List<Student>? Students
        {
            get
            {
                if (_students != null)
                {
                    foreach (var stud in _students)
                    {
                        stud.Room = null;
                    }
                }
                return _students;
            }
            set
            {
                _students = value;
            }
        }
        List<PurityRaidLog>? _purityRaidLogs = [];
        public List<PurityRaidLog>? PurityRaidLogs
        {
            get
            {
                if (_purityRaidLogs != null)
                {
                    foreach (var log in _purityRaidLogs)
                    {
                        log.Room = null;
                    }
                }
                return _purityRaidLogs;
            }
            set
            {
                _purityRaidLogs = value;
            }
        }

        public override string ToString()
        {
            return Number;
        }
    }
}
