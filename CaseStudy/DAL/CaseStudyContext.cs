using CaseStudy.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Web;

namespace CaseStudy.DAL
{
    public class CaseStudyContext: DbContext
    {
        public CaseStudyContext(): base("DefaultConnection")
        {

        }
        public DbSet<Participant> Participants { get; set; }
        public DbSet<Registration> Registrations { get; set; }
        public DbSet<Session> Sessions { get; set; }
        public DbSet<Event> Events { get; set; }
        public System.Data.Entity.DbSet<CaseStudy.Models.Client> Clients { get; set; }
    }
}