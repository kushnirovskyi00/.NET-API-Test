using BlogPost.WebApi;
using BlogPost.WebApi.Controller;

using BlogPost.WebApi.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Assert = Microsoft.VisualStudio.TestTools.UnitTesting.Assert;

namespace BlogPost.WebApi.Controllers;

public class UserControllerUnitTest
{
    [TestClass]
    public class UserControllerTests
    {
        private UserController _controller;
        private Mock<IMockAuthenticationService> _authenticationServiceMock;

        [TestInitialize]
        public void Setup()
        {
            _authenticationServiceMock = new Mock<IMockAuthenticationService>();
            _controller = new UserController(_authenticationServiceMock.Object);
        }

        [TestMethod]
        public void Login_ValidCredentials_ReturnsOkResultWithToken(Assert assert)
        {
            // Arrange
            var validLoginRequest = new LoginRequest { Username = "validUsername", Password = "validPassword" };
            var expectedToken = "validToken";

            _authenticationServiceMock
                .Setup(x => x.AuthenticateUser(validLoginRequest.Username, validLoginRequest.Password))
                .Returns(true);

            _authenticationServiceMock
                .Setup(x => x.GenerateToken(validLoginRequest.Username))
                .Returns(expectedToken);

            var result = _controller.Login(validLoginRequest);

            // Assert
            Assert.IsInstanceOfType(result, typeof(OkObjectResult));

            // If the result is an OkObjectResult, proceed with additional assertions
            if (result is OkObjectResult okResult)
            {
                var responseData = (dynamic)okResult.Value;
                Assert.AreEqual(expectedToken, responseData.Token);
            }
        }

        [TestMethod]
        public void Login_InvalidCredentials_ReturnsUnauthorizedResult()
        {
            // Arrange
            var invalidLoginRequest = new LoginRequest { Username = "invalidUsername", Password = "invalidPassword" };

            _authenticationServiceMock
                .Setup(x => x.AuthenticateUser(invalidLoginRequest.Username, invalidLoginRequest.Password))
                .Returns(false);

            // Act
            var result = _controller.Login(invalidLoginRequest);

            // Assert
            Assert.IsInstanceOfType(result, typeof(UnauthorizedObjectResult));
        }
    }
}