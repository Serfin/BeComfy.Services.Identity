using Microsoft.AspNetCore.Mvc;

namespace BeComfy.Services.Identity.Controllers
{
    [Route("[controller]")]
    public class HomeController : ControllerBase
    {
        public IActionResult Home()
            => Ok("BeComfy Identity Service");
    }
}