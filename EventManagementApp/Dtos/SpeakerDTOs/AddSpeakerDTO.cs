namespace EventManagementApp.Dtos.SpeakerDTOs
{
    public class AddSpeakerDTO : BaseDTO
    {
        public string SpeakerName { get; set; }
        public string SpeakerBio { get; set; }
        public string SpeakerImage { get; set; }
        public List<int> Events { get; set; } = new List<int>();
    }
}
