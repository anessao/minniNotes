using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


using System.Data.Entity;

namespace minniNotes.Models
{
    public class AppDbContext : DbContext
    {
        public AppDbContext() : base("MinniNotes")
        { }

        public DbSet<Card> Cards { get; set; }
        public DbSet<CardDeck> Decks { get; set; }
        public DbSet<School> Schools { get; set; }
        public DbSet<EnrolledClass> Classes { get; set; }
        public DbSet<Note> Notes { get; set; }
    }
}