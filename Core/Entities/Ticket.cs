using System.ComponentModel.DataAnnotations.Schema;

namespace Core.Entities
{
    public class Ticket : BaseEntity
    {
        public string TicketName { get; set; }
        public decimal TicketPrice { get; set; }
        public virtual ICollection<TicketPurchase> Purchase { get; set; }
    }
}
