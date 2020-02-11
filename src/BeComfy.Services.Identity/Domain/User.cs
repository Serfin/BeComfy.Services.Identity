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
        
        public User(Guid id, string role, string email, string password, 
            DateTime createdAt, DateTime updatedAt, 
            string firstname = null, string secondname = null, string surname = null)
        {
            Id = id;
            Firstname = firstname;
            Secondname = secondname;
            Surname = surname;
            Role = role.ToUpperInvariant();
            Email = email.ToLowerInvariant();
            Password = password;
            CreatedAt = createdAt;
            UpdatedAt = updatedAt;
        }

        public Guid Id { get; set; }
        public string Firstname { get; set; }
        public string Secondname { get; set; }
        public string Surname { get; set; }
        public string Role { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }

        public void SetPassword(string password, IPasswordHasher<User> passwordHasher)
        {
            if (string.IsNullOrWhiteSpace(password))
            {
                throw new BeComfyDomainException("Password can not be empty.");
            }         
                
            Password = passwordHasher.HashPassword(this, password);
        }

        public bool ValidatePassword(string password, IPasswordHasher<User> passwordHasher)
            => passwordHasher.VerifyHashedPassword(this, Password, password) != PasswordVerificationResult.Failed;
    }
}