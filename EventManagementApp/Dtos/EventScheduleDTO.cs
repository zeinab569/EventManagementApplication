namespace EventManagementApp.Dtos
{
    public class EventScheduleDTO : BaseDTO
    {
        public string StartTime { get; set; }
        public string EndTime { get; set; }
        public string ActivityDescription { get; set; }
        public int EventId { get; set; }
        public string Event { get; set; }
    }
}
