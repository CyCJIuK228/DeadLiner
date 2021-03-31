using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using DeadLinerWebApp.BLL.Interfaces;
using DeadLinerWebApp.DAL.Interfaces;
using DeadLinerWebApp.DAL.Models;
using DeadLinerWebApp.PL.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using Task = System.Threading.Tasks.Task;

namespace DeadLinerWebApp.BLL.Services
{
    public class AuthorizationService : IAuthorizationService
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public AuthorizationService(IMapper mapper, IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        private async Task Authenticate(HttpContext context, string email, bool isRemember)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimsIdentity.DefaultNameClaimType, email)
            };

            var id = new ClaimsIdentity(claims, "ApplicationCookie", ClaimsIdentity.DefaultNameClaimType,
                ClaimsIdentity.DefaultRoleClaimType);

            await context.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(id),
                new AuthenticationProperties
                {
                    IsPersistent = isRemember,
                    ExpiresUtc = DateTime.Now.AddHours(1)
                });
        }

        public async Task<CurrentUserViewModel> Login(LoginViewModel model, HttpContext context)
        {
            var userModel = _unitOfWork.Users
                .Find(user => user.Email.ToLowerInvariant().Equals(model.Email.ToLowerInvariant()) && user.Password.Equals(model.Password))
                .FirstOrDefault();

            if (userModel == null)
                return null;

            await Authenticate(context, model.Email, model.IsRemember);
            return _mapper.Map<CurrentUserViewModel>(userModel);

        }

        public async void Logout(HttpContext context)
        {
            await context.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
        }

        public CurrentUserViewModel Register(RegisterViewModel model)
        {
            var userModel = _unitOfWork.Users
                .Find(u => u.Email.Equals(model.Email) && u.Password.Equals(model.Password)).FirstOrDefault();

            if (userModel != null)
                return null;

            var user = _mapper.Map<User>(model);
            _unitOfWork.Users.Create(user);
            _unitOfWork.Save();
            return _mapper.Map<CurrentUserViewModel>(user);
        }
    }
}