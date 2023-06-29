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
                .ForMember(e => e.Events, e => e.MapFrom(e => e.Events.Select(s => s.EventName)));
            CreateMap<AddSpeakerDTO, Speaker>();


        }
    }
}

