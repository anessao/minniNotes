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
    [RoutePrefix("api/cards")]
    [Authorize]
    public class CardController : ApiController
    {
        [HttpPost, Route("add")]
        public HttpResponseMessage AddNewCard(CardToCreate requestedCard)
        {
            var CurrentUserId = User.Identity.GetUserId();
            CurrentUserId.ToString();

            var db = new ApplicationDbContext();
            var newCard = new Card
            {
                Question = requestedCard.Question,
                UserId = db.Users.Find(CurrentUserId),
                School = db.Schools.Find(requestedCard.SchoolId),
                Answer = requestedCard.Answer,
                Deck = db.Decks.Find(requestedCard.DeckId)
            };

            db.Cards.Add(newCard);
            db.SaveChanges();

            return Request.CreateResponse(HttpStatusCode.Created, newCard);
        }

        //Get all notes written by the user api/note/list
        [HttpGet, Route("list/{deckId}")]
        public HttpResponseMessage GetAllCardsByDeck(int deckId)
        {
            var UserId = User.Identity.GetUserId();
            UserId.ToString();

            var db = new ApplicationDbContext();
            var cards = db.Cards;

            var listOfCardsByDeck = db.Cards.Where(x => x.Deck.Id.Equals(deckId));

            return Request.CreateResponse(HttpStatusCode.OK, listOfCardsByDeck);
        }

        [HttpDelete, Route("remove/{cardid}")]
        public HttpResponseMessage DeleteNote(int cardid)
        {
            var db = new ApplicationDbContext();

            var card = db.Cards.Where(x => x.Id.Equals(cardid)).FirstOrDefault();

            db.Cards.Remove(card);
            db.SaveChanges();

            return Request.CreateResponse(HttpStatusCode.Accepted);
        }
    }
}