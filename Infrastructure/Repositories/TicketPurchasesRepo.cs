using Core.Entities;
using Core.Interfaces;
using Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
    public class TicketPurchasesRepo : GenericRepo<TicketPurchase>,ITicketPurchasesRepo
    {
        private readonly EventManagementContext _context;
        public TicketPurchasesRepo(EventManagementContext context) : base(context)
        {
        }
    }
}
