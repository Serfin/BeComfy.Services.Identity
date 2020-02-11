using System;
using System.Threading.Tasks;
using BeComfy.Services.Identity.Domain;

namespace BeComfy.Services.Identity.Repositories
{
    public interface IUserRepository
    {
        Task AddAsync(User user);
        Task<User> GetAsync(string email);
        Task<User> GetAsync(Guid id);
        Task UpdateAsync(User id);
        Task DeleteAsync(Guid id);
    }
}