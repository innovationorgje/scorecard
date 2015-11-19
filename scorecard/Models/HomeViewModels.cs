using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace scorecard.Models
{
    public class HomeIndexViewModel
    {
        public List<Group> Groups { get; set; }

        [Required]
        [DataType(DataType.Text)]
        [Display(Name = "Update Message")]
        public string UpdateText { get; set; }

        [Display(Name = "Status")]
        public CriteriaState UpdateStatus { get; set; }

        [Display(Name = "Type")]
        public UpdateType UpdateType { get; set; }

        [Required]
        public int UpdateId { get; set; }
    }

    public class StatusUpdateViewModel
    {
        public IEnumerable<StatusUpdate> Updates { get; set; }

        public StatusUpdateViewModel(IEnumerable<StatusUpdate> updates)
        {
            this.Updates = updates;
        }
    }

    public class StatusUpdateViewFailedModel
    {
        public string Message { get; set; }
        public Exception Exception { get; set; }
    }
}