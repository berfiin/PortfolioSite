using KurumsalWebProjesi.Models.DataContext;
using KurumsalWebProjesi.Models.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace KurumsalWebProjesi.Controllers
{
    public class AboutMeController : Controller
    {


        WebDBContext db = new WebDBContext();
        // GET: AboutMe
        public ActionResult Index()
        {
            var h = db.AboutMe.ToList();

            return View(h);
        }


        public ActionResult Edit(int id)
        {
            var h = db.AboutMe.Where(x => x.AboutMeId == id).FirstOrDefault();

            return View(h);

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult Edit(int id, AboutMe h)
        {


            if (ModelState.IsValid)
            {
                var aboutme = db.AboutMe.Where(x => x.AboutMeId == id).SingleOrDefault();

                aboutme.Description = h.Description;
                db.SaveChanges();
                return RedirectToAction("Index");


            }
            return View(h);


        }

    }
}