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
            var appDatePath = Server.MapPath("~/App_Data");
            var configFile = Path.Combine(appDatePath, "Config");
            Config config = null;
            if (System.IO.File.Exists(configFile))
            {
                var configJson = System.IO.File.ReadAllText(configFile, Encoding.GetEncoding(1251));
                config = JsonConvert.DeserializeObject<Config>(configJson);
            }

            return View(config ?? Config.Default);
        }
    }
}