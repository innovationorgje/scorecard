using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace scorecard.Models
{
    public class ExternalLoginConfirmationViewModel
    {
        [Required]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required]
        [Display(Name = "Full Name")]
        public string FullName { get; set; }

        public string LinkedInToken { get; set; }
        public string LinkedInProfile { get; set; }
    }

    public class ExternalLoginFailureViewModel
    {
        public string EmailAddress { get; set; }

        public ExternalLoginFailureViewModel()
        {
        }

        public ExternalLoginFailureViewModel(string emailAddress)
        {
            this.EmailAddress = emailAddress;
        }
    }
}
