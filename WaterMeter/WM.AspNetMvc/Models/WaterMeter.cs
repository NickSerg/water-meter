using System;
using System.ComponentModel.DataAnnotations;

namespace WM.AspNetMvc.Models
{
    public class WaterMeter
    {
        public WaterMeter()
        {
            Created = DateTime.Now;
        }

        [Key]
        public int Id { get; set; }

        [Required]
        [Display(Name = "Холодная вода")]
        public int Cold { get; set; }

        [Required]
        [Display(Name = "Горячая вода")]
        public int Hot { get; set; }
        
        [Required]
        [Display(Name = "Дата для расчёта")]
        public DateTime Period { get; set; }

        public DateTime Created { get; set; }
    }
}
