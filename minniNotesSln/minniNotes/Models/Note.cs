using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace minniNotes.Models
{
    public class Note
    {
        public int Id { get; set; }
        public string NoteText { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime DateLastEdited { get; set; }
        public string Title { get; set; }

        public virtual ApplicationUser UserId { get; set; }
        public virtual CardDeck CardDeck { get; set; }
        public virtual School School { get; set; }
        public virtual EnrolledClass EnrolledClass { get; set; }
    }
}