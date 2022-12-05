using BusinessLayer.Concrete;
using BusinessLayer.ValidationRules;
using DataAccessLayer.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MvcProje.Controllers
{
    public class ContactController : Controller
    {
        ContactManager cm = new ContactManager(new EFContactDal());
        ContactValidator cv = new ContactValidator();
        MessageManager mm = new MessageManager(new EFMessageDal());
        // GET: Contact
        public ActionResult Index(string p="")
        {
            var contactValues = cm.GetList(p);
            if (!string.IsNullOrEmpty(p))
                contactValues = cm.GetList(p);
            return View(contactValues);
        }
        [HttpGet]
        public ActionResult GetContactDetails(int id)
        {
            var contactValue = cm.GetByID(id);
            return View(contactValue);
        }

        public PartialViewResult ContactPartial()
        {
            ViewBag.value = cm.GetList("").Count();
            ViewBag.Invalue = mm.GetListInbox("admin").Count();
            ViewBag.Sendvalue = mm.GetListSendbox("admin").Count();
            ViewBag.Notread = mm.GetListInbox("admin").Where(x => !x.MessageStatus).Count();
            return PartialView();
        }
    }
}