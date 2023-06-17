namespace EventManagementApp.Dtos.EventScheduleDTOs
{
    public class EventScheduleDTO : BaseDTO
    {
        public string StartTime { get; set; }
        public string EndTime { get; set; }
        public string ActivityDescription { get; set; }
        public string Event { get; set; }
    }
}
