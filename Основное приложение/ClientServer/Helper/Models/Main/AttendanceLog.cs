namespace Helper.Models.Main
{
    public class AttendanceLog : Base
    {
        public AttendanceLog()
        {

        }
        public int StudentId { get; set; }
        public DateTime Date { get; set; }
        public string Marker { get; set; } = string.Empty;

        Student? _student;
        public Student? Student
        {
            get
            {
                if (_student == null)
                {
                    return null;
                }
                _student.AttendanceLogs = null;
                return _student;
            }
            set
            {
                _student = value;
            }
        }
    }
}
