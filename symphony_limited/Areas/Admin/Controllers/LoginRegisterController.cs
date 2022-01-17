using symphony_limited.FrameWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace symphony_limited.Areas.Admin.Controllers
{
    public class LoginRegisterController : Controller
    {
        symphony_limitedEntities db = new symphony_limitedEntities();
        // GET: Admin/Login
        public ActionResult Index()
        {

            return View(db.Logins.ToList());
        }
        public ActionResult Signup()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Signup(Login login)
        {
            if (db.Logins.Any(x => x.Email == login.Email))
            {
                ViewBag.Notification = " this account has aleardy exsted";
                return View();
            }
            else
            {
                db.Logins.Add(login);
                db.SaveChanges();

                Session["Id"] = login.Id.ToString();
                Session["Email"] = login.Email.ToString();
                return RedirectToAction("Index", "LoginRegister");
            }

        }
        public ActionResult Logout()
        {
            Session.Clear();
            return RedirectToAction("Index", "LoginRegister");
        }

        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(Login login)
        {
            var checkLogin = db.Logins.Where(x => x.Email.Equals(login.Email) && x.PassWord.Equals(login.PassWord)).FirstOrDefault();
            if (checkLogin != null)
            {
                Session["Id"] = login.Id.ToString();
                Session["Email"] = login.Email.ToString();
                return RedirectToAction("Index", "LoginRegister");
            }
            else
            {
                ViewBag.Notification = "Wrong Email or PassWord";
            }
            return View();
        }


        // GET: Admin/Logins/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Login login = db.Logins.Find(id);
            if (login == null)
            {
                return HttpNotFound();
            }
            return View(login);
        }

        // POST: Admin/Logins/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Login login = db.Logins.Find(id);
            db.Logins.Remove(login);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}