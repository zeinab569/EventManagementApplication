﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Core.Entities
{
    public class TicketPurchase : BaseEntity
    {
        [Required]

        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
        public DateTime PurchaseDate { get; set; }
        [MinLength(10)]
        public string PurchaseDetailes { get; set; }

        [ForeignKey("TicketId")]
        public int TicketId { get; set; }

        public Ticket Ticket { get; set; }

        public int? UserID {get;set;}
        
       
        
    }
}
