using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using symphony_limited.FrameWork;

namespace symphony_limited.Areas.Frontend.Controllers
{
    public class MenuCategoryController : Controller
    {
        // GET: Frontend/MenuCategory
        public ActionResult Index()
        {
            using (symphony_limitedEntities db = new symphony_limitedEntities())
            {
                var category = db.Categories.ToList();
                Hashtable namecategory = new Hashtable();
                foreach(var item in category)
                {
                    namecategory.Add(item.CategoryId, item.NameCategory);
                }
                ViewBag.NameCategory = namecategory;
                return PartialView("Index");
            }
        }
    }
}