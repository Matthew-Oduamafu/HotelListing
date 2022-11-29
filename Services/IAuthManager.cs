using HotelListing.Models;

namespace HotelListing.Services
{
    public interface IAuthManager
    {
        Task<bool> ValidateUser(LoginUserDto userDto);

        Task<string> CreateToken();
    }
}