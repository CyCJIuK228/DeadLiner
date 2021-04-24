using System;
using System.Linq;
using AutoMapper;
using DeadLinerWebApp.BLL.Interfaces;
using DeadLinerWebApp.DAL.Entity;
using DeadLinerWebApp.DAL.Interfaces;
using DeadLinerWebApp.PL.Models;

namespace DeadLinerWebApp.BLL.Services
{
    public class ProfileService: IProfileService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public ProfileService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public UserInfoViewModel GetUserInfo(string userName)
        {
            var user = _unitOfWork.Users.GetWithInclude(p => p.FullName == userName, i => i.UserInfo).FirstOrDefault();
            var response = _mapper.Map<UserInfoViewModel>(user.UserInfo);
            return response ?? _mapper.Map<UserInfoViewModel>(user);
        }

        public void UpdateUserInfo(UserInfoViewModel model, string userName)
        {
            var userInfo = _unitOfWork.UserInfos.GetWithInclude(p => p.User.FullName.Equals(userName), i => i.User).FirstOrDefault();
            var user = _unitOfWork.Users.Find(u => u.FullName.Equals(userName)).First();
            if (userInfo == null)
            {
                userInfo = new UserInfo
                {
                    UserId = user.UserId
                };
                _unitOfWork.UserInfos.Create(userInfo);
                _unitOfWork.Save();
            }

            userInfo = _unitOfWork.UserInfos.GetWithInclude(p => p.User.FullName.Equals(userName), i => i.User).FirstOrDefault();
            if (!string.IsNullOrEmpty(model.CurrentPassword))
            {
                if (user.Password != model.CurrentPassword)
                    throw new Exception("The password is incorrect.");
                if (model.NewPassword != model.ConfirmPassword)
                    throw new Exception("Passwords are not same.");

                userInfo.User.Password = model.NewPassword;
            }

            if (!string.IsNullOrEmpty(model.Address))
                userInfo.Address = model.Address;
            if (!string.IsNullOrEmpty(model.Bio))
                userInfo.Bio = model.Bio;
            if (!string.IsNullOrEmpty(model.Country))
                userInfo.Country = model.Country;
            if (!string.IsNullOrEmpty(model.Email))
                userInfo.User.Email = model.Email;
            if (!string.IsNullOrEmpty(model.Group))
                userInfo.Group = model.Group;
            if (!string.IsNullOrEmpty(model.Phone))
                userInfo.Phone = model.Phone;
            if (!string.IsNullOrEmpty(model.University))
                userInfo.University = model.University;
            if (model.BirthDay != userInfo.BirthDay)
                userInfo.BirthDay = model.BirthDay;

            _unitOfWork.UserInfos.Update(userInfo);
            _unitOfWork.Users.Update(userInfo.User);
            _unitOfWork.Save();
        }
    }
}
