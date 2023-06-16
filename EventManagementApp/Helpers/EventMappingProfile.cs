using AutoMapper;
using Core.Entities;
using EventManagementApp.Dtos.EventDTOs;

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
            //.ForMember(e => e.Venue, e => e.MapFrom(e => e.Venue.VenueName))

            CreateMap<AddEventDTO, Event>()
                .ForMember(e => e.Sponsors.Select(s => s.Id), e => e.MapFrom(e => e.Sponsors))
                .ForMember(e => e.Speakers.Select(s => s.Id), e => e.MapFrom(e => e.Speakers));

        }
    }
}
