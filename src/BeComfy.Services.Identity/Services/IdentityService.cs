using System;
using System.Threading.Tasks;
using BeComfy.Common.Authentication;
using BeComfy.Common.Types.Exceptions;
using BeComfy.Services.Identity.Domain;
using BeComfy.Services.Identity.Repositories;
using Microsoft.AspNetCore.Identity;

namespace BeComfy.Services.Identity.Services
{
    public class IdentityService : IIdentityService
    {
        private readonly IUserRepository _userRepository;
        private readonly IPasswordHasher<User> _passwordHasher;
        private readonly IJwtHandler _jwtHandler;

        public IdentityService(IUserRepository userRepository, IPasswordHasher<User> passwordHasher,
            IJwtHandler jwtHandler)
        {
            _userRepository = userRepository;
            _passwordHasher = passwordHasher;
            _jwtHandler = jwtHandler;
        }

        public async Task<JsonWebToken> SignInAsync(string email, string password)
        {
            var user = await _userRepository.GetAsync(email);
            if (user == null || !user.ValidatePassword(password, _passwordHasher))
            {
                throw new BeComfyDomainException("Invalid credentials.");
            }

            var jwt = _jwtHandler.CreateToken(user.Id.ToString("N"), user.Role, claims: null);

            return jwt;
        }

        public async Task SignUpAsync(Guid id, string email, string password, string role = "user")
        {
            var user = await _userRepository.GetAsync(email);

            if (user != null)
            {
                throw new BeComfyDomainException($"Email {email} is already in use");
            }

            if (string.IsNullOrWhiteSpace(role))
            {
                role = Role.User;
            }

            user = new User(id , role, email, password, DateTime.Now, DateTime.Now);
            user.SetPassword(password, _passwordHasher);

            await _userRepository.AddAsync(user);
        }
    }
}