using System;
using System.ComponentModel.DataAnnotations;

namespace WM.AspNetMvc.Models
{
    public class WaterMeter
    {
        [Key]
        [Required]
        [Display(Name = "Id")]
        public int Id { get; set; }

        [Required]
        [Display(Name = "Холодная вода")]
        public int Cold { get; set; }

        [Required]
        [Display(Name = "Горячая вода")]
        public int Hot { get; set; }

        [Required]
        [Display(Name = "Период показаний")]
        public DateTime Period { get; set; }

        [Required]
        [Display(Name = "Дата регистрации")]
        public DateTime Created { get; set; }
    }
}
