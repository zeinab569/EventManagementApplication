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
    public class SponsorRepo : GenericRepo<Sponsor>, ISponsorRepo
    {
        private readonly EventManagementContext _context;
        public SponsorRepo(EventManagementContext context) : base(context)
        {
            _context = context;

        }
        public bool IsSponsorExist(int sponsorId)
        {
            return _context.Sponsors.Any(p => p.Id == sponsorId);
        }
    }
}
