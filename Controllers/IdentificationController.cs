using KurumsalWebProjesi.Models.DataContext;
using KurumsalWebProjesi.Models.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;

namespace KurumsalWebProjesi.Controllers
{
    public class IdentificationController : Controller
    {
        WebDBContext db = new WebDBContext(); //veritabanımızın örneğini aldık.
        // GET: Idendification
        public ActionResult Index()
        {
            return View(db.Identification.ToList()); //kimlik tablomuzun listesini view a attık.
        }

      
        public ActionResult Edit(int id)
        {
            var ıdentification = db.Identification.Where(x => x.IdentificationId == id).SingleOrDefault();
            return View(ıdentification);
        }

       
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult Edit(int id, Identification ıdentification, HttpPostedFileBase LogoURL)
        {
            if (ModelState.IsValid) //modelimiz doğrulandıysa işlemlere geç
            {
                var ı = db.Identification.Where(x => x.IdentificationId == id).SingleOrDefault();

                if(LogoURL != null)
                {

                    if(System.IO.File.Exists(Server.MapPath(ı.LogoURL))) //daha önce kaydettiğimiz bir dosya var mı yok mu onun kontrolünü saglıyoruz.
                        
                    {

                        System.IO.File.Delete(Server.MapPath(ı.LogoURL));

                    }
                    WebImage img = new WebImage(LogoURL.InputStream);
                    FileInfo imginfo = new FileInfo(LogoURL.FileName); //bize gelen dosyanın bilgilerini aldık.

                    string logoname = LogoURL.FileName + imginfo.Extension;
                    img.Resize(300,300);
                    img.Save("~/Uploads/Identification/" + logoname);

                    ı.LogoURL = "/Uploads/Identification/" + logoname;

                }

                ı.Title = ıdentification.Title;
                ı.Keywords = ıdentification.Keywords;
                ı.Description = ıdentification.Description;
                ı.Degree = ıdentification.Degree;


                db.SaveChanges();

                return RedirectToAction("Index"); //index e gönderdik


            }

            return View(ıdentification);

        }

       
    }
}
