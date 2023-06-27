using AFS.Controllers;
using AFS.Dtos;
using AFS.Repositories.Abstract;
using AFS.Repositories.Concrete;
using FakeItEasy;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using MovieStoreMvc.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace AFS.Tests.Controllers
{
    public class UserAuthenticationControllerTests
    {
        private IUserAuthenticationService _authService;
        private UserAuthenticationController _userAuthenticationContoller;
        public UserAuthenticationControllerTests()
        {
            //Dependencies
            _authService = A.Fake<IUserAuthenticationService>();

            //SUT
            _userAuthenticationContoller = new UserAuthenticationController(_authService);
        }

        [Fact]
        public void UserAuthenticationController_Register_ShouldReturnsViewResult()
        {
            //Arrange
            var registermodel = A.Fake<RegistrationModel>();
            var status =  A.Fake<Status>();


            A.CallTo(() => _authService.RegisterAsync(registermodel)).Returns(status);

            //Act

            var result = _userAuthenticationContoller.Register();

            //Assert

            result.Should().BeOfType<Task<IActionResult>>();

        }

    }
}
