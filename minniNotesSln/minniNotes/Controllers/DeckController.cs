using Microsoft.AspNet.Identity;
using minniNotes.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;

namespace minniNotes.Controllers
{
    [RoutePrefix("api/deck")]
    [Authorize]
    public class DeckController : ApiController
    {
        [HttpPost, Route("add")]
        public HttpResponseMessage AddNewCardDeck(CardDeck requestedDeck)
        {
            var CurrentUserId = User.Identity.GetUserId();
            CurrentUserId.ToString();

            var db = new ApplicationDbContext();
            var newDeck = new CardDeck
            {
                Title = requestedDeck.Title,
                HighestScore = 0,
                Cards = null,
                UserId = db.Users.Find(CurrentUserId)
            };

            db.Decks.Add(newDeck);
            db.SaveChanges();

            return Request.CreateResponse(HttpStatusCode.Created, newDeck);
        }

        //Get all notes written by the user api/deck/list
        [HttpGet, Route("list")]
        public HttpResponseMessage GetAllCardDecks()
        {
            var UserId = User.Identity.GetUserId();
            UserId.ToString();

            var db = new ApplicationDbContext();
            var cardDecks = db.Decks;

            var listOfCardDecks = db.Decks.Where(x => x.UserId.Id.Contains(UserId));

            return Request.CreateResponse(HttpStatusCode.OK, listOfCardDecks);
        }

        //Edit Score api/deck/update
        [HttpPut, Route("update")]
        public HttpResponseMessage UpdateScoreOnDeck(DeckScore updatedScore)
        {
            var db = new ApplicationDbContext();
            var cardDeck = db.Decks.Where(x => x.Id.Equals(updatedScore.DeckId)).FirstOrDefault();

            var newCardScore = cardDeck.HighestScore = updatedScore.HighestScore;
            db.SaveChanges();

            return Request.CreateResponse(HttpStatusCode.Accepted, newCardScore);
        }

        //Delete Deck and cards
        [HttpDelete, Route("remove/{deckid}")]
        public HttpResponseMessage DeleteDeckAndCards(int deckid)
        {
            var db = new ApplicationDbContext();
            try
            {
                var listOfCardsByDeck = db.Cards.Where(x => x.Deck.Id.Equals(deckid));
                if (listOfCardsByDeck.ToList().Count > 0)
                {
                    foreach (Card card in listOfCardsByDeck)
                    {
                        db.Cards.Remove(card);
                    }
                }
            }
            catch
            {
                
            }
            var cardDeck = db.Decks.Where(x => x.Id.Equals(deckid)).FirstOrDefault();

            

            db.Decks.Remove(cardDeck);
            db.SaveChanges();

            return Request.CreateResponse(HttpStatusCode.Accepted);
        }
    }
}