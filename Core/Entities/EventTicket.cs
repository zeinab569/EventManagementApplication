using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Entities
{
    public class EventTicket
    {
        public int TicketQuentity { get; set; }
        [ForeignKey("Event")]
        public int EventId { get; set; }
        [ForeignKey("Ticket")]
        public int TicketId { get; set; }
        public Event Event { get; set; }
        public Ticket Ticket { get; set; }
    }
}
