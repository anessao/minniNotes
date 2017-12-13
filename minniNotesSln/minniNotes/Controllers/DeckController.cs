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

        //Get all notes written by the user api/note/list
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
    }
}