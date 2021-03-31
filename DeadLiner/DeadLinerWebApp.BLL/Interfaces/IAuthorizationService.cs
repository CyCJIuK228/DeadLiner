using System.Threading.Tasks;
using DeadLinerWebApp.PL.Models;
using Microsoft.AspNetCore.Http;

namespace DeadLinerWebApp.BLL.Interfaces
{
    public interface IAuthorizationService
    {
        Task<CurrentUserViewModel> Login(LoginViewModel model, HttpContext context);
        void Logout(HttpContext context);
        CurrentUserViewModel Register(RegisterViewModel model);
    }
}