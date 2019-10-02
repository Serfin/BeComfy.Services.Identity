using System;
using System.Threading.Tasks;
using BeComfy.Common.Authentication;
using BeComfy.Services.Identity.Domain;

namespace BeComfy.Services.Identity.Services
{
    public interface IIdentityService
    {
        Task SignUpAsync(Guid id, string email, string password, string role = Role.User);
        Task<JsonWebToken> SignInAsync(string email, string password);
    }
}