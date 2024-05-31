namespace Helper.Models.Main
{
    public class Group : Base
    {
        public string Name { get; set; } = string.Empty;
        public override string ToString()
        {
            return this.Name;
        }


        public int CountStudents
        {
            get
            {
                if (Students == null)
                {
                    return 0;
                }
                return Students.Count();
            }
        }
        IEnumerable<Student>? _students = [];
        public IEnumerable<Student>? Students
        {
            get
            {
                if (_students != null)
                {
                    foreach (var stud in _students)
                    {
                        stud.Group = null;
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
