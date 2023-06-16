using System.ComponentModel.DataAnnotations.Schema;

namespace Core.Entities
{
    public class Gallary: BaseEntity
    {
        
        public string Photo { get; set; }
        public string PhotoDetails { get; set;}
        public int EventId { get; set; }
        [ForeignKey("EventId")]
        public Event Event { get; set; }

    }
}
