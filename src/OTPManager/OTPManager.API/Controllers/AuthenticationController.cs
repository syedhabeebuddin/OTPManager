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
        public AuthenticationController(IOTPAuthenticator otpAuthenticator)
        {
            _otpAuthenticator = otpAuthenticator;
        }

        [HttpGet("{phone}")]
        [ProducesResponseType(typeof(string), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult Get2FACode([CustomPhone] string phone)
        {
            if (_otpAuthenticator.IsLimitExceeded(phone))
                return Problem("Reached Max Limit");

            return Ok(_otpAuthenticator.GenerateCode(phone));
        }
        
        [HttpPost]
        [ProducesResponseType(typeof(bool), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult Verify2FACode(VerifyCodeRequest verifyCodeRequest)
        {            
            return Ok(_otpAuthenticator.VerifyCode(verifyCodeRequest.Phone, verifyCodeRequest.Code));
        }
    }
}
