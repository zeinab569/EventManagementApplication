using AutoMapper;
using Core.Entities;
using EventManagementApp.Dtos;

namespace EventManagementApp.Helpers
{
    public class TicketPurchasesMappingProfile:Profile
    {

        public TicketPurchasesMappingProfile()
        {
            CreateMap<TicketPurchase, TicketPurchasesDTO>().
                ForMember(e => e.TicketPrice, opt => opt.MapFrom(e => e.Ticket.TicketPrice))
                .ForMember(e => e.TicketType, opt => opt.MapFrom(e => e.Ticket.TicketType))
        
                
               .ReverseMap();
            CreateMap<TicketPurchase, TicketPurchasePostDTO>().
             ForMember(e => e.TicketId, opt => opt.MapFrom(e => e.TicketId))
             .ReverseMap();
        }
    }
}
