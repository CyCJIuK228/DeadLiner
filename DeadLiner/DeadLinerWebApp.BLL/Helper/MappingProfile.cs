using AutoMapper;
using DeadLinerWebApp.DAL.Models;
using DeadLinerWebApp.PL.Models;

namespace DeadLinerWebApp.BLL.Helper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<User, CurrentUserViewModel>();
            CreateMap<RegisterViewModel, User>()
                .ForMember(d => d.FullName, m => m.MapFrom(v => v.FirstName + " " + v.LastName));
            CreateMap<Hub, HubModel>().ForMember(d => d.Title, m => m.MapFrom(v => v.Name));
        }
    }
}