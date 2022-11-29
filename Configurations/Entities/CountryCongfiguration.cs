using HotelListing.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HotelListing.Configurations.Entities
{
    public class CountryCongfiguration : IEntityTypeConfiguration<Country>
    {
        public void Configure(EntityTypeBuilder<Country> builder)
        {
            builder.HasData(
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
        }
    }
}