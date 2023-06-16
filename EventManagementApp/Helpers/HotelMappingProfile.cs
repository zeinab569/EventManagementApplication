using AutoMapper;
using Core.Entities;
using EventManagementApp.Dtos.HotelDTO;
using EventManagementApp.Dtos.SponsorDTO;

namespace EventManagementApp.Helpers
{
    public class HotelMappingProfile : Profile
    {
        public HotelMappingProfile()
        {
            CreateMap<Hotel, HotelDTO>().ReverseMap();
            CreateMap<Hotel, AddHotelDTO>().ReverseMap();

        }
    }
}
