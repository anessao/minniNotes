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
    [RoutePrefix("api/school")]
    [Authorize]
    public class SchoolController : ApiController
    {
        [HttpPost, Route("add")]
        public HttpResponseMessage AddNewSchool(School requestedSchool)
        {
            var CurrentUserId = User.Identity.GetUserId();
            CurrentUserId.ToString();

            var db = new ApplicationDbContext();
            var newSchool = new School
            {
                Name = requestedSchool.Name,
                City = requestedSchool.City,
                State = requestedSchool.State,
                isActive = true,
                UserId = db.Users.Find(CurrentUserId)
            };

            db.Schools.Add(newSchool);
            db.SaveChanges();

            return Request.CreateResponse(HttpStatusCode.Created, newSchool);
        }

        //Get all notes written by the user api/note/list
        [HttpGet, Route("list")]
        public HttpResponseMessage GetAllSchoolsByUser()
        {
            var UserId = User.Identity.GetUserId();
            UserId.ToString();

            var db = new ApplicationDbContext();
            var schools = db.Decks;

            var listOfSchools = db.Schools.Where(x => x.UserId.Id.Contains(UserId));

            return Request.CreateResponse(HttpStatusCode.OK, listOfSchools);
        }
    }
}