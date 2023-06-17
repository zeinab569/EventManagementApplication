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
                .ForMember(e => e.Event, e => e.MapFrom(e => e.Events.Select(s => s.EventName)))
                .ReverseMap();

            CreateMap<Speaker, AddSpeakerDTO>().
                ForMember(e => e.EventId, opt => opt.MapFrom(e => e.Events.Select(s => s.Id)))
            .ReverseMap();
        }
    }
}


// 6:40
