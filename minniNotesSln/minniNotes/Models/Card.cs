namespace minniNotes.Models
{
    public class Card
    {
        public int Id { get; set; }
        public string Question { get; set; }
        public string Answer { get; set; }

        public virtual CardDeck Deck { get; set; }
        public virtual School School { get; set; }
        public virtual EnrolledClass EnrolledClass { get; set; }
        public virtual ApplicationUser UserId { get; set; }
    }
}