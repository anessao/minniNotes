using System;
using System.Collections.Generic;

namespace minniNotes.Models
{
    public class EnrolledClass
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

        public virtual ApplicationUser UserId { get; set; }
        public virtual List<Note> Notes { get; set; }
        public virtual School School { get; set; }

    }
}