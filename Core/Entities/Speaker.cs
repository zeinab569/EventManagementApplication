namespace Core.Entities
{
    public class Speaker: BaseEntity
    {      
        public string SpeakerName { get; set; }
        public string SpeakerBio { get; set; }
        public string SpeakerImage { get; set; }

        public virtual ICollection<Event> Events { get; set; }
    }
}
