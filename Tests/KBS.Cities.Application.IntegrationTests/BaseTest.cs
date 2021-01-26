using KBS.Cities.Domain.Entities;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace KBS.Cities.Application.IntegrationTests
{
    using static TestSetup;

    public class BaseTest
    {
        [SetUp]
        public async Task TestSetUp()
        {
            await ResetState();
            await AddRangeAsync(new List<City>
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
                new() {Name = "Yekaterinburg", Population = 1_349_772, Established = new DateTime(1796, 11, 18)},
                new() {Name = "Nizhny Tagil", Population = 349_008, Established = new DateTime(1722, 10, 1)},
                new() {Name = "Kamensk-Uralsky", Population = 174_689, Established = new DateTime(1701, 1, 1)},
                new() {Name = "Pervouralsk", Population = 124_528, Established = new DateTime(1732, 1, 1)},
                new() {Name = "Serov", Population = 99_373, Established = new DateTime(1893, 1, 1)},
                new() {Name = "Novouralsk", Population = 85_522, Established = new DateTime(1941, 1, 1)},
                new() {Name = "Verkhnyaya Pyshma", Population = 59_749, Established = new DateTime(1660, 1, 1)},
                new() {Name = "Asbest", Population = 68_893, Established = new DateTime(1889, 1, 1)},
                new() {Name = "Revda", Population = 61_875, Established = new DateTime(1734, 1, 1)},
                new() {Name = "Polevskoy", Population = 64_220, Established = new DateTime(1821, 1, 1)}
            });
        }
    }
}
