using AutoMapper;
using Core.Entities;
using EventManagementApp.Dtos;
using EventManagementApp.Dtos.SponsorDTO;

namespace EventManagementApp.Helpers
{
    public class SponsorMappingProfile : Profile
    {
        public SponsorMappingProfile() 
        {
            CreateMap<Sponsor, SponsorDTO>()
                .ForMember(e => e.Events, e => e.MapFrom(e => e.Events.Select(s => s.EventName)))
                .ReverseMap();

            CreateMap<Sponsor, AddSponsorDTO>().
                ForMember(e => e.EventId, opt => opt.MapFrom(e => e.Events.Select(s => s.Id)))
            .ReverseMap();
        }
    }
}
