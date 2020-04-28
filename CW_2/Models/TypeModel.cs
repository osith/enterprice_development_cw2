using System;

namespace CW_2.Models
{
    public class TypeModel
    {
        public Nullable<bool> Recurring { get; set; }
        public class OneTimeModel : TypeModel
        {

        }

        public class RecurringModel : TypeModel
        {
            public Nullable<int> RecurringType { get; set; }
        }
    }
}
