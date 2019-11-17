namespace GrafanaTelegramWebHook.Models
{
    public class Proxy
    {
        public string Address { get; set; }
      
        public int Port { get; set; } = 0;
       
        public string Login { get; set; }
      
        public string Password { get; set; }

    }
}