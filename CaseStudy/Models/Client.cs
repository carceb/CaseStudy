using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CaseStudy.Models
{
    public class Client
    {
        
        [Key]
        public int ClientId { get; set; }
        [Required]
        [Display(Name = "Client Name")]
        public string ClientName { get; set; }

    }
}