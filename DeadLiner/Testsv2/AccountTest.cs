using System;
using AutoMapper;
using DeadLinerWebApp.BLL.Helper;
using DeadLinerWebApp.BLL.Interfaces;
using DeadLinerWebApp.BLL.Services;
using DeadLinerWebApp.DAL.Entity;
using DeadLinerWebApp.PL.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Testsv2;

namespace DeadLinerWebApp.Tests
{
    [TestClass]
    public class AccountTest
    {
        private static IAuthorizationService _service;

        [ClassInitialize]
        public static void TestFixtureSetup(TestContext context)
        {
            MoqArrange.Arrange();
            var configuration = new MapperConfiguration(cfg => cfg.AddProfile(typeof(MappingProfile)));
            IMapper mapper = new Mapper(configuration);
            _service = new AuthorizationService(mapper, MoqArrange.UnitOfWorkMock.Object);
        }

        [TestMethod]
        public async void LogIn_NonExistentUser_ReturnNull()
        {
            // Arrange 
            var user = new LoginViewModel
            {
                Email = "testEmail",
                Password = "testPassword"
            };
            MoqArrange.UserRepositoryMock.Setup(e => e.Find(It.IsAny<Func<User, bool>>())).Returns(() => null);

            // Act
            var result = await _service.Login(user, new DefaultHttpContext());

            // Assert
            Assert.IsNull(result);
        }
    }
}
