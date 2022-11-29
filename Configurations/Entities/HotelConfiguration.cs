using HotelListing.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HotelListing.Configurations.Entities
{
    public class HotelConfiguration : IEntityTypeConfiguration<Hotel>
    {
        public void Configure(EntityTypeBuilder<Hotel> builder)
        {
            builder.HasData(
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