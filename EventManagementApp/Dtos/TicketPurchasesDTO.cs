﻿using Core.Entities;
using System.ComponentModel.DataAnnotations;

namespace EventManagementApp.Dtos
{
    public class TicketPurchasesDTO
    {
        [Required]

        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
        public DateTime PurchaseDate { get; set; }
        [MinLength(10)]
        public string PurchaseDetailes { get; set; }
        public string TicketType { get; set; }
        public decimal TicketPrice { get; set; }
         
        public string UserName { get; set; }
    }
}
