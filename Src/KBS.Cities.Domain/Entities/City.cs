using System;

namespace KBS.Cities.Domain.Entities
{
    public class City
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public int Population { get; set; }
        public DateTime Established { get; set; }
    }
}
