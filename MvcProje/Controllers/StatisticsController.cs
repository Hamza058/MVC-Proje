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
            //int categoryTrue = 0, categoryFalse = 0, categoryResult = 0;

            //foreach (var item in categoryvalues)
            //{
            //    if (item.CategoryStatus == true)
            //        categoryTrue++;
            //    else
            //        categoryFalse++;

            //    categoryResult = categoryTrue - categoryFalse;
            //}


            //int headingCount = 0;
            //int[] dizi = new int[headingvalues.Count];
            //int i = 0;
            //int value = 0;
            //int id = 0;
            //foreach (var item in headingvalues)
            //{
            //    dizi[i] = item.CategoryID;
            //    i++;
            //    if (item.CategoryID == 4)
            //        headingCount++;
            //}
            //var c = dizi.GroupBy(v => v);
            //foreach (var item in c)
            //{
            //    if (value < item.Count())
            //    {
            //        value = item.Count();
            //        id = item.Key;
            //    }
            //}
            //string maxCategory = cm.GetByID(id).CategoryName;

            //var writervalues = wm.GetList();
            //int writerCount = 0;
            //foreach (var item in writervalues)
            //{
            //    if (item.WriterName.IndexOf('a') >= 0 || item.WriterName.IndexOf('A') >= 0)
            //        writerCount++;
            //}
            //Dictionary<string, string> results = new Dictionary<string, string>();
            ////results.Add("Kategori Sayısı", categoryCount.ToString());
            //results.Add("Spor Kategorisine ait başlık sayısı", headingCount.ToString());
            //results.Add("Yazar Adında 'a' harfi geçen yazar sayısı", writerCount.ToString());
            //results.Add("En Fazla Başlığa sahip kategori", maxCategory);
            //results.Add("Kategori tablosundaki true-false farkı", categoryResult.ToString());
            //return View(results);
            return View();
        }
    }
}