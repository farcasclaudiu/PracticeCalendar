using PracticeCalendar.Domain.Common;

namespace PracticeCalendar.Domain.Exceptions
{
    public class InvalidAttendeeException : DomainException
    {
        private int attendeeId;

        public InvalidAttendeeException(int attendeeId)
        {
            this.attendeeId = attendeeId;
        }

        public override string Message => $"Invalid Attendee with id: {attendeeId}";
    }
}
