namespace EventManagementApp.Dtos.EventDTOs
{
    public class AddEventDTO
    {
        public string EventName { get; set; }
        public string EventDescription { get; set; }
        public DateTime EventDate { get; set; }
        public DateTime EventTime { get; set; }
        public IFormFile? EventImage { get; set; }
        public List<int> SponsorsId { get; set; } = new List<int>();
        public List<int> SpeakersId { get; set; } = new List<int>();

    }
}
