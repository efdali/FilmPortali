using FilmPortalı.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FilmPortalı.Areas.Amdin.Controllers
{
    [Authorize(Roles = "A")]
    public class CategoryController : Controller
    {
        FilmPortaliEntities db = new FilmPortaliEntities();
        // GET: Amdin/Category
        public ActionResult Index()
        {
            List<Categories> cat = db.Categories.ToList(); 
            return View(cat);
        }

        public ActionResult Add()
        {
            return View("CategoryForm");
        }

        public ActionResult Update(int id)
        {
            var model = db.Categories.Find(id);
            if (model == null) return HttpNotFound();
            return View("CategoryForm", model);
        }
        
        public ActionResult Delete(int id)
        {
            var model = db.Categories.Find(id);
            if (model == null) return HttpNotFound();
            db.Categories.Remove(model);
            db.SaveChanges();
            return Json(new {statu=1});
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Save(Categories cat)
        {
            if (cat.CId == 0) db.Categories.Add(cat);
            else db.Entry(cat).State = System.Data.Entity.EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}