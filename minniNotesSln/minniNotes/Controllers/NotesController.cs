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
        public HttpResponseMessage AddNewNote(Note noteItem)
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
                School = noteItem.School,
                NoteText = noteItem.NoteText,
                EnrolledClass = noteItem.EnrolledClass
            };

            db.Notes.Add(newNote);
            db.SaveChanges();

            return Request.CreateResponse(HttpStatusCode.Created, newNote);
        }

        [HttpGet, Route("view/{noteId}")]
        public HttpResponseMessage GetNoteById(int noteId)
        {
            var db = new ApplicationDbContext();

            var SelectedNote = db.Notes.Where(n => n.Id == noteId)
                .Select(note => new Note
                {
                    Title = note.Title,
                    DateCreated = note.DateCreated,
                    DateLastEdited = note.DateLastEdited,
                    CardDeck = note.CardDeck,
                    School = note.School,
                    NoteText = note.NoteText,
                    EnrolledClass = note.EnrolledClass
                });

            return Request.CreateResponse(HttpStatusCode.OK, SelectedNote);
        }

        [HttpPut, Route("edit/{noteId}")]
        public HttpResponseMessage UpdateNote(int noteId, Note updatedNote)
        {
            var db = new ApplicationDbContext();

            var SelectedNote = db.Notes.Where(n => n.Id == noteId)
                .Select(note => new Note
                {
                    Title = updatedNote.Title,
                    DateCreated = note.DateCreated,
                    DateLastEdited = updatedNote.DateLastEdited,
                    CardDeck = updatedNote.CardDeck,
                    School = updatedNote.School,
                    NoteText = updatedNote.NoteText,
                    EnrolledClass = updatedNote.EnrolledClass
                }).FirstOrDefault();

            db.SaveChanges();

            return Request.CreateResponse(HttpStatusCode.OK, SelectedNote);
        }
    }
}