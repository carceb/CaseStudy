using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CaseStudy.Models
{
    public class Participant
    {
        [Key]
        public int ParticipantID { get; set; }

        [Required]
        [StringLength(200)]
        [Display(Name = "Participant Name")]
        public string ParticipantName { get; set; }
    }
}