using BusinessLayer.Concrete;
using DataAccessLayer.EntityFramework;
using EntityLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
            foreach (var item in adminvalues)
            {
                byte[] veri1 = Convert.FromBase64String(item.AdminUserName);
                string name = ASCIIEncoding.ASCII.GetString(veri1);
                adminvalues.FirstOrDefault(x => x.AdminUserName == item.AdminUserName).AdminUserName = name;
            }
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
            byte[] username = ASCIIEncoding.ASCII.GetBytes(admin.AdminUserName);
            admin.AdminUserName = Convert.ToBase64String(username);
            byte[] password = ASCIIEncoding.ASCII.GetBytes(admin.AdminPassword);
            admin.AdminPassword = Convert.ToBase64String(password);
            admin.AdminUserRole = "A";

            am.AdminAdd(admin);
            return RedirectToAction("Index");
        }
        [HttpGet]
        public ActionResult EditAdmin(int id)
        {
            var adminvalue = am.GetByID(id);

            byte[] veri1 = Convert.FromBase64String(adminvalue.AdminUserName);
            string name = ASCIIEncoding.ASCII.GetString(veri1);
            byte[] veri2 = Convert.FromBase64String(adminvalue.AdminPassword);
            string password = ASCIIEncoding.ASCII.GetString(veri2);

            adminvalue.AdminUserName = name;
            adminvalue.AdminPassword = password;
            return View(adminvalue);
        }
        [HttpPost]
        public ActionResult EditAdmin(Admin admin)
        {
            byte[] username = ASCIIEncoding.ASCII.GetBytes(admin.AdminUserName);
            admin.AdminUserName = Convert.ToBase64String(username);
            byte[] password = ASCIIEncoding.ASCII.GetBytes(admin.AdminPassword);
            admin.AdminPassword = Convert.ToBase64String(password);
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