namespace EventManagementApp.Dtos.SpeakerDTOs
{
    public class SpeakerDTO : BaseDTO
    {
        public string SpeakerName { get; set; }
        public string SpeakerBio { get; set; }
        public string SpeakerImage { get; set; }
        public List<string> Events { get; set; } = new List<string>();
    }
}
