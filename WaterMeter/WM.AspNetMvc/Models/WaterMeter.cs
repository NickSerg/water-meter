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

        public int Cold { get; set; }

        public int Hot { get; set; }
        
        public DateTime Period { get; set; }

        public DateTime Created { get; set; }
    }
}
