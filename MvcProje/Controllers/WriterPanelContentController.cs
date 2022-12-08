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
    [Authorize(Roles = "W")]
    public class WriterPanelContentController : Controller
    {
        ContentManager cm = new ContentManager(new EFContentDal());
        WriterManager wm = new WriterManager(new EFWriterDal());

        public static string mail;
        public static int id;
        static int headingID;
        // GET: WriterPanelContent
        public ActionResult MyContent(string p)
        {
            p = (string)Session["WriterMail"];
            mail = p;
            var writer = wm.GetList().FirstOrDefault(x => x.WriterMail == p);
            ViewBag.name = writer.WriterName + " " + writer.WriterSurName;
            ViewBag.img = writer.WriterImage;
            id = writer.WriterID;
            var contentvalues = cm.GetListByWriter(id);
            return View(contentvalues);
        }
        [HttpGet]
        public ActionResult AddContent(int id)
        {
            var writer = wm.GetList().FirstOrDefault(x => x.WriterMail == mail);
            ViewBag.name = writer.WriterName + " " + writer.WriterSurName;
            ViewBag.img = writer.WriterImage;
            headingID = id;
            return View();
        }
        [HttpPost]
        public ActionResult AddContent(Content content)
        {
            content.ContentDate = DateTime.Parse(DateTime.Now.ToShortDateString());
            content.WriterID = id;
            content.ContentStatus = true;
            content.HeadingID = headingID;
            cm.ContentAdd(content);
            return RedirectToAction("MyContent");
        }
    }
}