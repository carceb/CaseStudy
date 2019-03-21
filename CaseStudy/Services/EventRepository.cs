using CaseStudy.DAL;
using CaseStudy.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;

namespace CaseStudy.Services
{
    public class EventRepository
    {
        public List<Event> GetAll()
        {
            using (var db = new CaseStudyContext())
            {
                return db.Events.Include(x => x.Sessions).ToList();
            }
        }

        internal void Create(Event model)
        {
            using (var db = new CaseStudyContext())
            {
                db.Events.Add(model);
                db.SaveChanges();
            }
        }
    }
}