using System;
using System.Security.Claims;
using AutoMapper;
using DeadLinerWebApp.BLL.Helper;
using DeadLinerWebApp.DAL.Entity;
using DeadLinerWebApp.DAL.Interfaces;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Moq;
using Task = System.Threading.Tasks.Task;

namespace DeadLinerWebApp.UnitTests
{
    public class MoqArrange
    {
        public Mock<IRepository<User>> UserRepositoryMock { get; }
        public Mock<IUnitOfWork> UnitOfWorkMock { get; }
        public Mock<IServiceProvider> ServiceProviderMock { get; }
        public IMapper Mapper { get; }

        public MoqArrange()
        {
            UserRepositoryMock = new Mock<IRepository<User>>();
            UnitOfWorkMock = new Mock<IUnitOfWork>();
            UnitOfWorkMock.Setup(e => e.Users).Returns(UserRepositoryMock.Object);
            var configuration = new MapperConfiguration(cfg => cfg.AddProfile(typeof(MappingProfile)));
            Mapper = new Mapper(configuration);
            
            var authenticationServiceMock = new Mock<IAuthenticationService>();
            authenticationServiceMock
                .Setup(a => a.SignInAsync(It.IsAny<HttpContext>(), It.IsAny<string>(), It.IsAny<ClaimsPrincipal>(), It.IsAny<AuthenticationProperties>()))
                .Returns(Task.CompletedTask);
            ServiceProviderMock = new Mock<IServiceProvider>();
            ServiceProviderMock
                .Setup(s => s.GetService(typeof(IAuthenticationService)))
                .Returns(authenticationServiceMock.Object);
        }
    }
}