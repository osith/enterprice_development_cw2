using AutoMapper;
using System;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;

namespace CW_2.Models
{
    public class UserModal : BaseEntityContextService
    {
        public class RegisterUserDTO
        {
            public Guid Id { get; set; }
            public string Name { get; set; }
            public string Email { get; set; }
            public string Phone { get; set; }
            public string Password { get; set; }
            public Nullable<bool> IsDeleted { get; set; }
            public Nullable<DateTime> CreatedAt { get; set; }
        }

        public class LoginUserDTO
        {
            public string Email { get; set; }
            public string Password { get; set; }
        }


        public async Task SaveUser(RegisterUserDTO model)
        {
            var user = Mapper.Map<UserData>(model);
            DbContext.UserDatas.Add(user);
            await DbContext.SaveChangesAsync();
        }

        public async Task<RegisterUserDTO> ValidateLogin(LoginUserDTO model)
        {
            var user = await DbContext.UserDatas.Where(u => u.Email == model.Email && u.Password == model.Password)
                .FirstOrDefaultAsync();
            if (user != null)
                return Mapper.Map<RegisterUserDTO>(user);
            return null;
        }




    }
}
