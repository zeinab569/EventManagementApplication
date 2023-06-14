using AutoMapper;
using Core.Entities;
using Core.Identity;
using EventManagementApp.Dtos.AccountDto;

namespace EventManagementApp.Helpers
{
    public class MappingProfiles:Profile
    {
        public MappingProfiles() 
        {
            CreateMap<Core.Identity.Address, AddressDto>().ReverseMap();
        }

    }
}
