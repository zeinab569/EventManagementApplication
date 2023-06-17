using Core.Entities;
using EventManagementApp.Dtos.EventDTOs;

namespace EventManagementApp.Dtos.SponsorDTO
{
    public class SponsorDTO : BaseDTO
    {
        public string SponsorName { get; set; }
        public string SponsorLogo { get; set; }
        public string SponsorDetails { get; set; }
        public List<string> Events { get; set; } = new List<string>();

    }
}
