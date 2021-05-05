using AutoMapper;
using DeadLinerWebApp.DAL.Entity;
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
            CreateMap<UsersHubs, HubModel>().ForMember(d => d.Title, m => m.MapFrom(v => v.Hub.Name))
                .ForMember(d => d.Description, m => m.MapFrom(v => v.Hub.Description))
                .ForMember(d => d.IsMentor, m => m.MapFrom(s => s.Role.Title == "mentor"));
            CreateMap<UserInfo, UserInfoViewModel>()
                .ForMember(d => d.Email, m => m.MapFrom(v => v.User.Email));
            CreateMap<User, UserInfoViewModel>();
            CreateMap<Hub, HubModel>().ForMember(d => d.Title, m => m.MapFrom(v => v.Name));
        }
    }
}