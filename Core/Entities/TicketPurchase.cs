using System.ComponentModel.DataAnnotations.Schema;

namespace Core.Entities
{
    public class TicketPurchase: BaseEntity
    {
        
        public DateTime PurchaseDate { get;set; }
        public string PurchaseDetailes{ get;set; }


        public int TicketId { get; set; }
        [ForeignKey("TicketId")]
        public Ticket Ticket { get; set; }  
       
        
    }
}
