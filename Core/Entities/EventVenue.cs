using System.Collections.Generic;

namespace Core.Entities
{
    public class EventVenue : BaseEntity
    {
        public string VenueName { get; set; }
        public string VenueAddress { get; set; }
        public int VenueCapacity { get; set; }
        public string VenueDetails { get; set; }
        public virtual ICollection<Event> Events { get; set; } = new List<Event>();


    }
}
