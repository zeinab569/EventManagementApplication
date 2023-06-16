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
                .ForMember(dest => dest.Sponsors, opt => opt.MapFrom(src => src.Sponsors.Select(id => new Sponsor { Id = id })))
                .ForMember(dest => dest.Speakers, opt => opt.MapFrom(src => src.Speakers.Select(id => new Speaker { Id = id }))); */

        }
    }
}
