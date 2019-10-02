using System;
using Microsoft.AspNetCore.Mvc;

namespace BeComfy.Services.Identity.Controllers
{
    public class BaseController : ControllerBase
    {
        protected bool IsAdmin
            => User.IsInRole("admin");

        protected Guid UserId
            => string.IsNullOrEmpty(User?.Identity?.Name)
                ? Guid.Empty
                : Guid.Parse(User.Identity.Name);
    }
}