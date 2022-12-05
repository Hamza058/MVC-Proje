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
    public class WriterPanelMessageController : Controller
    {
        MessageManager mm = new MessageManager(new EFMessageDal());
        MessageValidator mv = new MessageValidator();
        static string p;
        // GET: WriterPanelMessage
        public ActionResult Inbox()
        {
            p = (string)Session["WriterMail"];
            var messagelist = mm.GetListInbox(p);
            return View(messagelist);
        }
        public ActionResult Sendbox()
        {
            var messageList = mm.GetListSendbox(p);
            return View(messageList);
        }
        [HttpGet]
        public ActionResult GetInBoxMessageDetails(int id)
        {
            var value = mm.GetByID(id);
            value.MessageStatus = true;
            mm.MessageUpdate(value);
            return View(value);
        }
        [HttpGet]
        public ActionResult GetSendBoxMessageDetails(int id)
        {
            var value = mm.GetByID(id);
            return View(value);
        }

        public ActionResult GetMessageNotRead()
        {
            var value = mm.GetListInbox(p).Where(x => !x.MessageStatus);
            return View(value);
        }
        public PartialViewResult MessageListMenu()
        {
            ViewBag.Invalue = mm.GetListInbox(p).Count();
            ViewBag.Sendvalue = mm.GetListSendbox(p).Count();
            ViewBag.Notread = mm.GetListInbox(p).Where(x => !x.MessageStatus).Count();
            return PartialView();
        }
        [HttpGet]
        public ActionResult NewMessage()
        {
            return View();
        }
        [HttpPost]
        public ActionResult NewMessage(Message message)
        {
            ValidationResult results = mv.Validate(message);
            if (results.IsValid)
            {
                message.SenderMail = p;
                message.MessageDate = DateTime.Parse(DateTime.Now.ToShortDateString());
                mm.MessageAdd(message);
                return RedirectToAction("SendBox");
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
    }
}