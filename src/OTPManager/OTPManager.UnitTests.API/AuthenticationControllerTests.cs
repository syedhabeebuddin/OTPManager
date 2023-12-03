using FluentAssertions;
using FluentAssertions.Execution;
using Microsoft.AspNetCore.Mvc;
using Moq;
using OTPManager.API.Controllers;
using OTPManager.Application.Services;

namespace OTPManager.UnitTests.API
{
    public class AuthenticationControllerTests
    {
        private readonly Mock<IOTPAuthenticator> _otpAuthenticatorMock;
        public AuthenticationControllerTests()
        {
            _otpAuthenticatorMock = new Mock<IOTPAuthenticator>();
        }

        [Fact]
        public void GetCode_Should_Return_Code()
        {
            //Arrange
            var code = "112233";
            _otpAuthenticatorMock.Setup(x => x.IsLimitExceeded(It.IsAny<string>())).Returns(false);
            _otpAuthenticatorMock.Setup(x => x.GenerateCode(It.IsAny<string>())).Returns(code);

            //Act
            var authenticationController = new AuthenticationController(_otpAuthenticatorMock.Object);
            var actualResult = authenticationController.Get2FACode(code) as OkObjectResult;

            //Assert
            using (new AssertionScope())
            {
                actualResult.Should().NotBeNull();
                actualResult.Value.Should().BeOfType<string>();
                actualResult.Value.Should().Be(code);
            }
        }
    }
}
