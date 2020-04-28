using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static CW_2.Models.Enums;

namespace CW_2.Models
{
    public class EventModel : BaseEntityContextService
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
            public Nullable<DateTime> EventOn { get; set; }
        }

        public class TaskDTO : EventDTO
        {
            public Nullable<DateTime> Complete { get; set; }
        }

        public class EventViewDTO
        {
            public Guid Id { get; set; }
            public string Description { get; set; }
            public string Type { get; set; }
            public Nullable<bool> Recurring { get; set; }
            public string RecurringType { get; set; }
            public Guid UserDataId { get; set; }
            public Nullable<DateTime> CreatedAt { get; set; }
            public Nullable<bool> IsDeleted { get; set; }
            public string EventAt { get; set; }
            public Nullable<DateTime> EventOn { get; set; }
            public Nullable<DateTime> CompleteBefore { get; set; }
        }

        public int? getRecurringTypeEnum(object selectedType)
        {
            var type = selectedType.ToString();
            if (!string.IsNullOrEmpty(type))
            {
                if (selectedType.Equals("Weekly"))
                    return (int)RecurringTypes.Weekly;
                else if (selectedType.Equals("Monthly"))
                    return (int)RecurringTypes.Monthly;
                else if (selectedType.Equals("Yearly"))
                    return (int)RecurringTypes.Yearly;
            }
            return null;
        }

        public async Task SaveEvent(AppointmentDTO appointment)
        {
            var eventAppointment = Mapper.Map<EventEntity>(appointment);
            DbContext.EventEntities.Add(eventAppointment);
            await DbContext.SaveChangesAsync();
        }

        public async Task SaveEvent(TaskDTO task)
        {
            var eventTask = Mapper.Map<EventEntity>(task);
            DbContext.EventEntities.Add(eventTask);
            await DbContext.SaveChangesAsync();
        }

        public List<EventViewDTO> LoadAllEvents(Guid userId)
        {
            var events = DbContext.EventEntities.Where(e => e.UserDataId == userId && e.IsDeleted == null).ToList();
            return Mapper.Map<List<EventViewDTO>>(events);
        }

        public async Task UpdateEvent(AppointmentDTO appointment)
        {
            var updateAppointment = await DbContext.EventEntities.FindAsync(appointment.Id);
            Mapper.Map(appointment, updateAppointment);
            await DbContext.SaveChangesAsync();
        }

        public async Task UpdateEvent(TaskDTO task)
        {
            var updateTask = await DbContext.EventEntities.FindAsync(task.Id);
            Mapper.Map(task, updateTask);
            await DbContext.SaveChangesAsync();
        }

    }
}
