using Ardalis.GuardClauses;
using PracticeCalendar.Domain.Common;
using PracticeCalendar.Domain.Common.Interfaces;
using PracticeCalendar.Domain.Events;
using PracticeCalendar.Domain.Exceptions;

namespace PracticeCalendar.Domain.Entities
{
    /// <summary>
    /// Practice event aggregate
    /// </summary>
    public class PracticeEvent : EntityBase, IAggregateRoot
    {
        public PracticeEvent(string title, string description, DateTime startTime, DateTime endTime)
        {
            Guard.Against.NullOrEmpty(title, nameof(title));
            Guard.Against.NullOrEmpty(description, nameof(description));
            this.Title = title;
            this.Description = description;
            this.StartTime = startTime;
            this.EndTime = endTime;
        }

        public string Title { get; private set; } = string.Empty;
        public string Description{ get; private set; } = string.Empty;

        public IList<Attendee> Attendees { get; private set; } = new List<Attendee>();

        public DateTime StartTime { get; private set; }
        public DateTime EndTime { get; private set; }

        public void AddAttendee(Attendee attendee)
        {
            Guard.Against.Null(attendee, nameof(attendee));
            attendee.AssignToEvent(this.Id);
            Attendees.Add(attendee);

            var attendeeAddedEvent = new AttendeeAddedEvent(this, attendee);
            base.RegisterDomainEvent(attendeeAddedEvent);
        }

        public void UpdateTitleAndDescription(string title, string description)
        {
            this.Title = title;
            this.Description = description;

            var titleDescUpdatedEvent = new EventUpdateTitleAndDescriptionEvent(this);
            base.RegisterDomainEvent(titleDescUpdatedEvent);
        }

        public void AttendeeAcceptEvent(int attendeeId)
        {
            var attendee = this.Attendees.FirstOrDefault(x => x.Id == attendeeId);
            if (attendee == null)
                throw new InvalidAttendeeException(attendeeId);
            attendee.SetIsAttending(true);

            var attendeeAcceptedEvent = new AttendeeAcceptedEvent(this, attendee);
            base.RegisterDomainEvent(attendeeAcceptedEvent);
        }

        public void AttendeeDeclineEvent(int attendeeId)
        {
            var attendee = this.Attendees.FirstOrDefault(x => x.Id == attendeeId);
            if (attendee == null)
                throw new InvalidAttendeeException(attendeeId);
            attendee.SetIsAttending(false);

            var attendeeDeclinedEvent = new AttendeeDeclinedEvent(this, attendee);
            base.RegisterDomainEvent(attendeeDeclinedEvent);
        }
    }
}
