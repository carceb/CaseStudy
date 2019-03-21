using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace CaseStudy.Models
{
    public class Session
    {
        [Key]
        public int SessionID { get; set; }

        [ForeignKey("EventID")]
        public Event Event { get; set; }

        [StringLength(300)]
        [Display(Name = "Session Name")]
        public string SessionName { get; set; }

        public DateTime Hour { get; set; }
 
        [Display(Name = "Maximum Capacity")]
        public int MaximumCapacity { get; set; }

        public int EventID { get; set; }

    }
}