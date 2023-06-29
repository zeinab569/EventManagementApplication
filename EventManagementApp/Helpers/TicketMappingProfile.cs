using AutoMapper;
using Core.Entities;
using EventManagementApp.Dtos;

namespace EventManagementApp.Helpers
{
    public class TicketMappingProfile:Profile
    {
        public TicketMappingProfile()
        {
         CreateMap<Ticket,TicketDTO>()
        .ForMember(e=>e.EventName,opt=>opt.MapFrom(e=>e.Event.EventName))
        .ForMember(e=>e.EventDate,opt=>opt.MapFrom(e=>e.Event.EventDate))
        .ForMember(e=>e.EventTime,opt=>opt.MapFrom(e=>e.Event.EventTime))
        .ForMember(e=>e.EventId,opt=>opt.MapFrom(e=>e.Event.Id))
        .ForMember(e=>e.TicketId,opt=>opt.MapFrom(e=>e.Id))
        .ReverseMap();

         CreateMap<Ticket, TicketPostDTO>()
         .ForMember(e => e.EventId, opt => opt.MapFrom(e => e.EventId))
         .ReverseMap();
           
        }
    }
}
