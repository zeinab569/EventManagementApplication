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
    public class EventScheduleRepo : GenericRepo<EventSchedule>, IEventScheduleRepo
    {
        private readonly EventManagementContext _context;
        public EventScheduleRepo(EventManagementContext context) : base(context)
        {
        }
    }
}
