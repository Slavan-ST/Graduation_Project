namespace Helper.Models.Main
{
    public class EventO : Base
    {
        public string Name { get; set; } = string.Empty;
        public string? Description { get; set; } = string.Empty;
        public string? Location { get; set; } = string.Empty;
        public string? Organizer { get; set; } = string.Empty;
        public DateTime Date { get; set; } = DateTime.Now;
        public string ShortDateTime { get => Date.ToShortDateString() + " " + Date.ToShortTimeString(); }
    }
}
