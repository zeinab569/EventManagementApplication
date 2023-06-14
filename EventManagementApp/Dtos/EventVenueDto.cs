using Core.Entities;

namespace EventManagementApp.Dtos
{
    public class EventVenueDto
    {
        public int Id { get; set; }
        public string VenueName { get; set; }
        public string VenueAddress { get; set; }
        public int VenueCapacity { get; set; }
        public string VenueDetails { get; set; }

        public virtual ICollection<Event> Events { get; set; } = new List<Event>();

    }
}
