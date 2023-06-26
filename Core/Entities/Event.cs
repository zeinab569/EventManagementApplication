using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Entities
{
    public class Event : BaseEntity
    {

        public string EventName { get; set; }
        public string EventDescription { get; set; }
        public DateTime EventDate { get; set; }
        public DateTime EventTime { get; set; }
        public string EventImage { get; set; }
        public virtual List<Sponsor> Sponsors { get; set; } = new List<Sponsor>();
        public virtual List<Speaker> Speakers { get; set; } = new List<Speaker>();
        public virtual ICollection<Gallary> Gallaries { get; set; } = new List<Gallary>();
        public virtual ICollection<EventSchedule> EventSchedules { get; set; } = new List<EventSchedule>();

    }
}
