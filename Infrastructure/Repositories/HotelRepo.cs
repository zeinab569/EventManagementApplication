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
    public class HotelRepo : GenericRepo<Hotel>, IHotelRepo
    {
        private readonly EventManagementContext _context;
        public HotelRepo(EventManagementContext context) : base(context)
        {
            _context = context;
        }
        public bool IsHotelExist(int hotelId)
        {
            return _context.Hotels.Any(p => p.Id == hotelId);
        }
    }
}
