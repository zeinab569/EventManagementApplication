namespace EventManagementApp.Dtos.SpeakerDTOs
{
    public class AddSpeakerDTO
    {
        public string SpeakerName { get; set; }
        public string SpeakerBio { get; set; }
        public IFormFile? SpeakerImage { get; set; }
    }
}
