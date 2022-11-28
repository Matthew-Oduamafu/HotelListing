using HotelListing.Data;
using Microsoft.AspNetCore.Identity;

namespace HotelListing.Services
{
    public static class ServiceExtensions
    {
        public static void ConfigureIdentityServices(this IServiceCollection services)
        {
            var builder = services.AddIdentityCore<ApiUser>(q => q.User.RequireUniqueEmail = true);
            builder = new IdentityBuilder(builder.UserType, typeof(IdentityRole), services);

            builder.AddEntityFrameworkStores<DatabaseContext>().AddDefaultTokenProviders();
        }
    }
}