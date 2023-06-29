using AutoMapper;
using Core.Entities;
using EventManagementApp.Dtos.EventScheduleDTOs;

namespace EventManagementApp.Helpers
{
    public class EventScheduleMappingProfile : Profile
    {
        public EventScheduleMappingProfile()
        {
            CreateMap<EventSchedule, EventScheduleDTO>()
                .ForMember(e => e.Event, e => e.MapFrom(e => e.Event.EventName));

            CreateMap<AddScheduleDTO, EventSchedule>()
                .ForMember(e => e.EventId, e => e.MapFrom(e => e.EventId));
        }
    }
}
