using System.ComponentModel.DataAnnotations;

namespace EventManagementApp.Dtos
{
    public class TicketPurchasePostDTO
    {
        [Required]

        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
        public DateTime PurchaseDate { get; set; }
        [MinLength(10)]
        public string PurchaseDetailes { get; set; }
        public int TicketId { get; set; }
        public int? UserId { get; set; }
    }
}
