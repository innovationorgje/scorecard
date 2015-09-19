using System;
using System.Linq;
using System.Collections.Generic;

namespace scorecard.Models
{
    public enum CriteriaState
    {
        Red = 0,
        Amber = 1,
        Green = 2
    }

    public class Criteria
    {
        public int ID { get; set; }
        public string Reference { get; set; }
        public string Title { get; set; }
        public string Summary { get; set; }
        public CriteriaState State { get; set; }

        public virtual Group Group { get; set; }
        public virtual ICollection<StatusUpdate> Updates { get; set; }

        public IEnumerable<StatusUpdate> LatestUpdates
        {
            get
            {
                return this.Updates.OrderBy(u => u.Stamp);
            }
        }

        public string CssClass
        {
            get
            {
                return Criteria.ToCssClass(this.State);
            }
        }

        public static string ToCssClass(CriteriaState state)
        {
            string css = string.Empty;

            switch (state)
            {
                case CriteriaState.Red:
                    css = "danger";
                    break;
                case CriteriaState.Amber:
                    css = "warning";
                    break;
                case CriteriaState.Green:
                    css = "success";
                    break;
            }

            return css;
        }
    }
}