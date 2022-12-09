using BusinessLayer.Concrete;
using BusinessLayer.ValidationRules;
using DataAccessLayer.EntityFramework;
using EntityLayer.Concrete;
using FluentValidation.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MvcProje.Controllers
{
    [Authorize(Roles = "B,A")]
    public class WriterController : Controller
    {
        WriterManager wm = new WriterManager(new EFWriterDal());
        WriterValidator writerValidator = new WriterValidator();
        ContentManager cm = new ContentManager(new EFContentDal());

        public ActionResult Index()
        {
            var writerValues = wm.GetList();
            return View(writerValues);
        }
        
        [HttpGet]
        public ActionResult EditWriter(int id)
        {
            var writervalue = wm.GetByID(id);
            return View(writervalue);
        }
        [HttpPost]
        public ActionResult EditWriter(Writer p)
        {
            ValidationResult results = writerValidator.Validate(p);
            if (results.IsValid)
            {
                wm.WriterUpdate(p);
                return RedirectToAction("Index");
            }
            else
            {
                foreach (var item in results.Errors)
                {
                    ModelState.AddModelError(item.PropertyName, item.ErrorMessage);
                }
            }
            return View();
        }
        public ActionResult WriterContent(int id)
        {
            ViewBag.name = wm.GetByID(id).WriterName + " " + wm.GetByID(id).WriterSurName;
            var contentvalues = cm.GetListByWriter(id);
            return View(contentvalues);
        }
    }
}