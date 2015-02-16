using System;
using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;
using WM.AspNetMvc.Models;

namespace WM.AspNetMvc.Controllers
{
    [Authorize(Roles = "Admin, Users")]
    public class WaterMeterController : Controller
    {
        private readonly ApplicationDbContext applicationDbContext;

        public WaterMeterController()
        {
            applicationDbContext = new ApplicationDbContext();
        }

        // GET: WaterMeter
        public ActionResult Index()
        {
            ViewBag.MyHeader = "Water Meter";
            
            return View(applicationDbContext.WaterMeters.OrderByDescending(x=>x.Period));
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public ActionResult AddWaterMeter()
        {
            return View();
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public ActionResult AddWaterMeter(WaterMeter model)
        {
            if (ModelState.IsValid)
            {
                if(applicationDbContext.WaterMeters.FirstOrDefault(x=>x.Period == model.Period) != null)
                    applicationDbContext.WaterMeters.Add(model);

                applicationDbContext.SaveChanges();
                return RedirectToAction("Index");
            }

            return View();
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public ActionResult EditWaterMeter(WaterMeter waterMeter)
        {
            return View(waterMeter);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public ActionResult EditWaterMeter(WaterMeter waterMeter, FormCollection formCollection)
        {
            if (ModelState.IsValid)
            {
                applicationDbContext.Entry(waterMeter).State= EntityState.Modified;
                applicationDbContext.SaveChanges();
                return RedirectToAction("Index");
            }

            return View();
        }

        [Authorize(Roles = "Admin")]
        public ActionResult DeleteWaterMeter(WaterMeter waterMeter)
        {
            if (waterMeter != null)
            {
                var deleteWaterMeter = applicationDbContext.WaterMeters.Find(waterMeter.Id);
                if (deleteWaterMeter != null)
                {
                    applicationDbContext.WaterMeters.Remove(deleteWaterMeter);
                    applicationDbContext.SaveChanges();
                }
            }

            return RedirectToAction("Index");
        }
    }
}