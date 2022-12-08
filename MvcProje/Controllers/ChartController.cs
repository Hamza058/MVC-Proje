using BusinessLayer.Concrete;
using DataAccessLayer.EntityFramework;
using MvcProje.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MvcProje.Controllers
{
    [Authorize(Roles = "B,A")]
    public class ChartController : Controller
    {
        CategoryManager cm = new CategoryManager(new EFCategoryDal());
        HeadingManager hm = new HeadingManager(new EFHeadingDal());

        // GET: Chart
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult CategoryChart()
        {
            return Json(BlogList(), JsonRequestBehavior.AllowGet);
        }

        public List<CategoryClass> BlogList()
        {
            var length = cm.GetList().Count();
            
            List<CategoryClass> ct = new List<CategoryClass>();
            for (int i = 1; i <= length; i++)
            {
                ct.Add(new CategoryClass()
                {
                    CategoryName = cm.GetByID(i).CategoryName,
                    CategoryCount = hm.GetList().Where(x => x.CategoryID == i).Count()
                });
            }
            return ct;
        }
    }
}