﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
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
            
            return View(waterMeterDataContext.WaterMeters);
        }

        [HttpGet]
        public ActionResult AddWaterMeter()
        {
            return View();
        }

        [HttpPost]
        public ActionResult AddWaterMeter(WaterMeter waterMeter)
        {
            if (ModelState.IsValid)
            {
                waterMeterDataContext.WaterMeters.Add(waterMeter);
                waterMeterDataContext.SaveChanges();
                return RedirectToAction("Index");
            }

            return View();
        }
    }
}