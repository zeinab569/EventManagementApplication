namespace EventManagementApp.Dtos.SpeakerDTOs
{
    public class AddSpeakerDTO
    {
        public string SpeakerName { get; set; }
        public string SpeakerBio { get; set; }
        public string SpeakerImage { get; set; }
        public List<int> EventId { get; set; }=new List<int> { };
    }
}
