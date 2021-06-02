using System;
using System.Collections.Generic;
using DeadLinerWebApp.BLL.Interfaces;
using DeadLinerWebApp.BLL.Services;
using DeadLinerWebApp.DAL.Entity;
using DeadLinerWebApp.PL.Models;
using Microsoft.AspNetCore.Http;
using Moq;
using Xunit;

namespace DeadLinerWebApp.UnitTests
{
    public class AccountTest : IClassFixture<MoqArrange>
    {
        private readonly IAuthorizationService _service;
        private readonly MoqArrange _moqArrange;

        public AccountTest(MoqArrange moqArrange)
        {
            _moqArrange = moqArrange;
            _service = new AuthorizationService(moqArrange.Mapper, moqArrange.UnitOfWorkMock.Object);
        }

        [Fact]
        public async void LogIn_NonExistentUser_ReturnNull()
        {
            // Arrange 
            var user = new LoginViewModel
            {
                Email = "testEmail",
                Password = "testPassword"
            };
            _moqArrange.UserRepositoryMock.Setup(e => e.Find(It.IsAny<Func<User, bool>>())).Returns(() => new List<User>());

            // Act
            var result = await _service.Login(user, new DefaultHttpContext());

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public async void LogIn_ExistentUser_ReturnUser()
        {
            // Arrange 
            var requestModel = new LoginViewModel
            {
                Email = "testEmail",
                Password = "testPassword"
            };

            var responseModel = new CurrentUserViewModel
            {
                Email = "testEmail",
                FullName = "TestFullName",
                Password = "testPassword"
            };

            _moqArrange.UserRepositoryMock.Setup(e => e.Find(It.IsAny<Func<User, bool>>())).Returns(() =>
                new List<User> {new User {Email = requestModel.Email, Password = requestModel.Password, FullName = "TestFullName"}});

            // Act
            var result = await _service.Login(requestModel,
                new DefaultHttpContext {RequestServices = _moqArrange.ServiceProviderMock.Object});

            // Assert
            Assert.NotNull(result);
            Assert.Equal(responseModel.Email, result.Email);
            Assert.Equal(responseModel.Password, result.Password);
            Assert.Equal(responseModel.FullName, result.FullName);
        }
    }
}
