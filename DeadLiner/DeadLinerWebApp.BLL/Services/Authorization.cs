using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using DeadLinerWebApp.BLL.Interfaces;
using DeadLinerWebApp.DAL.Interfaces;
using DeadLinerWebApp.PL.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;

namespace DeadLinerWebApp.BLL.Services
{
    public class Authorization : IAuthorization
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public Authorization(IMapper mapper, IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        private async Task Authenticate(HttpContext context, string email)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimsIdentity.DefaultNameClaimType, email)
            };

            var id = new ClaimsIdentity(claims, "ApplicationCookie", ClaimsIdentity.DefaultNameClaimType,
                ClaimsIdentity.DefaultRoleClaimType);

            await context.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(id));
        }

        public async Task<CurrentUser> Login(LoginModel model, HttpContext context)
        {
            var modelDto = _mapper.Map<LoginModel>(model);
            var userModel = _unitOfWork.Users
                .Find(user => user.Email.Equals(modelDto.Email) && user.Password.Equals(modelDto.Password))
                .FirstOrDefault();
            
            if (userModel != null)
            {
                await Authenticate(context, modelDto.Email);
                return _mapper.Map<CurrentUser>(userModel);
            }

            return null;
        }

        public async void Logout(HttpContext context)
        {
            await context.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
        }
    }
}