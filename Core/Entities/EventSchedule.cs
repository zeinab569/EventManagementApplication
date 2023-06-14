using System.ComponentModel.DataAnnotations.Schema;

namespace Core.Entities
{
    public class EventSchedule: BaseEntity
    {  
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public string ActivityDescription { get; set;}

        public int EventId { get; set; }
        [ForeignKey("EventId")]
        public Event Event { get; set; }
        
    }
}
