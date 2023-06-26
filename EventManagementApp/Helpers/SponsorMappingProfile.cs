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
                .ForMember(e => e.Events, e => e.MapFrom(e => e.Events.Select(s => s.EventName)));

            CreateMap<AddSponsorDTO, Sponsor>();
        }
    }
}
