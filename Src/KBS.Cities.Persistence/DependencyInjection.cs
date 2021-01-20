using KBS.Cities.Application.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace KBS.Cities.Persistence
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddPersistenceInMemory(this IServiceCollection services)
        {
            services.AddDbContext<InMemoryDbContext>(options => options.UseInMemoryDatabase("in_memory"));
            services.AddScoped<IDbContext>(provider => provider.GetService<InMemoryDbContext>());
            return services;
        }
    }
}
