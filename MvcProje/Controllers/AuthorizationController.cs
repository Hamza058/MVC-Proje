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
    public class AuthorizationController : Controller
    {
        AdminManager am = new AdminManager(new EFAdminDal());
        // GET: Authorization
        public ActionResult Index()
        {
            var adminvalues = am.GetList();
            return View(adminvalues);
        }
        [HttpGet]
        public ActionResult AddAdmin()
        {
            return View();
        }
        [HttpPost]
        public ActionResult AddAdmin(Admin admin)
        {
            am.AdminAdd(admin);
            return RedirectToAction("Index");
        }
        [HttpGet]
        public ActionResult EditAdmin(int id)
        {
            var adminvalue = am.GetByID(id);
            return View(adminvalue);
        }
        [HttpPost]
        public ActionResult EditAdmin(Admin admin)
        {
            am.AdminUpdate(admin);
            return RedirectToAction("Index");
        }
        public ActionResult StatusFalse(int id)
        {
            var adminID = am.GetByID(id);
            adminID.AdminStatus = false;
            am.AdminDelete(adminID);
            return RedirectToAction("Index");
        }
        public ActionResult StatusTrue(int id)
        {
            var adminID = am.GetByID(id);
            adminID.AdminStatus = true;
            am.AdminDelete(adminID);
            return RedirectToAction("Index");
        }
    }
}