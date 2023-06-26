using AutoMapper;
using Core.Entities;
using EventManagementApp.Dtos.GallaryDTOs;
using EventManagementApp.Dtos.SpeakerDTOs;

namespace EventManagementApp.Helpers
{
    public class GallaryMappingProfile :Profile
    {
        public GallaryMappingProfile()
        {
            CreateMap<Gallary, GallaryDTO>()
                .ForMember(e => e.Event, e => e.MapFrom(e => e.Event.EventName))
                .ReverseMap();

            CreateMap<Gallary, AddGallaryDTO>()
                //.ForMember(e => e.EventId, opt => opt.MapFrom(e => e.Event.Id))
                .ReverseMap();
        }
    }
}
