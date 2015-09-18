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
        [StringLength(250, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 10)]
        [DataType(DataType.Text)]
        [Display(Name = "Update Message")]
        public string UpdateText { get; set; }

        [Display(Name = "Status")]
        public CriteriaState UpdateStatus { get; set; }

        [Required]
        public int UpdateId { get; set; }
    }

    public class StatusUpdateViewModel
    {
        public StatusUpdate Update { get; set; }

        public StatusUpdateViewModel(StatusUpdate update)
        {
            this.Update = update;
        }
    }

    public class StatusUpdateViewFailedModel
    {
        public string Message { get; set; }
        public Exception Exception { get; set; }
    }
}