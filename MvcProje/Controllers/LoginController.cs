using BusinessLayer.Concrete;
using DataAccessLayer.Concrete;
using DataAccessLayer.EntityFramework;
using EntityLayer.Concrete;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using System.Net.Mail;

namespace MvcProje.Controllers
{
    [AllowAnonymous]
    public class LoginController : Controller
    {
        AdminManager am = new AdminManager(new EFAdminDal());
        WriterManager wm = new WriterManager(new EFWriterDal());

        // GET: Login
        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Index(Admin admin)
        {
            byte[] username = ASCIIEncoding.ASCII.GetBytes(admin.AdminUserName);
            admin.AdminUserName = Convert.ToBase64String(username);
            byte[] password = ASCIIEncoding.ASCII.GetBytes(admin.AdminPassword);
            admin.AdminPassword = Convert.ToBase64String(password);

            var admins = am.GetList();
            var adminuserinfo = admins.FirstOrDefault(x => x.AdminUserName == admin.AdminUserName && x.AdminPassword == admin.AdminPassword);

            if (adminuserinfo != null)
            {
                if (adminuserinfo.AdminStatus == true)
                {
                    FormsAuthentication.SetAuthCookie(adminuserinfo.AdminUserName, false);
                    Session["AdminUserName"] = adminuserinfo.AdminUserName;
                    return RedirectToAction("Index", "Contact");
                }
                else
                {
                    ViewBag.AdminMessage = "Admin durumunuz onaylanmamıştır. !!!";
                    return View();
                }
            }
            else
            {
                ViewBag.AdminMessage = "Kullanıcı bilgileriniz yanlış !!!";
                return View();
            }
        }
        [HttpGet]
        public ActionResult WriterLogin()
        {
            return View();
        }
        [HttpPost]
        public ActionResult WriterLogin(Writer writer)
        {
            var writers = wm.GetList();
            var writeruserinfo = writers.FirstOrDefault(x => x.WriterMail == writer.WriterMail &&
            x.WriterPassword == writer.WriterPassword && x.WriterStatus);

            if (writeruserinfo != null)
            {
                if (writeruserinfo.WriterStatus == true)
                {
                    FormsAuthentication.SetAuthCookie(writeruserinfo.WriterMail, false);
                    Session["WriterMail"] = writeruserinfo.WriterMail;
                    return RedirectToAction("MyContent", "WriterPanelContent");
                }  
                else
                {
                    ViewBag.AdminMessage = "Kullanıcı bilgileriniz onaylanmamıştır. !!!";
                    return View();
                }
            }
            else
            {
                ViewBag.WriterMessage = "Hatalı Kullancı Adı veya Şifre";
                return View();
            }
        }
        [HttpGet]
        public ActionResult WriterAdd()
        {
            return View();
        }
        [HttpPost]
        public ActionResult WriterAdd(Writer writer, HttpPostedFileBase WriterImage)
        {
            try
            {
                MailAddress m = new MailAddress(writer.WriterMail);
                if (WriterImage.ContentLength > 0)
                {
                    var fileName = Path.GetFileName(WriterImage.FileName);
                    var path = Path.Combine(Server.MapPath("~/image"), fileName);
                    WriterImage.SaveAs(path);
                    writer.WriterImage = fileName;
                    writer.UserRole = "W";
                    wm.WriterAdd(writer);
                    return RedirectToAction("WriterLogin", "Login");
                }
                else
                {
                    TempData["msg"] = "<script>alert('Lütfen Seçtiğiniz Resmi kontrol ediniz');</script>";
                    return RedirectToAction("WriterAdd", "Login");
                }
            }
            catch (Exception)
            {
                //return Content("<script language='javascript' type='text/javascript'>alert('Geçerli bir mail adresi giriniz !!!');</script>");
                TempData["msg"] = "<script>alert('Geçerli bir mail adresi giriniz');</script>";
                return RedirectToAction("WriterAdd", "Login");
            }
        }
        public ActionResult LogOut()
        {
            FormsAuthentication.SignOut();
            Session.Abandon();
            return RedirectToAction("WriterLogin", "Login");
        }
    }
}