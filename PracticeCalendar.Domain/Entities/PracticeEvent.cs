﻿using Ardalis.GuardClauses;
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
        public PracticeEvent(string title, string description)
        {
            Guard.Against.NullOrEmpty(title, nameof(title));
            Guard.Against.NullOrEmpty(description, nameof(description));
            this.Title = title;
            this.Description = description;
        }

        public string Title { get; private set; } = string.Empty;
        public string Description{ get; private set; } = string.Empty;

        private List<Attendee> attendees = new List<Attendee>();
        public IEnumerable<Attendee> Attendees => attendees.AsReadOnly();

        public DateTime StartTime { get; private set; }
        public DateTime EndTime { get; private set; }

        public void AddAttendee(Attendee attendee)
        {
            Guard.Against.Null(attendee, nameof(attendee));
            attendee.AssignToEvent(this.Id);
            attendees.Add(attendee);

            var attendeeAddedEvent = new AttendeeAddedEvent(this, attendee);
            base.RegisterDomainEvent(attendeeAddedEvent);
        }

        public void UpdateTitleAndDescription(string title, string description)
        {
            this.Title = title;
            this.Description = description;
        }

        public void AttendeeAcceptEvent(int attendeeId)
        {
            var attendee = this.Attendees.FirstOrDefault(x => x.Id == attendeeId);
            if (attendee == null)
                throw new InvalidAttendeeException(attendeeId);
            attendee.SetIsAttending(true);
        }

        public void AttendeeDeclineEvent(int attendeeId)
        {
            var attendee = this.Attendees.FirstOrDefault(x => x.Id == attendeeId);
            if (attendee == null)
                throw new InvalidAttendeeException(attendeeId);
            attendee.SetIsAttending(false);
        }
    }
}