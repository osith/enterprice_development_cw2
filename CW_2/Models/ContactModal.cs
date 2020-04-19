using AutoMapper;
using System;
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
            public virtual UserData UserData { get; set; }
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
            DbContext.ContactDatas.Add(contact);
            await DbContext.SaveChangesAsync();
        }

        public async Task SaveUser(PayerDTO model, RegisterUserDTO loggedUser)
        {
            var contact = Mapper.Map<ContactData>(model);
            var currentUser = Mapper.Map<UserData>(loggedUser);
            contact.UserDataId = currentUser.Id;
            DbContext.ContactDatas.Add(contact);
            await DbContext.SaveChangesAsync();
        }

        //public async Task<ContactDTO> ValidateLogin(LoginUserDTO model)
        //{
        //    var user = await DbContext.UserDatas.Where(u => u.Phone == model.Phone && u.Password == model.Password)
        //        .FirstOrDefaultAsync();
        //    if (user != null)
        //        return Mapper.Map<RegisterUserDTO>(user);
        //    return null;
        //}
    }
}
