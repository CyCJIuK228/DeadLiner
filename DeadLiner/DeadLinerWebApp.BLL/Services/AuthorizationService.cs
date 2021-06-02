using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using DeadLinerWebApp.BLL.Interfaces;
using DeadLinerWebApp.DAL.Entity;
using DeadLinerWebApp.DAL.Interfaces;
using DeadLinerWebApp.PL.Models;
using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using MimeKit;
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

        private async Task Authenticate(HttpContext context, string fullName)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimsIdentity.DefaultNameClaimType, fullName)
            };

            var id = new ClaimsIdentity(claims, "ApplicationCookie", ClaimsIdentity.DefaultNameClaimType,
                ClaimsIdentity.DefaultRoleClaimType);

            await context.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(id),
                new AuthenticationProperties
                {
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

            await Authenticate(context, userModel.FullName);
            return _mapper.Map<CurrentUserViewModel>(userModel);

        }

        public void ForgotPassword(ForgotPasswordViewModel model)
        {
            var user = _unitOfWork.Users.Find(u => u.Email.ToLowerInvariant().Equals(model.Email.ToLowerInvariant()))
                .FirstOrDefault();

            if (user == null)
                //TODO
                throw new Exception();

            var message = new MimeMessage();
            var from = new MailboxAddress("Deadliner",
                "support_team@deadliner.com");

            var to = new MailboxAddress(model.Email, user.Email);

            message.To.Add(to);
            message.From.Add(@from);
            message.Subject = "Recovery password deadliner app.";
            var generator = new Random();
            var random = generator.Next(0, 1000000).ToString("D6");
            var bodyBuilder = new BodyBuilder { HtmlBody = $"<h1>Please, enter this recovery code in the message box: {random}</h1>", TextBody = "Recovery code" };
            message.Body = bodyBuilder.ToMessageBody();
            var client = new SmtpClient();
            client.Connect("smtp.gmail.com", 587, SecureSocketOptions.StartTls);
            client.Authenticate("ivanbogov678@gmail.com", "123bigay456");
            client.Send(message);
            client.Disconnect(true);
            client.Dispose();

            _unitOfWork.Codes.Create(new RecoveryCode { Code = random, UserId = user.UserId});
            _unitOfWork.Save();
        }

        public void ReplacePassword(RecoveryPasswordViewModel model)
        {
            var user = _unitOfWork.Users.Find(u => u.Email.ToLowerInvariant().Equals(model.Email)).FirstOrDefault();

            if (user != null)
            {
                user.Password = model.NewPassword;
                _unitOfWork.Users.Update(user);
            }

            _unitOfWork.Save();
        }

        public string GetRecoveryCode(string email)
        {
            return _unitOfWork.Codes.GetWithInclude(u => u.User, p => p.User).Last().Code;
        }

        public async void Logout(HttpContext context)
        {
            await context.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
        }

        public CurrentUserViewModel Register(RegisterViewModel model)
        {
            var userModel = _unitOfWork.Users
                .Find(u => u.FullName == model.FirstName + ' ' + model.LastName).FirstOrDefault();

            if (userModel != null)
                return null;

            var user = _mapper.Map<User>(model);
            _unitOfWork.Users.Create(user);
            _unitOfWork.Save();
            return _mapper.Map<CurrentUserViewModel>(user);
        }
    }
}