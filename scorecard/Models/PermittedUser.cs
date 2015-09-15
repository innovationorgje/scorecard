using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace scorecard.Models
{
    public class PermittedUser
    {
        public int ID { get; set; }
        public string Email { get; set; }
        public string Roles { get; set; }
    }
}