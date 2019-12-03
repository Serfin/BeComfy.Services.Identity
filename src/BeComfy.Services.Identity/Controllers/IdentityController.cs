using System.Threading.Tasks;
using BeComfy.Common.Authentication;
using BeComfy.Common.Mvc;
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
            => Content($"Your id: '{UserId:N}'.");

        [HttpPost("sign-up")]
        public async Task<IActionResult> SignUp(SignUp command)
        {
            command.BindId(c => c.Id);
            await _identityService.SignUpAsync(command.Id, command.Email.ToLowerInvariant(), 
                command.Password, command.Role);

            return NoContent();
        }

        [HttpPost("sign-in")]
        public async Task<IActionResult> SignIn(SignIn command)
            => Ok(await _identityService.SignInAsync(command.Email.ToLowerInvariant(), command.Password));
    }
}