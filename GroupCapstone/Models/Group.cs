using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace GroupCapstone.Models
{
    public class Group
    {
        public int ID { get; set; }
        public string GroupName { get; set; }

        //[ForeignKey("Event")]
    }
}
