using Core.Entities;
using System.ComponentModel.DataAnnotations;

namespace EventManagementApp.Dtos
{
    public class TicketDTO
    {
       
        public string TicketType { get; set; }
         
         public int TicketId { get; set; }
        public decimal TicketPrice { get; set; }

        public int TicketQuentity { get; set; }
        public string TicketDetails { get; set; }

        public int EventId { get; set; }
        public string EventName { get; set; }
        public DateTime EventDate { get; set; }
        public DateTime EventTime { get; set; }

        public List<TicketPurchasesDTO> Purchase { get; set; }=new List<TicketPurchasesDTO>();

    }
}
