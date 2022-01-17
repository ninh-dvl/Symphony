using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using symphony_limited.FrameWork;

namespace symphony_limited.Areas.Admin.Controllers
{
    public class AcademiesController : Controller
    {
        private symphony_limitedEntities db = new symphony_limitedEntities();

        // GET: Admin/Academies
        public ActionResult Index()
        {
            //string a = Session["Id"].ToString();
            //if(Session["Id"] == null)
            //{
            //    return RedirectToAction("Login", "LoginRegister");
            //}
            return View(db.Academies.ToList());
        }

        // GET: Admin/Academies/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Academy academy = db.Academies.Find(id);
            if (academy == null)
            {
                return HttpNotFound();
            }
            return View(academy);
        }

        // GET: Admin/Academies/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Admin/Academies/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "AcademyId,PeopleManageme,Address,PhoneNumber,Email,Fanpage,ImgAcademy,description")] Academy academy, HttpPostedFileBase uploadAcademy)
        {
            if(ModelState.IsValid)
            {
                db.Academies.Add(academy);

                db.SaveChanges();

                if (uploadAcademy != null && uploadAcademy.ContentLength > 0)
                {
                    int id = int.Parse(db.Academies.ToList().Last().AcademyId.ToString());

                    string _FileName = "";
                    int index = uploadAcademy.FileName.IndexOf('.');
                    _FileName = "academy" + id.ToString() + "." + uploadAcademy.FileName.Substring(index + 1);
                    string _path = Path.Combine(Server.MapPath("~/Upload/Academy"), _FileName);
                    uploadAcademy.SaveAs(_path);

                    Academy unv = db.Academies.FirstOrDefault(x => x.AcademyId == id);
                    unv.ImgAcademy = _FileName;
                    db.SaveChanges();
                }
            }


            return RedirectToAction("Index");
        }

        // GET: Admin/Academies/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Academy academy = db.Academies.Find(id);
            if (academy == null)
            {
                return HttpNotFound();
            }
            return View(academy);
        }

        // POST: Admin/Academies/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "AcademyId,PeopleManageme,Address,PhoneNumber,Email,Fanpage,ImgAcademy,description")] Academy academy , HttpPostedFileBase uploadAcademy)
        {
            if (ModelState.IsValid)
            {
                db.Entry(academy).State = EntityState.Modified;

                if (uploadAcademy != null && uploadAcademy.ContentLength > 0)
                {
                    int id = academy.AcademyId;

                    string _FileName = "";
                    int index = uploadAcademy.FileName.IndexOf('.');
                    _FileName = "academy" + id.ToString() + "." + uploadAcademy.FileName.Substring(index + 1);
                    string _path = Path.Combine(Server.MapPath("~/Upload/Academy"), _FileName);
                    uploadAcademy.SaveAs(_path);
                    academy.ImgAcademy = _FileName;
                }
            }

            db.SaveChanges();

            return RedirectToAction("Index");
        }

        // GET: Admin/Academies/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Academy academy = db.Academies.Find(id);
            if (academy == null)
            {
                return HttpNotFound();
            }
            return View(academy);
        }

        // POST: Admin/Academies/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Academy academy = db.Academies.Find(id);
            db.Academies.Remove(academy);
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
