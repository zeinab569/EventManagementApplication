using System.ComponentModel.DataAnnotations;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Model;

namespace EventManagementApp.Dtos
{
    public class TicketPostDTO
    {
        [EnumDataType(typeof(Core.Validation.TicketEnum), ErrorMessage = "Ticket Type must be in Basic|Pro|VIP")]
        public string TicketType { get; set; }

        [Required(ErrorMessage = "Ticket Price is Required")]
        [Range(100, 50000)]
        public decimal TicketPrice { get; set; }

        [Required(ErrorMessage = "Ticket Quantity is Required")]
        public int TicketQuentity { get; set; }
        public string TicketDetails { get; set; }
        public int EventId { get; set; }
    }
}
