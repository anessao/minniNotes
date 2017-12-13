using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace minniNotes.Models
{
    public class CardToCreate
    {
       public string Question { get; set; }
        public int SchoolId { get; set; }
        public string Answer { get; set; }
        public int DeckId { get; set; }
    }
}