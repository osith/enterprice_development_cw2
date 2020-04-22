using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static CW_2.Models.ContactModal;
using static CW_2.Models.Enums;

namespace CW_2.Models
{
    public class TransactionModel : BaseEntityContextService
    {

        public TransactionModel(Guid userId)
        {
            comboContacts = new List<object>();
            this.userId = userId;
            LoadContacts(userId);
        }

        private Guid userId { get; set; }

        public class TransactionDTO
        {
            public Guid Id { get; set; }
            public double Amount { get; set; }
            public DateTime CreatedAt { get; set; }
            public Nullable<bool> IsDeleted { get; set; }
            public Nullable<bool> Recurring { get; set; }
            public Nullable<int> RecurringType { get; set; }
            public Guid UserDataId { get; set; }
            public Guid ContactDataId { get; set; }
        }

        public List<object> comboContacts { get; set; }

        public List<ContactDTO> Contacts
        {
            get => LoadContacts(userId);
            set => new List<ContactDTO>();
        }

        private List<ContactDTO> LoadContacts(Guid userId)
        {
            if (userId == Guid.Empty)
                return null;

            var contacts = DbContext.ContactDatas.Where(c => c.UserDataId == userId).ToList();
            var contactNames = contacts.Select(c => c.Name).ToList();
            comboContacts.Clear();
            foreach (var item in contactNames)
                comboContacts.Add(item);

            return Mapper.Map<List<ContactDTO>>(contacts);
        }

        public Guid FindContact(int index)
        {
            return Contacts[index].Id;
        }

        public async Task SaveTransactions(List<TransactionDTO> transactions)
        {
            var transactionEntries = Mapper.Map<List<TransactionEnity>>(transactions);
            DbContext.TransactionEnities.AddRange(transactionEntries);
            await DbContext.SaveChangesAsync();
        }

        public List<string> getRecurringTypes()
        {
            return new List<string>()
            {
                "Weekly",
                "Monthly",
                "Yearly"
            };
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


    }
}
