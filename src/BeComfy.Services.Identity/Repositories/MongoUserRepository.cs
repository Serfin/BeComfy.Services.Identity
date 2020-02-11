using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using BeComfy.Common.Mongo;
using BeComfy.Services.Identity.Domain;

namespace BeComfy.Services.Identity.Repositories
{
    public class MongoUserRepository : IUserRepository
    {
        private readonly IMongoRepository<User> _collection;

        public MongoUserRepository(IMongoRepository<User> collection)
        {
            _collection = collection;
        }

        public async Task AddAsync(User entity)
            => await _collection.AddAsync(entity);

        public async Task DeleteAsync(Guid id)
            => await _collection.DeleteAsync(id);

        public async Task<User> GetAsync(Guid id)
            => await _collection.GetAsync(x => x.Id == id);

        public async Task<User> GetAsync(string email)
            => await _collection.GetAsync(x => x.Email == email);


        public async Task UpdateAsync(User entity)
            => await _collection.UpdateAsync(entity);
    }
}