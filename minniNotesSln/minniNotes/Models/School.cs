using System.Collections.Generic;

namespace minniNotes.Models
{
    public class School
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public bool isActive { get; set; }

        public virtual ApplicationUser UserId { get; set; }
        public virtual List<EnrolledClass> EnrolledClasses { get; set; }
    }
}