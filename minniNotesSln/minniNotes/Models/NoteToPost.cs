using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace minniNotes.Models
{
    public class NoteToPost
    {
        public string Title { get; set; }
        public int SchoolId { get; set; }
        public int ClassId { get; set; }
        public string NoteText { get; set; }
    }
}