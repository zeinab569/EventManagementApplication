namespace Core.Entities
{
    public class Sponsor: BaseEntity
    {     
        public string SponsorName { get; set; }
        public string SponsorLogo { get; set; }
        public string SponsorDetails { get; set; }

        public virtual ICollection<Event> Events { get; set; }
    }
}
