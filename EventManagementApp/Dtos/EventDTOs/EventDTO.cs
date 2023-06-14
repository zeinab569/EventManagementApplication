namespace EventManagementApp.Dtos.EventDTOs
{
    public class EventDTO : BaseDTO
    {
        public string EventName { get; set; }
        public string EventDescription { get; set; }
        public DateTime EventDate { get; set; }
        public DateTime EventTime { get; set; }
        public string EventImage { get; set; }
    }
}
