using Core.Entities;
using Core.Interfaces;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

    

namespace Infrastructure.Repositories
{
    public  class TicketRepo : GenericRepo<Ticket>,ITicketRepo
    {

        private readonly EventManagementContext _context;
        public TicketRepo(EventManagementContext context) : base(context)
        {
            _context = context;
        }

        public async Task<IReadOnlyList<Ticket>> GetListByEventIDAsync(int id)
        {
            return await _context.Tickets.Where(e=>e.EventId==id).ToListAsync();
        }
    }
}
