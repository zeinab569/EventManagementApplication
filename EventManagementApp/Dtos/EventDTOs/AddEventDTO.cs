namespace EventManagementApp.Dtos.EventDTOs
{
    public class AddEventDTO
    {
        public string EventName { get; set; }
        public string EventDescription { get; set; }
        public DateTime EventDate { get; set; }
        public DateTime EventTime { get; set; }
        public string EventImage { get; set; }
        public List<int> Sponsors { get; set; } = new List<int>();
        public List<int> Speakers { get; set; } = new List<int>();
        public List<int> Gallaries { get; set; } = new List<int>();
        public List<int> EventSchedules { get; set; } = new List<int>();
    }
}
