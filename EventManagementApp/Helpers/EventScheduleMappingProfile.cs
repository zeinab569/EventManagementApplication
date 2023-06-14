using AutoMapper;
using Core.Entities;
using EventManagementApp.Dtos;

namespace EventManagementApp.Helpers
{
    public class EventScheduleMappingProfile : Profile
    {
        public EventScheduleMappingProfile()
        {
            CreateMap<EventSchedule, EventScheduleDTO>()
                .ForMember(e => e.Event, e => e.MapFrom(e => e.Event.EventName))
                .ReverseMap();
        }
    }
}
