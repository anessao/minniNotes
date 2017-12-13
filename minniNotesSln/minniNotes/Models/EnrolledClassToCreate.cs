using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace minniNotes.Models
{
    public class EnrolledClassToCreate
    {
        public string Name { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int SchoolId { get; set; }
    }
}