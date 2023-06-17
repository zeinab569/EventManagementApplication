namespace EventManagementApp.Dtos.EventScheduleDTOs
{
    public class AddScheduleDTO
    {
        public string StartTime { get; set; }
        public string EndTime { get; set; }
        public string ActivityDescription { get; set; }
        public int EventId { get; set; }
    }
}
