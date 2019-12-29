using BeComfy.Services.Identity.Domain;
using Microsoft.EntityFrameworkCore;

namespace BeComfy.Services.Identity.EF
{
    public class IdentityContext : DbContext
    {
        public DbSet<User> Users { get; set; }

        public IdentityContext(DbContextOptions options)
            : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            
        }
    }
}