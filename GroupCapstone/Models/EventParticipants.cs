using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace GroupCapstone.Models
{
    public class EventParticipants
    {
        [Key]
        public int Id { get; set; }
        public int EventId { get; set; }

        public int ParticipantId { get; set; }
    }
}
