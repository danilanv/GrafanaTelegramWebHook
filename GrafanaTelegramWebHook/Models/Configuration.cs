namespace GrafanaTelegramWebHook.Models
{
    public class Configuration
    {
        public Proxy Proxy { get; set; } = new Proxy();

        public BotSettings BotSettings { get; set; } = new BotSettings();
    }
}