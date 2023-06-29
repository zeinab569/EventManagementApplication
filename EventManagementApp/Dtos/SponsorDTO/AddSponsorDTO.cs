namespace EventManagementApp.Dtos.SponsorDTO
{
    public class AddSponsorDTO
    {
        public string SponsorName { get; set; }
        public IFormFile? SponsorLogo { get; set; }
        public string SponsorDetails { get; set; }
    }
}
