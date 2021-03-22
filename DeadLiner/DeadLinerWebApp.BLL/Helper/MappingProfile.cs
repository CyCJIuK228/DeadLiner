using AutoMapper;
using DeadLinerWebApp.DAL.Models;
using DeadLinerWebApp.PL.Models;

namespace DeadLinerWebApp.BLL.Helper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<User, CurrentUser>();
        }
    }
}