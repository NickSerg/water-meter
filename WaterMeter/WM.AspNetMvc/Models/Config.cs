namespace WM.AspNetMvc.Models
{
    public class Config
    {
        public Contact Contact { get; set; }

        public static Config Default
        {
            get
            {
                return new Config {Contact = new Contact {Email = "support@watermeter.com"}};
            }
        }
    }
    public class Contact
    {
        public string Sity { get; set; }
        public string Home { get; set; }

        public string Email { get; set; }
    }
}
