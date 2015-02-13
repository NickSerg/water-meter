using System.Data.Entity;
using WM.AspNetMvc.Migrations;

namespace WM.AspNetMvc.Models
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit http://go.microsoft.com/fwlink/?LinkID=317594 to learn more.

    public partial class ApplicationDbContext
    {
        static ApplicationDbContext()
        {
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<ApplicationDbContext, Configuration>()); 
        }

        public DbSet<WaterMeter> WaterMeters { get; set; }
    }
}