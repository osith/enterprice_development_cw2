using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static CW_2.Models.UserModal;

namespace CW_2.Models
{
    public class ContactModal : BaseEntityContextService
    {
        public class ContactDTO
        {
            public Guid Id { get; set; }
            public string Name { get; set; }
            public string Type { get; set; }
            public string Contact { get; set; }
            public string Email { get; set; }
            public Nullable<DateTime> CreatedAt { get; set; }
            public Nullable<bool> IsDeleted { get; set; }
            public Guid UserDataId { get; set; }
        }

        public class ContactViewDTO
        {
            public Guid Id { get; set; }
            public string Name { get; set; }
            public string Type { get; set; }
            public string Contact { get; set; }
            public string Email { get; set; }
            public string AccountNo { get; set; }
            public Nullable<double> ExpectAmt { get; set; }
            public string BankName { get; set; }
            public Guid UserDataId { get; set; }
        }

        public class PayerDTO : ContactDTO
        {
            public double ExpectAmt { get; set; }
        }

        public class PayeeDTO : ContactDTO
        {
            public string AccountNo { get; set; }
            public string BankName { get; set; }
        }

        public async Task SaveUser(PayeeDTO model, RegisterUserDTO loggedUser)
        {
            var contact = Mapper.Map<ContactData>(model);
            var currentUser = Mapper.Map<UserData>(loggedUser);
            contact.UserDataId = currentUser.Id;
            contact.CreatedAt = DateTime.UtcNow;
            DbContext.ContactDatas.Add(contact);
            await DbContext.SaveChangesAsync();
        }

        public async Task SaveUser(PayerDTO model, RegisterUserDTO loggedUser)
        {
            var contact = Mapper.Map<ContactData>(model);
            var currentUser = Mapper.Map<UserData>(loggedUser);
            contact.UserDataId = currentUser.Id;
            contact.CreatedAt = DateTime.UtcNow;
            DbContext.ContactDatas.Add(contact);
            await DbContext.SaveChangesAsync();
        }

        public async Task UpdateContact(PayerDTO updatemodel, RegisterUserDTO loggedUser)
        {
            var updateContact = await DbContext.ContactDatas.FindAsync(updatemodel.Id);
            updatemodel.UserDataId = loggedUser.Id;
            Mapper.Map(updatemodel, updateContact);
            await DbContext.SaveChangesAsync();
        }

        public async Task UpdateContact(PayeeDTO updatemodel, RegisterUserDTO loggedUser)
        {
            var updateContact = await DbContext.ContactDatas.FindAsync(updatemodel.Id);
            updatemodel.UserDataId = loggedUser.Id;
            Mapper.Map(updatemodel, updateContact);
            await DbContext.SaveChangesAsync();
        }

        public List<ContactViewDTO> loadAllContacts()
        {
            var contacts = DbContext.ContactDatas.Where(c => c.IsDeleted == null).ToList();
            return Mapper.Map<List<ContactViewDTO>>(contacts);
        }

        public ContactViewDTO LoadContactInfo(Guid contactId)
        {
            var contact = DbContext.ContactDatas.Where(c => c.Id == contactId).FirstOrDefault();
            return Mapper.Map<ContactViewDTO>(contact);
        }

        public async Task<bool> DeleteContact(Guid contactId)
        {
            var contact = await DbContext.ContactDatas.FindAsync(contactId);
            if (contact != null)
            {
                contact.IsDeleted = true;
                await DbContext.SaveChangesAsync();
                return true;
            }
            return false;
        }
    }
}
