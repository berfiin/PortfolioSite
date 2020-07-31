

using KurumsalWebProjesi.Models.DataContext;
using KurumsalWebProjesi.Models.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;

namespace KurumsalWeb.Controllers
{
    public class BlogController : Controller
    {
        private WebDBContext db = new WebDBContext();
        // GET: Blog

        public ActionResult Index()
        {
            db.Configuration.LazyLoadingEnabled = false;
            return View(db.Blog.Include("Category").ToList().OrderByDescending(x => x.BlogId));
        }
        public ActionResult Create()
        {
            ViewBag.CategoryId = new SelectList(db.Category, "CategoryId", "CategoryName");
            return View();
        }
        [HttpPost]
        [ValidateInput(false)]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Blog blog, HttpPostedFileBase ResimURL)
        {

            if (ResimURL != null)
            {
                WebImage img = new WebImage(ResimURL.InputStream);
                FileInfo imginfo = new FileInfo(ResimURL.FileName);

                string blogimgname = Guid.NewGuid().ToString() + imginfo.Extension;
                img.Resize(600, 400);
                img.Save("~/Uploads/Blog/" + blogimgname);

                blog.ImageURL = "/Uploads/Blog/" + blogimgname;
            }
            db.Blog.Add(blog);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult Edit(int id)
        {
            if (id == null)
            {
                return HttpNotFound();
            }
            var b = db.Blog.Where(x => x.BlogId == id).SingleOrDefault();
            if (b == null)
            {
                return HttpNotFound();
            }
            ViewBag.KategoriId = new SelectList(db.Category, "CategoryId", "CategoryName", b.CategoryId);
            return View(b);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]

        public ActionResult Edit(int id, Blog blog, HttpPostedFileBase ResimURL)
        {
            if (ModelState.IsValid)
            {
                var b = db.Blog.Where(x => x.BlogId == id).SingleOrDefault();
                if (ResimURL != null)
                {
                    if (System.IO.File.Exists(Server.MapPath(b.ImageURL)))
                    {
                        System.IO.File.Delete(Server.MapPath(b.ImageURL));
                    }
                    WebImage img = new WebImage(ResimURL.InputStream);
                    FileInfo imginfo = new FileInfo(ResimURL.FileName);

                    string blogimgname = Guid.NewGuid().ToString() + imginfo.Extension;
                    img.Resize(600, 400);
                    img.Save("~/Uploads/Blog/" + blogimgname);

                    b.ImageURL = "/Uploads/Blog/" + blogimgname;
                }
                b.Title = blog.Title;
                b._Content = blog._Content;
                b.CategoryId = blog.CategoryId;
                db.SaveChanges();
                return RedirectToAction("Index");

            }
            return View(blog);
        }

        [HttpPost]
        public ActionResult Delete(int id)
        {
            var b = db.Blog.Find(id);
            if (b == null)
            {
                return HttpNotFound();
            }
            if (System.IO.File.Exists(Server.MapPath(b.ImageURL)))
            {
                System.IO.File.Delete(Server.MapPath(b.ImageURL));
            }
            db.Blog.Remove(b);
            db.SaveChanges();

            return RedirectToAction("Index");
        }
    }
}
       
        
 