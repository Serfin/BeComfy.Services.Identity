 using System;
 using System.Threading.Tasks;
 using BeComfy.Services.Identity.Domain;
 using BeComfy.Services.Identity.EF;
 using Microsoft.EntityFrameworkCore;

 namespace BeComfy.Services.Identity.Repositories
 {
     public class UserRepository// : IUserRepository
     {
         private readonly IdentityContext _context;

         public UserRepository(IdentityContext context)
         {
             _context = context;
         }
        
         public async Task AddAsync(User user)
         {
             await _context.Users.AddAsync(user);
             await _context.SaveChangesAsync();
         }

         public async Task DeleteAsync(Guid id)
         {
             var user = await GetAsync(id);
             _context.Users.Remove(user);
             await _context.SaveChangesAsync();
         }

         public async Task<User> GetAsync(string email)
         {
             var user = await _context.Users.FirstOrDefaultAsync(x => x.Email == email);

             return user;
         }

         public async Task<User> GetAsync(Guid id)
         {
             var user = await _context.Users.FindAsync(id);

             return user;
         }

         public async Task UpdateAsync(Guid id)
         {
             var user = await GetAsync(id);
             _context.Users.Update(user);
             await _context.SaveChangesAsync();
         }
     }
 }