using AutoMapper;
using HotelListing.Data;
using HotelListing.Models;

namespace HotelListing.Configurations
{
    public class MapperInitializer : Profile
    {
        public MapperInitializer()
        {
            CreateMap<Country, CreateCountryDto>().ReverseMap();
            CreateMap<Country, CountryDto>().ReverseMap();

            CreateMap<Hotel, CreateHotelDto>().ReverseMap();
            CreateMap<Hotel, HotelDto>().ReverseMap();

            CreateMap<ApiUser, UserDto>().ReverseMap();
        }
    }
}