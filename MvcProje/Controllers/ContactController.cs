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
        public ActionResult Index()
        {
            var contactValues = cm.GetList();
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
            ViewBag.value = cm.GetList().Count();
            ViewBag.Invalue = mm.GetListInbox().Count();
            ViewBag.Sendvalue = mm.GetListSendbox().Count();
            ViewBag.Notread = mm.GetListInbox().Where(x => !x.MessageStatus).Count();
            return PartialView();
        }
    }
}