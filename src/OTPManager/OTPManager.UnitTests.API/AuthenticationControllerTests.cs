using FluentAssertions;
using FluentAssertions.Execution;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using OTPManager.API.Controllers;
using OTPManager.API.Models;
using OTPManager.Application.Services;

namespace OTPManager.UnitTests.API
{
    public class AuthenticationControllerTests : IDisposable
    {
        private Mock<IOTPAuthenticator> _otpAuthenticatorMock;
        private Mock<ILogger<AuthenticationController>> _loggerMock;
        public AuthenticationControllerTests()
        {
            _otpAuthenticatorMock = new Mock<IOTPAuthenticator>();
            _loggerMock = new Mock<ILogger<AuthenticationController>>();
        }

        //Note : To Cleanup / Dispose , after each test case.
        public void Dispose()
        {
            _otpAuthenticatorMock = null;
            _loggerMock = null;
        }

        [Fact]
        public void GetCode_Should_Return_Code()
        {
            //Arrange
            var code = "112233";
            _otpAuthenticatorMock.Setup(x => x.IsLimitExceeded(It.IsAny<string>())).Returns(false);
            _otpAuthenticatorMock.Setup(x => x.GenerateCode(It.IsAny<string>())).Returns(code);

            //Act
            var authenticationController = new AuthenticationController(_otpAuthenticatorMock.Object,_loggerMock.Object);
            var actualResult = authenticationController.Get2FACode(code) as OkObjectResult;

            //Assert
            using (new AssertionScope())
            {
                actualResult.Should().NotBeNull();
                actualResult.Value.Should().BeOfType<string>();
                actualResult.Value.Should().Be(code);
            }
        }

        [Fact]
        public void Verify2FACode_Should_Return_True()
        {
            //Arrange
            var code = "112233";
            var phone = "1111122222";
            _otpAuthenticatorMock.Setup(x => x.IsLimitExceeded(It.IsAny<string>())).Returns(false);
            _otpAuthenticatorMock.Setup(x => x.GenerateCode(It.IsAny<string>())).Returns(code);
            _otpAuthenticatorMock.Setup(x => x.VerifyCode(It.IsAny<string>(), It.IsAny<string>())).Returns(true);
            var request = new VerifyCodeRequest { Phone = phone, Code = code };

            //Act
            var authenticationController = new AuthenticationController(_otpAuthenticatorMock.Object, _loggerMock.Object);            
            var actualResult = authenticationController.Verify2FACode(request) as OkObjectResult;

            //Assert
            using (new AssertionScope())
            {
                actualResult.Should().NotBeNull();
                actualResult.Value.Should().BeOfType<bool>();
                actualResult.Value.Should().Be(true);
            }
        }
    }
}
