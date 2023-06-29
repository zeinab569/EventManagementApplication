using Core.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Core.Entities
{
    public class TicketPurchase : BaseEntity
    {
        [Required]
        public DateTime PurchaseDate { get; set; }
        public string PurchaseDetailes { get; set; }

        [ForeignKey("TicketId")]
        public int TicketId { get; set; }

        public Ticket Ticket { get; set; }
      
        public int UserID {get;set;}
        [ForeignKey("UserID")]
        public User User { get; set; }

        public string? CardNumber { get; set; }



    }
}
