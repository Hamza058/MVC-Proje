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
            var categoryvalues = cm.GetList();
            int categoryCount = categoryvalues.Count();
            int categoryTrue=0, categoryFalse=0, categoryResult = 0;
            foreach (var item in categoryvalues)
            {
                if (item.CategoryStatus == true)
                    categoryTrue++;
                else
                    categoryFalse++;

                categoryResult = categoryTrue - categoryFalse;
            }

            var headingvalues = hm.GetList();
            int headingCount = 0;
            int[] dizi = new int[headingvalues.Count];
            int i = 0;
            int value = 0;
            int id = 0;
            foreach (var item in headingvalues)
            {
                dizi[i] = item.CategoryID;
                i++;
                if (item.CategoryID == 4)
                    headingCount++;
            }
            var c = dizi.GroupBy(v => v);
            foreach (var item in c)
            {
                if (value < item.Count())
                {
                    value = item.Count();
                    id = item.Key;
                }
            }
            string maxCategory = cm.GetByID(id).CategoryName;

            var writervalues = wm.GetList();
            int writerCount = 0;
            foreach (var item in writervalues)
            {
                if (item.WriterName.IndexOf('a')>=0 || item.WriterName.IndexOf('A') >= 0)
                    writerCount++;
            }
            Dictionary<string, string> results = new Dictionary<string, string>();
            results.Add("Kategori Sayısı", categoryCount.ToString());
            results.Add("Spor Kategorisine ait başlık sayısı", headingCount.ToString());
            results.Add("Yazar Adında 'a' harfi geçen yazar sayısı", writerCount.ToString());
            results.Add("En Fazla Başlığa sahip kategori", maxCategory);
            results.Add("Kategori tablosundaki true-false farkı", categoryResult.ToString());
            return View(results);
        }
    }
}