using BusinessLayer.Concrete;
using DataAccessLayer.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MvcProje.Controllers
{
    public class StatisticsController : Controller
    {
        CategoryManager cm = new CategoryManager(new EFCategoryDal());
        HeadingManager hm = new HeadingManager(new EFHeadingDal());
        WriterManager wm = new WriterManager(new EFWriterDal());

        // GET: Statistics
        public ActionResult Index()
        {
            var category = cm.GetList();
            ViewBag.categoryCount = category.Count();

            var heading = hm.GetList().Where(x => x.CategoryID == 4);
            ViewBag.headingCount = heading.Count();

            var writer = wm.GetList().Where(x => x.WriterName.IndexOf('a') >= 0 || x.WriterName.IndexOf('A') >= 0);
            ViewBag.writerCount = writer.Count();

            int[] dizi = new int[hm.GetList().Count()];
            for (int i = 0; i < dizi.Length; i++)
            {
                dizi[i] = hm.GetByID(i+1).CategoryID;
            }

            int sayi = 0;
            int tekrar = 0;
            int max = 0;

            for (int i = 0; i < dizi.Length - 1; i++)
            {
                for (int j = 1; j < dizi.Length; j++)
                {
                    if (dizi[i] == dizi[j])
                    {
                        tekrar++;
                        if (max < tekrar)
                            sayi = dizi[i];
                        max = tekrar;
                    }
                }
                tekrar = 0;
            }
            ViewBag.maxCategory = cm.GetByID(sayi).CategoryName;

            int t = cm.GetList().Where(x => x.CategoryStatus).Count();
            int f = cm.GetList().Where(x => !x.CategoryStatus).Count();

            ViewBag.categoryTF = t - f;
            return View();
        }
    }
}