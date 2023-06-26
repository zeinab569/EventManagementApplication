using AutoMapper;
using Core.Entities;
using EventManagementApp.Dtos.EventDTOs;
using Infrastructure.Data;

namespace EventManagementApp.Helpers
{
    public class EventMappingProfile : Profile
    {
        public EventMappingProfile()
        {
            CreateMap<Event, EventDTO>();

            CreateMap<Event, EventByIdDTO>()
                .ForMember(e => e.Sponsors, e => e.MapFrom(e => e.Sponsors.Select(s => s.SponsorName)))
                .ForMember(e => e.Speakers, e => e.MapFrom(e => e.Speakers.Select(s => s.SpeakerName)))
                .ForMember(e => e.Gallaries, e => e.MapFrom(e => e.Gallaries.Select(s => s.Photo)));


            CreateMap<AddEventDTO, Event>()
                .ForMember(dest => dest.Speakers, opt => opt.Ignore())
                .ForMember(dest => dest.Sponsors, opt => opt.Ignore());


        }
    }
}
