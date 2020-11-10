using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GroupCapstone.Models
{
    public class EventUserGroupViewModel
    {
        public IEnumerable<Group> Groups { get; set; }

        public IEnumerable<UserGroup> UserGroups { get; set; }

        public string UserName { get; set; }

        public IEnumerable<Event> Events { get; set; }

        public IEnumerable<EventParticipants> EventParticipants { get; set; }

        public IEnumerable<Participant> Participants { get; set; }

        public string UserId { get; set; }

        public int participantId { get; set; }
        public IEnumerable<Event> Filtered { get; set; }
    }
}
