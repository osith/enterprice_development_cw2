using System;

namespace CW_2.Models
{
    public class Event
    {
        public class EventModel
        {
            public Guid Id { get; set; }
            public string Description { get; set; }
            public short Type { get; set; }
            public short EventCat { get; set; }
            public string EventAt { get; set; }
            public Nullable<bool> Recurring { get; set; }
            public Nullable<int> RecurringType { get; set; }
            public Guid UserDataId { get; set; }
        }

        public class AppointmentModel : EventModel
        {
            public string EventOn { get; set; }
        }

        public class TaskModel : EventModel
        {
            public string CompleteBefore { get; set; }
        }

    }
}
