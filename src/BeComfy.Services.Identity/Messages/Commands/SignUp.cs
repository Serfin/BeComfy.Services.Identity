using System;
using BeComfy.Common.CqrsFlow;
using Newtonsoft.Json;

namespace BeComfy.Services.Identity.Messages.Commands
{
    public class SignUp : ICommand
    {
        public string Email { get; }
        public string Password { get; }

        [JsonConstructor]
        public SignUp(string email, string password)
        {
            Email = email;
            Password = password;
        }
    }
}