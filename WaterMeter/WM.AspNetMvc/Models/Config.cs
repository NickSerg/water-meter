using System.IO;
using System.Text;
using System.Web;
using Newtonsoft.Json;

namespace WM.AspNetMvc.Models
{
    public class Config
    {
        private static Config instance;

        public Contact Contact { get; set; }

        public Report Report { get; set; }

        private static Config Default
        {
            get { return new Config()
            {
                Contact = new Contact {Email = "support@watermeter.com"},
                Report = new Report()
            }; }
        }

        public static Config GetCurrent(HttpServerUtilityBase server)
        {
            if (instance != null)
                return instance;

            var appDatePath = server.MapPath("~/App_Data");
            var configFile = Path.Combine(appDatePath, "Config");
            if (File.Exists(configFile))
            {
                var configJson = File.ReadAllText(configFile, Encoding.GetEncoding(1251));
                instance = JsonConvert.DeserializeObject<Config>(configJson);
            }
            else
                instance = Default;

            return instance;
        }
    }

    public class Contact
    {
        public string Sity { get; set; }

        public string Home { get; set; }

        public string Email { get; set; }
    }

    public class Report
    {
        public string Address { get; set; }

        public string Tenant { get; set; }
    }
}
