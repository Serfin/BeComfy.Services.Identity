using System;
using BeComfy.Common.Mongo;
using BeComfy.Common.Types.Exceptions;
using Microsoft.AspNetCore.Identity;

namespace BeComfy.Services.Identity.Domain
{
    public class User : IEntity
    {
        public User()
        {
            
        }
        
        public User(string role, string email, string password, 
            string firstname = null, string secondname = null, string surname = null)
        {
            Id = Guid.NewGuid();
            Firstname = firstname;
            Secondname = secondname;
            Surname = surname;
            Role = role.ToUpperInvariant();
            Email = email.ToLowerInvariant();
            Password = password;
            CreatedAt = DateTime.Now;
            UpdatedAt = DateTime.Now;
        }

        public Guid Id { get; set; }
        public string Firstname { get; set; }
        public string Secondname { get; set; }
        public string Surname { get; set; }
        public string Role { get; set; }
        public string Email { get; set; }
        public string Password { get; private set; }
        public DateTime CreatedAt { get; private set; }
        public DateTime UpdatedAt { get; private set; }

        public void SetPassword(string hashedPassword)
        {
            Password = hashedPassword;
        }
    }
}