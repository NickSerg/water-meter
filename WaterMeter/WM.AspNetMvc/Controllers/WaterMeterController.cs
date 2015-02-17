using System;
using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;
using Microsoft.Reporting.WebForms;
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
                var waterMeter = applicationDbContext.WaterMeters.FirstOrDefault(x => x.Period == model.Period);
                if (waterMeter != null)
                {
                    waterMeter.Period = model.Period;
                    waterMeter.Cold = model.Cold;
                    waterMeter.Hot = model.Hot;
                }
                else
                {
                    applicationDbContext.WaterMeters.Add(model);
                }

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

        [Authorize(Roles = "Admin, Users")]
        public ActionResult PrintWaterMeter(WaterMeter waterMeter)
        {
            var prevWaterMeter = applicationDbContext.WaterMeters
                .OrderByDescending(x => x.Period)
                .FirstOrDefault(x => x.Period < waterMeter.Period)
                                 ?? new WaterMeter {Period = waterMeter.Period};

            const string reportType = "PDF";
            var localReport = new LocalReport
            {
                ReportPath = Server.MapPath("~/Content/Report.rdlc"),
                EnableExternalImages = true,
            };
            var config = Config.GetCurrent(Server);
            localReport.SetParameters(new[]
            {
                new ReportParameter("prevPeriod", prevWaterMeter.Period.ToShortDateString()), 
                new ReportParameter("prevCold", prevWaterMeter.Cold.ToString()), 
                new ReportParameter("prevHot", prevWaterMeter.Hot.ToString()), 
                new ReportParameter("Period", waterMeter.Period.ToShortDateString()), 
                new ReportParameter("Cold", waterMeter.Cold.ToString()), 
                new ReportParameter("Hot", waterMeter.Hot.ToString()), 
                new ReportParameter("Address", config.Report.Address), 
                new ReportParameter("Tenant", config.Report.Tenant), 
                new ReportParameter("SignaturePath", new Uri(Server.MapPath("~/App_Data/signature.png")).AbsoluteUri)
            });

            //The DeviceInfo settings should be changed based on the reportType
            //http://msdn2.microsoft.com/en-us/library/ms155397.aspx
            const string deviceInfo = @"<DeviceInfo>
                <OutputFormat>PDF</OutputFormat>
               <PageWidth>8.27in</PageWidth>
                <PageHeight>11.69in</PageHeight>
                <MarginTop>0in</MarginTop>
                <MarginLeft>0in</MarginLeft>
                <MarginRight>0in</MarginRight>
                <MarginBottom>0in</MarginBottom>
            </DeviceInfo>";
            
            Warning[] warnings;
            string[] streams;
            string mimeType;
            string encoding;
            string fileNameExtension;
            var renderedBytes = localReport.Render(
                reportType,
                deviceInfo,
                out mimeType,
                out encoding,
                out fileNameExtension,
                out streams,
                out warnings);

            return File(renderedBytes, "application/pdf");
        }
    }
}