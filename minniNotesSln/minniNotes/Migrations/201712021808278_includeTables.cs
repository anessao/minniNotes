namespace minniNotes.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class includeTables : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Cards",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Question = c.String(),
                        Answer = c.String(),
                        Deck_Id = c.Int(),
                        EnrolledClass_Id = c.Int(),
                        School_Id = c.Int(),
                        UserId_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.CardDecks", t => t.Deck_Id)
                .ForeignKey("dbo.EnrolledClasses", t => t.EnrolledClass_Id)
                .ForeignKey("dbo.Schools", t => t.School_Id)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId_Id)
                .Index(t => t.Deck_Id)
                .Index(t => t.EnrolledClass_Id)
                .Index(t => t.School_Id)
                .Index(t => t.UserId_Id);
            
            CreateTable(
                "dbo.CardDecks",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Title = c.String(),
                        HighestScore = c.Int(nullable: false),
                        UserId_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId_Id)
                .Index(t => t.UserId_Id);
            
            CreateTable(
                "dbo.EnrolledClasses",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        StartDate = c.DateTime(nullable: false),
                        EndDate = c.DateTime(nullable: false),
                        School_Id = c.Int(),
                        UserId_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Schools", t => t.School_Id)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId_Id)
                .Index(t => t.School_Id)
                .Index(t => t.UserId_Id);
            
            CreateTable(
                "dbo.Notes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        NoteText = c.String(),
                        DateCreated = c.DateTime(nullable: false),
                        DateLastEdited = c.DateTime(nullable: false),
                        CardDeck_Id = c.Int(),
                        EnrolledClass_Id = c.Int(),
                        School_Id = c.Int(),
                        UserId_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.CardDecks", t => t.CardDeck_Id)
                .ForeignKey("dbo.EnrolledClasses", t => t.EnrolledClass_Id)
                .ForeignKey("dbo.Schools", t => t.School_Id)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId_Id)
                .Index(t => t.CardDeck_Id)
                .Index(t => t.EnrolledClass_Id)
                .Index(t => t.School_Id)
                .Index(t => t.UserId_Id);
            
            CreateTable(
                "dbo.Schools",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        City = c.String(),
                        State = c.String(),
                        isActive = c.Boolean(nullable: false),
                        UserId_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId_Id)
                .Index(t => t.UserId_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Cards", "UserId_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.Cards", "School_Id", "dbo.Schools");
            DropForeignKey("dbo.Cards", "EnrolledClass_Id", "dbo.EnrolledClasses");
            DropForeignKey("dbo.EnrolledClasses", "UserId_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.Notes", "UserId_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.Notes", "School_Id", "dbo.Schools");
            DropForeignKey("dbo.Schools", "UserId_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.EnrolledClasses", "School_Id", "dbo.Schools");
            DropForeignKey("dbo.Notes", "EnrolledClass_Id", "dbo.EnrolledClasses");
            DropForeignKey("dbo.Notes", "CardDeck_Id", "dbo.CardDecks");
            DropForeignKey("dbo.CardDecks", "UserId_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.Cards", "Deck_Id", "dbo.CardDecks");
            DropIndex("dbo.Schools", new[] { "UserId_Id" });
            DropIndex("dbo.Notes", new[] { "UserId_Id" });
            DropIndex("dbo.Notes", new[] { "School_Id" });
            DropIndex("dbo.Notes", new[] { "EnrolledClass_Id" });
            DropIndex("dbo.Notes", new[] { "CardDeck_Id" });
            DropIndex("dbo.EnrolledClasses", new[] { "UserId_Id" });
            DropIndex("dbo.EnrolledClasses", new[] { "School_Id" });
            DropIndex("dbo.CardDecks", new[] { "UserId_Id" });
            DropIndex("dbo.Cards", new[] { "UserId_Id" });
            DropIndex("dbo.Cards", new[] { "School_Id" });
            DropIndex("dbo.Cards", new[] { "EnrolledClass_Id" });
            DropIndex("dbo.Cards", new[] { "Deck_Id" });
            DropTable("dbo.Schools");
            DropTable("dbo.Notes");
            DropTable("dbo.EnrolledClasses");
            DropTable("dbo.CardDecks");
            DropTable("dbo.Cards");
        }
    }
}
