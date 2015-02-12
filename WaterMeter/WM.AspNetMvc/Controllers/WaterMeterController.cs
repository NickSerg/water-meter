using System;
using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;
using WM.AspNetMvc.Models;

namespace WM.AspNetMvc.Controllers
{
    public class WaterMeterController : Controller
    {
        private readonly WaterMeterDataContext waterMeterDataContext;

        public WaterMeterController()
        {
            waterMeterDataContext = new WaterMeterDataContext();
        }

        // GET: WaterMeter
        public ActionResult Index()
        {
            ViewBag.MyHeader = "Water Meter";
            
            return View(waterMeterDataContext.WaterMeters.OrderByDescending(x=>x.Period));
        }

        [HttpGet]
        public ActionResult AddWaterMeter()
        {
            return View();
        }

        [HttpPost]
        public ActionResult AddWaterMeter(WaterMeterViewModel model)
        {
            if (ModelState.IsValid)
            {
                var waterMeter = waterMeterDataContext.WaterMeters.FirstOrDefault(x =>
                    x.Period.Month == model.PeriodMonth
                    && x.Period.Year == model.PeriodYear);

                var isNew = waterMeter == null;
                if(isNew)
                    waterMeter = new WaterMeter();
                
                waterMeter.Period = new DateTime(model.PeriodYear, model.PeriodMonth, DateTime.DaysInMonth(model.PeriodYear, model.PeriodMonth));
                waterMeter.Cold = model.Cold;
                waterMeter.Hot = model.Hot;
                if(isNew)
                    waterMeterDataContext.WaterMeters.Add(waterMeter);
                waterMeterDataContext.SaveChanges();
                return RedirectToAction("Index");
            }

            return View();
        }

        [HttpGet]
        public ActionResult EditWaterMeter(WaterMeter waterMeter)
        {
            return View(waterMeter);
        }

        [HttpPost]
        public ActionResult EditWaterMeter(WaterMeter waterMeter, FormCollection formCollection)
        {
            if (ModelState.IsValid)
            {
                waterMeterDataContext.Entry(waterMeter).State= EntityState.Modified;
                waterMeterDataContext.SaveChanges();
                return RedirectToAction("Index");
            }

            return View();
        }

        public ActionResult DeleteWaterMeter(WaterMeter waterMeter)
        {
            if (waterMeter != null)
            {
                var deleteWaterMeter = waterMeterDataContext.WaterMeters.Find(waterMeter.Id);
                if (deleteWaterMeter != null)
                {
                    waterMeterDataContext.WaterMeters.Remove(deleteWaterMeter);
                    waterMeterDataContext.SaveChanges();
                }
            }

            return RedirectToAction("Index");
        }
    }
}