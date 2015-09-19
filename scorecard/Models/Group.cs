using System;
using System.Collections.Generic;

namespace scorecard.Models
{
    public class Group
    {
        public int ID { get; set; }
        public string Title { get; set; }

        public virtual ICollection<Criteria> Criteria {get; set;}
     }
}