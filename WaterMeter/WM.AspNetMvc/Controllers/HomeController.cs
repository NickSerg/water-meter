using System.IO;
using System.Text;
using System.Web.Mvc;
using Newtonsoft.Json;
using WM.AspNetMvc.Models;

namespace WM.AspNetMvc.Controllers
{
    [Authorize(Roles = "Admin, Users")]
    public class HomeController : Controller
    {
        public ActionResult About()
        {
            return View();
        }

        public ActionResult Contact()
        {
            var config = Config.GetCurrent(Server);
            return View(config);
        }
    }
}