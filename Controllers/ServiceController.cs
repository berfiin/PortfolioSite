using KurumsalWebProjesi.Models.DataContext;
using KurumsalWebProjesi.Models.Model;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;

namespace KurumsalWebProjesi.Controllers
{
    public class ServiceController : Controller
    {


       private  WebDBContext db = new WebDBContext();  //veritabanına erişim sağladık
        // GET: Service
        public ActionResult Index()
        {

            return View(db.Sevices.ToList());
        }
        public ActionResult Create()
        {

            return View();


        }
        [HttpPost]
       
        [ValidateInput(false)]
        public ActionResult Create(Service sevice, HttpPostedFileBase ImageURL)
        {

            
            if (ModelState.IsValid)
            {
                if (ImageURL != null)
                {

                  
                    WebImage img = new WebImage(ImageURL.InputStream);
                    FileInfo imginfo = new FileInfo(ImageURL.FileName); //bize gelen dosyanın bilgilerini aldık.

                    string logoname = Guid.NewGuid().ToString() + imginfo.Extension;
                    img.Resize(500, 500);
                    img.Save("~/Uploads/Service/" + logoname);

                    sevice.ImageURL = "/Uploads/Service/" + logoname;

                }
                db.Sevices.Add(sevice);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(sevice);


        }
        public ActionResult Edit(int? id)
        {
             if(id == null)
            {
                ViewBag.Uyari = "Güncellenicek hizmet bulunamadı";
            }

            var service = db.Sevices.Find(id);
            if(service == null)
            {
                return HttpNotFound();
            }


            return View(service);
        }
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Edit(int? id, Service service, HttpPostedFileBase ImageURL)
        {
            if (ModelState.IsValid)
            {
                var h = db.Sevices.Where(x => x.ServiceId == id).SingleOrDefault();

                if (ImageURL==null)
                {

                    if (System.IO.File.Exists(Server.MapPath(h.ImageURL))) //daha önce kaydettiğimiz bir dosya var mı yok mu onun kontrolünü saglıyoruz.

                    {

                        System.IO.File.Delete(Server.MapPath(h.ImageURL));

                    }
                    WebImage img = new WebImage(ImageURL.InputStream);
                    FileInfo imginfo = new FileInfo(ImageURL.FileName); //bize gelen dosyanın bilgilerini aldık.

                    string serviceName =Guid.NewGuid().ToString()  + imginfo.Extension;
                    img.Resize(300, 300);
                    img.Save("~/Uploads/Identification/" + serviceName);

                    h.ImageURL = "/Uploads/Identification/" + serviceName;
                }

                h.Title = service.Title;
                h.Description = service.Description;
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View();
        }
        public ActionResult Delete(int? id)
        {
            if(id==null)
            {
                return HttpNotFound();
            }

            var h = db.Sevices.Find(id);
            if(h ==null)
            {
                return HttpNotFound();
            }

            db.Sevices.Remove(h);
            db.SaveChanges();



            return RedirectToAction("Index");
        }

        }
    }