﻿using System;
using System.Collections.Generic;

namespace scorecard.Models
{
    public enum CriteriaState
    {
        Red = 1,
        Amber = 2,
        Green = 3
    }

    public class Criteria
    {
        public int ID { get; set; }
        public string Title { get; set; }
        public string Summary { get; set; }
        public CriteriaState State { get; set; }

        public virtual Group Group { get; set; }
        public virtual ICollection<StatusUpdate> Updates { get; set; }
    }
}