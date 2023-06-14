using Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Data.Config
{
    public class EventTicketConfiguration : IEntityTypeConfiguration<EventTicket>
    {
        public void Configure(EntityTypeBuilder<EventTicket> builder)
        {
            builder.HasKey(k => new { k.EventId, k.TicketId, k.TicketQuentity });
        }
    }
}
