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
    }
}
