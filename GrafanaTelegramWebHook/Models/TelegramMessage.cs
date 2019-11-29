namespace GrafanaTelegramWebHook.Models
{
    public class TelegramMessage
    {
        public long chat_id { get; set; }

        public string text { get; set; }

        public string parse_mode { get; set; }
    }
}