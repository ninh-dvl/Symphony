using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using PagedList;
using symphony_limited.FrameWork;

namespace symphony_limited.Areas.Frontend.Controllers
{
    public class CoursesController : Controller
    {
        private symphony_limitedEntities db = new symphony_limitedEntities();

        // GET: Frontend/Courses
        //public ActionResult Index(int courseid = 0)
        //{
        //    if(courseid == 0)
        //    {
        //        var courses = db.Courses.Include(c => c.Category).Include(c => c.Teacher);
        //        return View(courses.ToList());
        //    }
        //    else
        //    {
        //        var courses = db.Courses.Include(c => c.Category).Include(c => c.Teacher).Where(x => x.CourseId == courseid);
        //        return View(courses.ToList());
        //    }
        //}

        public ActionResult Index()
        {

            // using System.Threading; using System.Globalization;
            /*if (!String.IsNullOrEmpty(Request["locale"]))
            {
                Thread.CurrentThread.CurrentCulture = new CultureInfo(Request["locale"]);
                Thread.CurrentThread.CurrentUICulture = new CultureInfo(Request["locale"]);
            }*/

            var list = db.Courses.Include(c => c.Category).Include(c => c.Teacher);

            switch (Request["order"])
            {
                default:
                    list = (Request["sort"] == "desc") ? list.OrderByDescending(item => item.NameCourse)
                                                       : list.OrderBy(item => item.NameCourse);
                    break;
            }
            int PageNumber = String.IsNullOrEmpty(Request["pageNumber"]) ? 1 : Convert.ToInt32(Request["pageNumber"]);
            int PageSize = String.IsNullOrEmpty(Request["pageSize"]) ? 6 : Convert.ToInt32(Request["pageSize"]);

            return View(list.ToPagedList(PageNumber, PageSize));
        }
        // GET: Frontend/Courses/Details/5
        public ActionResult Details(int? id)
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


    }
}
