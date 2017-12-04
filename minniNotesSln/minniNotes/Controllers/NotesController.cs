using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using minniNotes.Models;
using Microsoft.AspNet.Identity;

namespace minniNotes.Controllers
{
    [RoutePrefix("api/note") ]
    [Authorize]
    public class NotesController : ApiController
    {
        //private ApplicationDbContext db = new ApplicationDbContext();

        //Get all notes written by the user api/note/list
        [HttpGet, Route("list")]
        public HttpResponseMessage GetAllNotes()
        {
            var UserId = User.Identity.GetUserId();
            UserId.ToString();

            var db = new ApplicationDbContext();
            var notes = db.Notes;

            var listOfNotes = db.Notes.Where(x => x.UserId.Id.Contains("UserId"));

            return Request.CreateResponse(HttpStatusCode.OK, listOfNotes);
        }

    }
}