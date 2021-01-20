using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using KBS.Cities.Domain.Entities;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Serilog;

namespace KBS.Cities.Persistence.Extensions
{
    public static class HostBuilderExtensions
    {
        public static IHost MigrateInMemoryDbContext<TContext>(this IHost host) where TContext : InMemoryDbContext
        {
            using var scope = host.Services.CreateScope();
            var services = scope.ServiceProvider;
            var context = services.GetService<TContext>();

            Log.Information("Seeding default cities...");
            
            context!.Set<City>().AddRange(new List<City>
            {
                new() {Name = "Kurgan", Population = 333_606, Established = new DateTime(1679, 1, 1)},
                new() {Name = "Shadrinsk", Population = 77_756, Established = new DateTime(1662, 1, 1)},
                new() {Name = "Shumikha", Population = 17_819, Established = new DateTime(1892, 1, 1)},
                new() {Name = "Kurtamysh", Population = 17_099, Established = new DateTime(1745, 1, 1)},
                new() {Name = "Kataysk", Population = 14_003, Established = new DateTime(1655, 1, 1)},
                new() {Name = "Dalmatovo", Population = 13_911, Established = new DateTime(1644, 1, 1)},
                new() {Name = "Petukhovo", Population = 11_292, Established = new DateTime(1892, 1, 1)},
                new() {Name = "Shchuchye", Population = 10_973, Established = new DateTime(1750, 1, 1)},
                new() {Name = "Vargashi", Population = 9_254, Established = new DateTime(1894, 1, 1)},
                new() {Name = "Kargapolye", Population = 8_433, Established = new DateTime(1821, 1, 1)},
            });
            context.SaveChangesAsync();

            Log.Information("Seeding is successful");

            return host;
        }
    }
}
