using AutoMapper;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;
using static CW_2.Models.ContactModal;
using static CW_2.Models.Enums;
using static CW_2.Models.ReportModel;

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

        public class TransactionViewDTO
        {
            public Guid Id { get; set; }
            public double Amount { get; set; }
            public DateTime CreatedAt { get; set; }
            public Nullable<bool> Recurring { get; set; }
            public string RecurringType { get; set; }
            public string ContactName { get; set; }
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

        public List<TransactionViewDTO> LoadAllTransactions(Guid userId)
        {
            var transactions = (from t in DbContext.TransactionEnities
                                join c in DbContext.ContactDatas
                                on t.ContactDataId equals c.Id
                                where t.IsDeleted == null && c.IsDeleted == null && t.UserDataId == userId
                                select new TransactionViewDTO()
                                {
                                    Id = t.Id,
                                    Amount = t.Amount,
                                    ContactName = c.Name,
                                    Recurring = t.Recurring,
                                    RecurringType = t.RecurringType.ToString(),
                                    CreatedAt = t.CreatedAt
                                }).ToList();

            foreach (var transaction in transactions)
                transaction.RecurringType = getRecurringType(transaction.RecurringType);

            return transactions;
        }

        private string getRecurringType(string selectedType)
        {
            if (selectedType == "3")
                return RecurringTypes.Weekly.ToString();
            else if (selectedType == "2")
                return RecurringTypes.Monthly.ToString();
            else if (selectedType == "1")
                return RecurringTypes.Yearly.ToString();
            return string.Empty;
        }

        public async Task UpdateTransaction(TransactionDTO transaction)
        {
            var updateTransaction = await DbContext.TransactionEnities.FindAsync(transaction.Id);
            Mapper.Map(transaction, updateTransaction);
            await DbContext.SaveChangesAsync();
        }

        public TransactionViewDTO LoadTransactionInfo(Guid transactionId)
        {
            var transaction = (from t in DbContext.TransactionEnities
                               join c in DbContext.ContactDatas
                               on t.ContactDataId equals c.Id
                               where t.Id == transactionId
                               select new TransactionViewDTO()
                               {
                                   Id = t.Id,
                                   Amount = t.Amount,
                                   ContactName = c.Name,
                                   Recurring = t.Recurring,
                                   RecurringType = t.RecurringType.ToString(),
                                   CreatedAt = t.CreatedAt
                               }).FirstOrDefault();
            transaction.RecurringType = getRecurringType(transaction.RecurringType);
            return transaction;
        }


        public Task<bool> WriteTransactionToXML(List<TransactionDTO> transactions) {
            return Task.Run(()=>WritetoFile(transactions));
        }


        private bool WritetoFile(List<TransactionDTO> transactions)
        {
            foreach (var transaction in transactions) {
                try
                {
                    String fileLocation = String.Format("{0}\\{1}",
                        Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "Transaction.xml");
                    if (!File.Exists(fileLocation))
                    {
                        XmlWriterSettings xmlWriterSettings = new XmlWriterSettings();
                        xmlWriterSettings.Indent = true;
                        xmlWriterSettings.NewLineOnAttributes = true;
                        using (XmlWriter xmlWriter = XmlWriter.Create(fileLocation, xmlWriterSettings))
                        {
                            xmlWriter.WriteStartDocument();
                            xmlWriter.WriteComment("Transaction XML File");
                            xmlWriter.WriteStartElement("TransactionData");
                            xmlWriter.WriteStartElement("Transaction");
                            xmlWriter.WriteElementString("Id", transaction.Id.ToString());
                            xmlWriter.WriteElementString("Amount", transaction.Amount.ToString());
                            xmlWriter.WriteElementString("ContactDataId", transaction.ContactDataId.ToString());
                            xmlWriter.WriteElementString("CreatedAt", transaction.CreatedAt.ToString());
                            xmlWriter.WriteElementString("Recurring", transaction.Recurring.ToString());
                            xmlWriter.WriteElementString("RecurringType", transaction.RecurringType.ToString());
                            xmlWriter.WriteElementString("UserDataId", transaction.UserDataId.ToString());
                            xmlWriter.WriteEndElement();
                            xmlWriter.WriteEndElement();
                            xmlWriter.WriteEndDocument();
                            xmlWriter.Flush();
                            xmlWriter.Close();
                        }
                    }
                    else
                    {
                        XDocument xDocument = XDocument.Load(fileLocation);
                        XElement root = xDocument.Element("TransactionData");
                        IEnumerable<XElement> rows = root.Descendants("Transaction");
                        XElement firstRow = rows.First();
                        firstRow.AddBeforeSelf(
                           new XElement("Transaction",
                           new XElement("Id", transaction.Id.ToString()),
                           new XElement("Amount", transaction.Amount.ToString()),
                           new XElement("ContactDataId", transaction.ContactDataId.ToString()),
                           new XElement("CreatedAt", transaction.CreatedAt.ToString()),
                           new XElement("Recurring", transaction.Recurring.ToString()),
                           new XElement("RecurringType", transaction.RecurringType.ToString()),
                           new XElement("UserDataId", transaction.UserDataId.ToString())
                           ));
                        xDocument.Save(fileLocation);
                    }
                }
                catch (Exception e)
                {
                    return false;
                }
            }
            return true;
        }

    }
}
