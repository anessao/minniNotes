using System.Collections.Generic;

namespace minniNotes.Models
{
    public class CardDeck
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public int HighestScore { get; set; }

        public virtual List<Card> Cards { get; set; }
        public virtual ApplicationUser UserId { get; set; }
    }
}