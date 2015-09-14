using System;
using System.Collections.Generic;

namespace scorecard.Models
{
    public class StatusUpdate
    {
        public int ID { get; set; }
        public string Text { get; set; }
        public CriteriaState StateFrom { get; set; }
        public CriteriaState StateTo { get; set; }
        public DateTime Stamp { get; set; }

        public virtual Criteria Criteria { get; set; }
        public virtual ApplicationUser User { get; set; }
    }
}