namespace CW_2.Models
{
    public class Enums
    {
        public enum RecurringTypes : int
        {
            Yearly = 1,
            Monthly = 2,
            Weekly = 3
        }

        public struct ContactType
        {
            public const string PAYER = "Payer";
            public const string PAYEE = "Payee";
        }

        public struct EventTypeString
        {
            public const string APPOINTMENT = "Appointment";
            public const string TASK = "Task";
        }

        public enum EventType
        {
            Appointment = 1,
            Task = 2
        }

        public enum MessageType
        {
            Error,
            Warning,
            Complete
        }
    }
}
