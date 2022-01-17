using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using symphony_limited.FrameWork;

namespace symphony_limited.Areas.Frontend.Controllers
{
    public class HomeController : Controller
    {
        private symphony_limitedEntities db = new symphony_limitedEntities();
        // GET: Frontend/Home
        public ActionResult Index()
        {
            var courses = db.Courses.Include(c => c.Category).Include(c => c.Teacher);
            return View(courses.ToList());


        }

        public ActionResult About()
        {
            return View();
        }

        public ActionResult Contact()
        {
            return View();
        }

        public ActionResult CourseDetail(int? id)
        {
            var courses = db.Courses.Include(c => c.Category).Include(c => c.Teacher);
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Course course = db.Courses.Find(id);
            if (course == null)
            {
                return HttpNotFound();
            }
            return View(course);
        }

        public ActionResult Courses()
        {
            return View();
        }
        public ActionResult Faq()
        {
            return View();
        }
        public ActionResult Examination()
        {
            return View();
        }
    }
}