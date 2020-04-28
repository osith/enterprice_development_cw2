using System;

namespace CW_2.Models
{
    public class EventModel
    {
        public class EventDTO
        {
            public Guid Id { get; set; }
            public string Description { get; set; }
            public short Type { get; set; }
            public Nullable<bool> Recurring { get; set; }
            public Nullable<int> RecurringType { get; set; }
            public Guid UserDataId { get; set; }
            public Nullable<DateTime> CreatedAt { get; set; }
            public Nullable<bool> IsDeleted { get; set; }
        }

        public class AppointmentDTO : EventDTO
        {
            public string EventAt { get; set; }
            public string EventOn { get; set; }
        }

        public class TaskModelDTO : EventDTO
        {
            public string CompleteBefore { get; set; }
            public Nullable<DateTime> Complete { get; set; }
        }

    }
}
