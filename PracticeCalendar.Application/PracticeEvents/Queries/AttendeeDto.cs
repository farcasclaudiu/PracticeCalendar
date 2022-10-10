namespace PracticeCalendar.Application.PracticeEvents.Queries
{
    public class AttendeeDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string EmailAddress { get; set; } = string.Empty;
        public bool IsAttending { get; set; }
    }
}
