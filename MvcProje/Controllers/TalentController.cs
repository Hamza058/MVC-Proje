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
	[AllowAnonymous]
    public class TalentController : Controller
    {
		TalentsManager tm = new TalentsManager(new EFTalentsDal());

        // GET: Talent
        public ActionResult Index()
        {
			var values = tm.GetList();
            return View(values);
        }
		public PartialViewResult TalentPartial()
		{
			return PartialView();
		}
		[HttpPost]
		public ActionResult TalentAdd(Talents talents)
		{
			tm.TalentsAdd(talents);
			return RedirectToAction("Index");
		}
		[HttpGet]
		public ActionResult TalentUpdate(int id)
		{
			var value = tm.GetByID(id);
			return View(value);
		}
		[HttpPost]
		public ActionResult TalentUpdate(Talents talents)
		{
			tm.TalentsUpdate(talents);
			return RedirectToAction("Index");
		}
	
		public ActionResult TalentDelete(int id)
		{
			var value = tm.GetByID(id);
			tm.TalentsDelete(value);
			return RedirectToAction("Index");
		}
	}
}