using Ardalis.GuardClauses;
using PracticeCalendar.Domain.Common;

namespace PracticeCalendar.Domain.Entities
{
    /// <summary>
    /// Attendee to an event
    /// </summary>
    public class Attendee: EntityBase
    {
        public Attendee(string name, string emailAddress)
        {
            Guard.Against.NullOrEmpty(name);
            Guard.Against.NullOrEmpty(emailAddress);
            Name = name;
            EmailAddress = emailAddress;
        }

        public int PracticeEventId { get; private set; }
        public string Name { get; set; } = string.Empty;
        public string EmailAddress { get; set; } = string.Empty;
        public bool IsAttending { get; private set; }
        
        /// <summary>
        /// Set if the Attendee is attending
        /// </summary>
        /// <param name="isAttending"></param>
        public void SetIsAttending (bool isAttending)
        {
            this.IsAttending = isAttending;
            //TODO - raise event
        }

        /// <summary>
        /// Assign Attendee to the practice event
        /// </summary>
        /// <param name="practiceEventId"></param>
        public void AssignToEvent(int practiceEventId)
        {
            this.PracticeEventId = practiceEventId;
        }
    }
}
