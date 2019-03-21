using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace CaseStudy.Models
{
    public class Registration
    {
        [Key]
        public int RegistrationID { get; set; }

        public int ParticipantID { get; set; }

        public int SessionID { get; set; }

        [ForeignKey("ParticipantID")]
        public Participant Participants { get; set; }

        [ForeignKey("SessionID")]
        public Session Sessions { get; set; }

    }
}