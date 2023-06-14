using Core.Entities;

namespace EventManagementApp.Dtos.EventDTOs
{
    public class EventByIdDTO : BaseEntity
    {
        public string EventName { get; set; }
        public string EventDescription { get; set; }
        public DateTime EventDate { get; set; }
        public DateTime EventTime { get; set; }
        public string EventImage { get; set; }
        public List<string> Sponsors { get; set; } = new List<string>();
        public List<string> Speakers { get; set; } = new List<string>();
        public List<string> Gallaries { get; set; } = new List<string>();
        public List<string> EventSchedules { get; set; } = new List<string>();
    }
}
