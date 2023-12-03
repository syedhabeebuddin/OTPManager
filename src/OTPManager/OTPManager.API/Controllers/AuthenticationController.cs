using Microsoft.AspNetCore.Mvc;
using OTPManager.API.Models;
using OTPManager.API.Validators;
using OTPManager.Application.Services;

namespace OTPManager.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly IOTPAuthenticator _otpAuthenticator;
        private readonly ILogger<AuthenticationController> _logger;
        public AuthenticationController(
            IOTPAuthenticator otpAuthenticator
            , ILogger<AuthenticationController> logger)
        {
            _otpAuthenticator = otpAuthenticator;
            _logger = logger;
        }

        [HttpGet("{phone}")]
        [ProducesResponseType(typeof(string), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult Get2FACode([CustomPhone] string phone)
        {
            _logger.LogInformation("Generate Code");

            if (_otpAuthenticator.IsLimitExceeded(phone))
            {
                _logger.LogDebug("The phone {phone}, has reached the limit of max number of codes", phone);
                return Problem("Reached Max Limit");
            }

            var code = _otpAuthenticator.GenerateCode(phone);
            _logger.LogInformation("Generated Code : {code}", code);

            return Ok(code);
        }
        
        [HttpPost]
        [ProducesResponseType(typeof(bool), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult Verify2FACode(VerifyCodeRequest verifyCodeRequest)
        {
            _logger.LogInformation("Verify Code");

            var isValid = _otpAuthenticator.VerifyCode(verifyCodeRequest.Phone, verifyCodeRequest.Code);
            _logger.LogInformation("Is Code Valid : {isValid}", isValid);

            return Ok(isValid);
        }
    }
}
