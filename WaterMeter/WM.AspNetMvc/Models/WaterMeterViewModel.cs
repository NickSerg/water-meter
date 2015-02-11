using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Linq;
using System.Web.Mvc;

namespace WM.AspNetMvc.Models
{
    public class WaterMeterViewModel
    {
        [Required]
        [Display(Name = "Месяц")]
        public int PeriodMonth { get; set; }

        [Required]
        [Display(Name = "Год")]
        public int PeriodYear { get; set; }

        [Required]
        [Display(Name = "Холодная вода")]
        public int Cold { get; set; }

        [Required]
        [Display(Name = "Горячая вода")]
        public int Hot { get; set; }

        public static IEnumerable<SelectListItem> Months
        {
            get
            {
                var currentMonth = DateTime.Now.Month;
                return (DateTimeFormatInfo.CurrentInfo?? DateTimeFormatInfo.InvariantInfo)
                        .MonthNames
                       .Select((monthName, index) => new SelectListItem
                       {
                           Value = (index + 1).ToString(),
                           Text = monthName,
                           Selected = index + 1 == currentMonth
                       });
            }
        }

        public static IEnumerable<SelectListItem> Years
        {
            get
            {
                var years = new List<SelectListItem>();
                var currentYear = DateTime.Now.Year;
                for (int i = currentYear - 3; i <= currentYear + 3; i++)
                {
                    var year = i.ToString();
                    years.Add(new SelectListItem { Value = year, Text = year, Selected = i == currentYear});
                }

                return years;
            }
        }
    }
}