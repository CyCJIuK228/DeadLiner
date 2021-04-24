using DeadLinerWebApp.PL.Models;

namespace DeadLinerWebApp.BLL.Interfaces
{
    public interface IProfileService
    {
        UserInfoViewModel GetUserInfo(string userName);
        void UpdateUserInfo(UserInfoViewModel model, string userName);
    }
}