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
    public class MessageController : Controller
    {
        MessageManager mm = new MessageManager(new EFMessageDal());
        MessageValidator mv = new MessageValidator();

        // GET: Message
        [Authorize]
        public ActionResult Inbox()
        {
            var messagelist = mm.GetListInbox("admin");
            return View(messagelist);
        }
        public ActionResult Sendbox()
        {
            var messageList = mm.GetListSendbox("admin");
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