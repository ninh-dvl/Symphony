using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using PagedList;
using symphony_limited.FrameWork;

namespace symphony_limited.Areas.Admin.Controllers
{
    public class CoursesController : Controller
    {
        private symphony_limitedEntities db = new symphony_limitedEntities();

        // GET: Admin/Courses
        //public ActionResult Index()
        //{
        //    var courses = db.Courses.Include(c => c.Category).Include(c => c.Teacher);
        //    return View(courses.ToList());
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
                //case nameof(Tên_Lớp_Mô_Hình.TÊN_CỘT_SẮP_XẾP_2):
                //    list = (Request["sort"] == "desc") ? list.OrderByDescending(item => item.TÊN_CỘT_SẮP_XẾP_2)
                //                                       : list.OrderBy(item => item.TÊN_CỘT_SẮP_XẾP_2);
                //    break;

                //case nameof(Tên_Lớp_Mô_Hình.TÊN_CỘT_SẮP_XẾP_3):
                //    list = (Request["sort"] == "desc") ? list.OrderByDescending(item => item.TÊN_CỘT_SẮP_XẾP_3)
                //                                       : list.OrderBy(item => item.TÊN_CỘT_SẮP_XẾP_3);
                //    break;
                default:
                    list = (Request["sort"] == "desc") ? list.OrderByDescending(item => item.NameCourse)
                                                       : list.OrderBy(item => item.NameCourse);
                    break;
            }

            if (!String.IsNullOrEmpty(Request["search"]))
            {
                string search = Request["search"];
                list = list.Where(item => item.NameCourse.Contains(search));
            }

            int PageNumber = String.IsNullOrEmpty(Request["pageNumber"]) ? 1 : Convert.ToInt32(Request["pageNumber"]);
            int PageSize = String.IsNullOrEmpty(Request["pageSize"]) ? 3 : Convert.ToInt32(Request["pageSize"]);

            return View(list.ToPagedList(PageNumber, PageSize));
        }

        // GET: Admin/Courses/Details/5
        public ActionResult Details(int? id)
        {
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

        // GET: Admin/Courses/Create
        public ActionResult Create()
        {
            ViewBag.CategoryId = new SelectList(db.Categories, "CategoryId", "NameCategory");
            ViewBag.TeacherId = new SelectList(db.Teachers, "TeacherId", "NameTeachear");
            return View();
        }

        // POST: Admin/Courses/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "CourseId,NameCourse,Price,Status,Description,Numberofsession,NameSubject,TeacherId,CategoryId,Img")] Course course, HttpPostedFileBase uploadhinh)
        {
            if (ModelState.IsValid)
            {
                db.Courses.Add(course);

                db.SaveChanges();

                if (uploadhinh != null && uploadhinh.ContentLength > 0)
                {
                    int id = int.Parse(db.Courses.ToList().Last().CourseId.ToString());

                    string _FileName = "";
                    int index = uploadhinh.FileName.IndexOf('.');
                    _FileName = "course" + id.ToString() + "." + uploadhinh.FileName.Substring(index + 1);
                    string _path = Path.Combine(Server.MapPath("~/Upload/Course"), _FileName);
                    uploadhinh.SaveAs(_path);

                    Course unv = db.Courses.FirstOrDefault(x => x.CourseId == id);
                    unv.Img = _FileName;
                    db.SaveChanges();
                }
            }

            ViewBag.CategoryId = new SelectList(db.Categories, "CategoryId", "NameCategory", course.CategoryId);
            ViewBag.TeacherId = new SelectList(db.Teachers, "TeacherId", "NameTeachear", course.TeacherId);
            return RedirectToAction("Index");
        }
        // GET: Admin/Courses/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Course course = db.Courses.Find(id);
            if (course == null)
            {
                return HttpNotFound();
            }
            ViewBag.CategoryId = new SelectList(db.Categories, "CategoryId", "NameCategory", course.CategoryId);
            ViewBag.TeacherId = new SelectList(db.Teachers, "TeacherId", "NameTeachear", course.TeacherId);
            return View(course);
        }

        // POST: Admin/Courses/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "CourseId,NameCourse,Price,Status,Description,Numberofsession,TeacherId,Img,CategoryId")] Course course, HttpPostedFileBase uploadhinh)
        {
            if (ModelState.IsValid)
            {
                db.Entry(course).State = EntityState.Modified;

                if (uploadhinh != null && uploadhinh.ContentLength > 0)
                {
                    int id = course.CourseId;

                    string _FileName = "";
                    int index = uploadhinh.FileName.IndexOf('.');
                    _FileName = "teacher" + id.ToString() + "." + uploadhinh.FileName.Substring(index + 1);
                    string _path = Path.Combine(Server.MapPath("~/Upload/Course"), _FileName);
                    uploadhinh.SaveAs(_path);
                    course.Img = _FileName;
                }
            }

            db.SaveChanges();
            ViewBag.CategoryId = new SelectList(db.Categories, "CategoryId", "NameCategory", course.CategoryId);
            ViewBag.TeacherId = new SelectList(db.Teachers, "TeacherId", "NameTeachear", course.TeacherId);
            return RedirectToAction("Index");


        }

        // GET: Admin/Courses/Delete/5
        public ActionResult Delete(int? id)
        {
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

        // POST: Admin/Courses/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Course course = db.Courses.Find(id);
            db.Courses.Remove(course);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
