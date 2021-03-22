using System.Threading.Tasks;
using DeadLinerWebApp.PL.Models;
using Microsoft.AspNetCore.Http;

namespace DeadLinerWebApp.BLL.Interfaces
{
    public interface IAuthorization
    {
        Task<CurrentUser> Login(LoginModel model, HttpContext context);
        void Logout(HttpContext context);
    }
}