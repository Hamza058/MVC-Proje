﻿using BusinessLayer.Concrete;
using DataAccessLayer.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MvcProje.Controllers
{
    [AllowAnonymous]
    public class DefaultController : Controller
    {
        // GET: Default
        HeadingManager hm = new HeadingManager(new EFHeadingDal());
        ContentManager cm = new ContentManager(new EFContentDal());

        public ActionResult Headings()
        {
            var headingList = hm.GetList();
            return View(headingList);
        }
        public PartialViewResult Index(int id = 0)
        {
            var contentList = cm.GetListByHeadingID(id);
            return PartialView(contentList);
        }
    }
}