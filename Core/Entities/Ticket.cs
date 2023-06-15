using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Core.Entities
{
    public  partial class Ticket: BaseEntity
    {

        [EnumDataType(typeof(Validation.TicketEnum),ErrorMessage ="Enum must be in Standard|Pro|Premium")]
        public string TicketType { get; set; }

        [Required(ErrorMessage = "Ticket Price is Required")]
        [Range(100, 50000)]
        public decimal TicketPrice { get; set; }

        [Required(ErrorMessage = "Ticket Quantity is Required")]
        public int TicketQuentity { get; set; }
        public string TicketDetails { get; set; }

       [ForeignKey("EventId")]
        public  int?  EventId { get; set; }
        
        public Event Event { get; set; }

        
        public virtual ICollection<TicketPurchase> Purchase { get; set; } = new List<TicketPurchase>();
    }
}
