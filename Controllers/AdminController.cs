using KurumsalWebProjesi.Models;
using KurumsalWebProjesi.Models.DataContext;
using KurumsalWebProjesi.Models.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace KurumsalWebProjesi.Controllers
{
    public class AdminController : Controller
    {

        WebDBContext db = new WebDBContext();
        // GET: Admin
        public ActionResult Index()
        {

            var query = db.Category.ToList();
            return View(query);
        }

        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Login(Admin admin)
        {
            var login = db.Admin.Where(x => x.Email == admin.Email).SingleOrDefault();
            if(login.Email==admin.Email && login.Password==admin.Password)
            {
                Session["adminid"] = login.AdminId;
                Session["email"] = login.Email;
                return RedirectToAction("Index", "Admin");

            }
            ViewBag.Uyari = "kullanici adi ya da şifre yanlış";
            return View(admin);

        }

        public ActionResult Logout()
        {
            Session["adminid"] = null;
            Session["email"] = null;
            Session.Abandon();

            return RedirectToAction("Login", "Admin");

        }
    }
}