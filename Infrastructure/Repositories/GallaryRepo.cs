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
    public class GallaryRepo : GenericRepo<Gallary>, IGallaryRepo
    {
        private readonly EventManagementContext _context;
        public GallaryRepo(EventManagementContext context) : base(context)
        {
        }
    }
}
