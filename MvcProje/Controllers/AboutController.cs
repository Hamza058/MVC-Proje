using BusinessLayer.Concrete;
using DataAccessLayer.EntityFramework;
using EntityLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MvcProje.Controllers
{
    [Authorize(Roles = "B,A")]
    public class AboutController : Controller
    {
        AboutManager am = new AboutManager(new EFAboutDal());
        // GET: About
        public ActionResult Index()
        {
            var aboutValues = am.GetList();
            return View(aboutValues);
        }

        [HttpGet]
        public ActionResult AddAbout()
        {
            return View();
        }
        [HttpPost]
        public ActionResult AddAbout(About about)
        {
            am.AboutAdd(about);
            return RedirectToAction("Index");
        }
        public PartialViewResult AboutPartial()
        {
            return PartialView();
        }

        public ActionResult AboutTrue(int id)
        {
            var about = am.GetByID(id);
            about.AboutStatus = true;
            am.AboutUpdate(about);
            return RedirectToAction("Index");
        }
        public ActionResult AboutFalse(int id)
        {
            var about = am.GetByID(id);
            about.AboutStatus = false;
            am.AboutDelete(about);
            return RedirectToAction("Index");
        }
    }
}