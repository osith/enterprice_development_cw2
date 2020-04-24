using AutoMapper;
using static CW_2.Models.ContactModal;
using static CW_2.Models.TransactionModel;
using static CW_2.Models.UserModal;

namespace CW_2.Models
{
    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            CreateMap<UserData, RegisterUserDTO>();
            CreateMap<RegisterUserDTO, UserData>();

            CreateMap<ContactDTO, ContactData>();
            CreateMap<ContactData, ContactDTO>();
            CreateMap<ContactData, ContactViewDTO>();

            CreateMap<ContactData, PayerDTO>();
            CreateMap<PayerDTO, ContactData>();

            CreateMap<ContactData, PayeeDTO>();
            CreateMap<PayeeDTO, ContactData>();

            CreateMap<TransactionEnity, TransactionDTO>();
            CreateMap<TransactionDTO, TransactionEnity>();


            //.ForMember(dest => dest.ExpectAmt, opt => opt.MapFrom(field => field.Exp));
        }
    }
}
