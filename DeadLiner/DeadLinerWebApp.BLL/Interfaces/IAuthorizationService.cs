using System.Threading.Tasks;
using DeadLinerWebApp.PL.Models;
using Microsoft.AspNetCore.Http;

namespace DeadLinerWebApp.BLL.Interfaces
{
    public interface IAuthorizationService
    {
        Task<CurrentUserViewModel> Login(LoginViewModel model, HttpContext context);
        CurrentUserViewModel Register(RegisterViewModel model);
        void ForgotPassword(ForgotPasswordViewModel model);
        void ReplacePassword(RecoveryPasswordViewModel model);
        string GetRecoveryCode(string email);
        void Logout(HttpContext context);
    }
}