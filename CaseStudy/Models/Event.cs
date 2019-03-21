using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CaseStudy.Models
{
    public class Event
    {
        [Key]
        public int EventID { get; set; }

        [Required]
        [StringLength(200)]
        [Display(Name ="Event Name")]
        public string EventName { get; set; }
        public List<Session> Sessions { get; set; }
    }
}