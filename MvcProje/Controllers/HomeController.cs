using BusinessLayer.Concrete;
using DataAccessLayer.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MvcProje.Controllers
{
    public class HomeController : Controller
    {
        HeadingManager hm = new HeadingManager(new EFHeadingDal());
        ContentManager cm = new ContentManager(new EFContentDal());
        WriterManager wm = new WriterManager(new EFWriterDal());
        MessageManager mm = new MessageManager(new EFMessageDal());

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult Test()
        {
            //SweetAlert
            return View();
        }
        [AllowAnonymous]
        public ActionResult HomePage()
        {
            ViewBag.heading = hm.GetList().Count();
            ViewBag.content = cm.GetList("").Count();
            ViewBag.writer = wm.GetList().Count();
            ViewBag.message = mm.GetList().Count();

            return View();
        }
	}
}