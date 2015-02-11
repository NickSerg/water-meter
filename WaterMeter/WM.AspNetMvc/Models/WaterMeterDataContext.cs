using System.Data.Entity;

namespace WM.AspNetMvc.Models
{
    public class WaterMeterDataContext : DbContext
    {
        public WaterMeterDataContext()
            : base("WaterMeterConnection")
        {
        }

        public DbSet<WaterMeter> WaterMeters { get; set; }
    }
}