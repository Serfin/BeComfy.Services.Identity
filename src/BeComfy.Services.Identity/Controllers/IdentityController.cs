using System.Threading.Tasks;
using BeComfy.Common.Authentication;
using BeComfy.Common.Mvc;
using BeComfy.Common.Types.Exceptions;
using BeComfy.Services.Identity.Messages.Commands;
using BeComfy.Services.Identity.Services;
using Microsoft.AspNetCore.Mvc;

namespace BeComfy.Services.Identity.Controllers
{
    [ApiController]
    [Route("")]
    public class IdentityController : BaseController
    {
        private readonly IIdentityService _identityService;

        public IdentityController(IIdentityService identityService)
        {
            _identityService = identityService;
        }
        
        [HttpGet("me")]
        [JwtAuthentication]
        public IActionResult Get() 
            => Content($"Your id: '{UserId}'.");

        [HttpPost("sign-up")]
        public async Task<IActionResult> SignUp(SignUp command)
        {
            try
            {
                await _identityService.SignUpAsync(command.Email.ToLowerInvariant(), 
                    command.Password);

                return NoContent();
            }
            catch (BeComfyDomainException domainEx)
            {
                return NotFound(domainEx.Message.ToString());
            }
        }

        [HttpPost("sign-in")]
        public async Task<IActionResult> SignIn(SignIn command)
        {
            try
            {
                var jwt = await _identityService.SignInAsync(command.Email.ToLowerInvariant(), command.Password);

                return Ok(jwt);
            }
            catch (BeComfyDomainException domainEx)
            {
                return NotFound(domainEx.Message.ToString());
            }
        }
    }
}