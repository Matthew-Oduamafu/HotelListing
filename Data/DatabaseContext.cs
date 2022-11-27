using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace HotelListing.Data
{
    public class DatabaseContext:DbContext
    {
        public DatabaseContext(DbContextOptions options):base(options)
        { }

        public DbSet<Country> Countries { get; set; }
        public DbSet<Hotel> Hotels { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Country>().HasData(
                new Country
                {
                    Id = 1,
                    Name = "Ghana",
                    ShortName = "GH"
                }, new Country
                {
                    Id = 2,
                    Name = "Jamaica",
                    ShortName = "JM"
                }, new Country
                {
                    Id = 3,
                    Name = "Nigeria",
                    ShortName = "NG"
                });

            modelBuilder.Entity<Hotel>().HasData(
                new Hotel
                {
                    Id = 1,
                    Name = "Sandals Resort and SPA",
                    Address = "Nigeril",
                    CountryId = 1,
                    Rating = 4.5
                }, new Hotel
                {
                    Id = 2,
                    Name = "Comfort Suites",
                    Address = "Goerge Town",
                    CountryId = 3,
                    Rating = 4.8
                }, new Hotel
                {
                    Id = 3,
                    Name = "Grand Palladium",
                    Address = "Nassua",
                    CountryId = 2,
                    Rating = 4.3
                });
        }
    }
}
