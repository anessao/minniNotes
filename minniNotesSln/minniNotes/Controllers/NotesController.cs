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

            var listOfNotes = db.Notes.Where(x => x.UserId.Id.Contains(UserId));

            return Request.CreateResponse(HttpStatusCode.OK, listOfNotes);
        }
        //Get all notes written by the user api/note/list
        [HttpPost, Route("add")]
        public HttpResponseMessage AddNewNote(NoteToPost noteItem)
        {
            var CurrentUserId = User.Identity.GetUserId();
            CurrentUserId.ToString();

            var db = new ApplicationDbContext();
            var newNote = new Note
            {
                Title = noteItem.Title,
                UserId = db.Users.Find(CurrentUserId),
                DateCreated = DateTime.Now,
                DateLastEdited = DateTime.Now,
                CardDeck = null,
                School = db.Schools.Find(noteItem.SchoolId),
                NoteText = noteItem.NoteText,
                EnrolledClass = db.Classes.Find(noteItem.ClassId)
            };

            db.Notes.Add(newNote);
            db.SaveChanges();

            return Request.CreateResponse(HttpStatusCode.Created, newNote);
        }

        [HttpGet, Route("view/{noteId}")]
        public HttpResponseMessage GetNoteById(int noteId)
        {
            var db = new ApplicationDbContext();

            var SelectedNote = db.Notes.Where(n => n.Id == noteId).FirstOrDefault();

            return Request.CreateResponse(HttpStatusCode.OK, SelectedNote);
        }

        [HttpPut, Route("edit/{noteId}")]
        public HttpResponseMessage UpdateNote(int noteId, NoteToPost updatedNote)
        {
            var db = new ApplicationDbContext();
            var note = db.Notes.Where(x => x.Id.Equals(noteId)).FirstOrDefault();

            note.Title = updatedNote.Title;
            note.DateCreated = note.DateCreated;
            note.DateLastEdited = DateTime.Now;
            note.CardDeck = null;
            note.School = db.Schools.Find(updatedNote.SchoolId);
            note.NoteText = updatedNote.NoteText;
            note.EnrolledClass = db.Classes.Find(updatedNote.ClassId);

            db.SaveChanges();

            return Request.CreateResponse(HttpStatusCode.OK, note);
        }

        [HttpDelete, Route("remove/{noteid}")]
        public HttpResponseMessage DeleteNote(int noteid)
        {
            var db = new ApplicationDbContext();
            
            var note = db.Notes.Where(x => x.Id.Equals(noteid)).FirstOrDefault();

            db.Notes.Remove(note);
            db.SaveChanges();

            return Request.CreateResponse(HttpStatusCode.Accepted);
        }
    }
}