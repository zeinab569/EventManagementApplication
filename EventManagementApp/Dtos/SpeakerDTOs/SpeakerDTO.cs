namespace EventManagementApp.Dtos.SpeakerDTOs
{
    public class SpeakerDTO : BaseDTO
    {
        public string SpeakerName { get; set; }
        public string SpeakerBio { get; set; }
        public string SpeakerImage { get; set; }
        public string Event { get; internal set; }
    }
}
