using KBS.Cities.Application.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace KBS.Cities.Persistence
{
    public class InMemoryDbContext : DbContext, IDbContext
    {
        public InMemoryDbContext(DbContextOptions<InMemoryDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
            base.OnModelCreating(builder);
        }
    }
}
