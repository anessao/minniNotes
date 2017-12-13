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
    [RoutePrefix("api/class")]
    [Authorize]
    public class EnrolledClassController : ApiController
    {
        [HttpPost, Route("add")]
        public HttpResponseMessage AddNewClass(EnrolledClassToCreate requestedClass)
        {
            var CurrentUserId = User.Identity.GetUserId();
            CurrentUserId.ToString();

            var db = new ApplicationDbContext();
            var newClass = new EnrolledClass
            {
                Name = requestedClass.Name,
                StartDate = requestedClass.StartDate,
                EndDate = requestedClass.EndDate,
                UserId = db.Users.Find(CurrentUserId),
                School = db.Schools.Find(requestedClass.SchoolId),
                Notes = null
            };

            db.Classes.Add(newClass);
            db.SaveChanges();

            return Request.CreateResponse(HttpStatusCode.Created, newClass);
        }

        //Get all notes written by the user api/note/list
        [HttpGet, Route("list")]
        public HttpResponseMessage GetAllClasses()
        {
            var UserId = User.Identity.GetUserId();
            UserId.ToString();

            var db = new ApplicationDbContext();
            var classes = db.Classes;

            var listOfClasses = db.Classes.Where(x => x.UserId.Id.Contains(UserId));

            return Request.CreateResponse(HttpStatusCode.OK, listOfClasses);
        }
    }
}