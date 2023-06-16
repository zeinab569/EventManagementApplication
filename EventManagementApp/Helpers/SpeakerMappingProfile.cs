using AutoMapper;
using Core.Entities;
using EventManagementApp.Dtos.SpeakerDTOs;

namespace EventManagementApp.Helpers
{
    public class SpeakerMappingProfile : Profile
    {
        public SpeakerMappingProfile()
        {
            CreateMap<Speaker, SpeakerDTO>()
                .ForMember(dest => dest.Events, opt => opt.MapFrom(src => src.Events.Select(s => s.EventName)));

            CreateMap<AddSpeakerDTO, Speaker>()
                .ForMember(dest => dest.Events, opt => opt.MapFrom(src => src.Events.Select(id => new Event { Id = id })));
        }
    }
}


// 6:40
