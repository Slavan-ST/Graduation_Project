namespace Helper.Models.Main
{
    public class Status : Base
    {
        public string Name { get; set; } = string.Empty;

        public override string ToString()
        {
            return Name;
        }

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
                        stud.Status = null;
                    }
                }
                return _students;
            }
            set
            {
                _students = value;
            }
        }

    }
}
