using KurumsalWebProjesi.Models.DataContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;
using PagedList;
using PagedList.Mvc;
using KurumsalWebProjesi.Models.Model;

namespace KurumsalWebProjesi.Controllers
{
    public class HomeController : Controller
    {



        private WebDBContext db = new WebDBContext();

        // GET: Home
        public ActionResult Index()
        {
            ViewBag.Services = db.Sevices.ToList().OrderByDescending(x => x.ServiceId);
                        
            return View();
        }
        public ActionResult SliderPartial()
        {
            return View(db.Slider.ToList().OrderByDescending(x => x.SliderId));
        }
        public ActionResult ServicePartial()
        {
            return View(db.Sevices.ToList());
        }

        public ActionResult AboutMe()
        {
           
            return View(db.AboutMe.SingleOrDefault());
        }

        public ActionResult Services()
        {
            return View(db.Sevices.ToList().OrderByDescending(x=>x.ServiceId));
        }
        public ActionResult Contact ()
        {
            return View(db.Contact.SingleOrDefault());
        }
        [HttpPost]
        public ActionResult Contact(string firstnamelastname = null,string email=null,string description=null,string message=null)
        {
            if(firstnamelastname != null && email != null)
            {
                WebMail.SmtpServer = "smtp.gmail.com";
                WebMail.EnableSsl = true;
                WebMail.UserName = "berfin.oti@gmail.com";
                WebMail.Password = "1234";
                WebMail.SmtpPort = 587;
                WebMail.Send("berfin.oti@gmail.com", description, email + "-" + message);
                ViewBag.Uyari = "Yout message has been sent succesfully!";

            }
            else
            {
                ViewBag.Uyari = "Error. Please try again.";
            }
            return View();
        }
        public ActionResult Blog(int page = 1)
        {

            return View(db.Blog.Include("Category").OrderByDescending(x=>x.BlogId).ToPagedList(page,5));

        }
        public ActionResult BlogDetail(int id)
        {
            var b = db.Blog.Include("Category").Where(x => x.BlogId == id).SingleOrDefault();

            return View(b);

        }
        public JsonResult Comment(string firstnamelastname, string email, string _content, int blogıd)
        {
            if(_content == null)
            {
                return Json(true, JsonRequestBehavior.AllowGet);
            }

            db.Comment.Add(new Comment { FirstNameLastName = firstnamelastname, Email = email, _Content = _content, BlogId = blogıd, Confirmation=false});
            db.SaveChanges();
            return Json(false, JsonRequestBehavior.AllowGet);
        }
        public ActionResult BlogCategoryPartial()
        {
           
            return PartialView(db.Category.Include("Blogs").ToList().OrderBy(x=>x.CategoryName));
        }
        public ActionResult BlogSavePartial()
        {

            return PartialView(db.Blog.ToList().OrderBy(x => x.BlogId));
        }
        public ActionResult FooterPartial()
        {
            ViewBag.Services = db.Sevices.ToList().OrderByDescending(x => x.ServiceId);

            ViewBag.Contact = db.Contact.SingleOrDefault();

            ViewBag.Blog = db.Blog.ToList().OrderByDescending(x => x.BlogId);
            return PartialView();
        }


    }
}