using AutoMapper;
using static CW_2.Models.ContactModal;
using static CW_2.Models.EventModel;
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

            CreateMap<AppointmentDTO, EventEntity>();
            CreateMap<TaskDTO, EventEntity>();
            CreateMap<EventEntity, EventViewDTO>()
                .ForMember(dest => dest.RecurringType,
                opt => opt.MapFrom(field => field.RecurringType == 1 ? "Yearly"
                : field.RecurringType == 2 ? "Monthly" : field.RecurringType == 3 ? "Weekly" : ""))
                .ForMember(dest => dest.Type, opt => opt.MapFrom(field => field.Type == 1 ? "Appointment" : "Task"))
                .ForMember(dest => dest.CompleteBefore, opt => opt.MapFrom(field => field.Complete));
        }
    }
}
