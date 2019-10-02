using BeComfy.Common.Messages;
using Newtonsoft.Json;

namespace BeComfy.Services.Identity.Messages.Commands
{
    public class SignIn : ICommand
    {
        public string Email { get; set; }
        public string Password { get; set; }

        [JsonConstructor]
        public SignIn(string email, string password)
        {
            Email = email;
            Password = password;
        }
    }
}