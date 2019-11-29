using System;
using System.Text;
using System.Threading.Tasks;
using GrafanaTelegramWebHook.Models;
using Microsoft.AspNetCore.Mvc;
using Flurl.Http;

namespace GrafanaTelegramWebHook.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SendToBotController : ControllerBase
    {
        private readonly string _botBaseUrl = "https://api.telegram.org/bot";
        private readonly Proxy _proxy;
        private readonly long _chatId;

        private FlurlClient _client;

        public SendToBotController(Proxy proxy, BotSettings botSettings)
        {
            _proxy = proxy;

            _chatId = botSettings.ChatId;
            _botBaseUrl = $"{_botBaseUrl}{botSettings.ApiKey}";

            _client = new FlurlClient
            {
                Settings =
                {
                    HttpClientFactory = new CustomFlurlHttpClient($"{proxy.Address}:{proxy.Port}", proxy.Login, proxy.Password)
                }
            };
        }
        
        [HttpPost]
        public async Task<ActionResult> SendMessageToBot(GrafanaRequest grafanaRequest)
        {
            Console.WriteLine("--> SendMessageToBot");

            var tgMessage = new TelegramMessage()
            {
                chat_id = _chatId,
                parse_mode = "Markdown",
            };

            try
            {
                var sb = new StringBuilder();
                sb.Append($"*Grafana alert*{Environment.NewLine}");
                sb.Append(grafanaRequest.Title + Environment.NewLine);
                sb.Append($"Правило: {grafanaRequest.RuleName}{Environment.NewLine}");
                sb.Append($"Состояние: {grafanaRequest.State}{Environment.NewLine}");
                sb.Append($"Сообщение: {grafanaRequest.Message}{Environment.NewLine}");
                if (grafanaRequest.EvalMatches != null)
                {
                    foreach (var ev in grafanaRequest.EvalMatches)
                    {
                        if (ev.Tags != null)
                            sb.Append($"Метрика: {ev.Metric}; Значение: {ev.Value}; tags: {String.Join(',', ev.Tags.ToString())}{Environment.NewLine}");
                        else
                            sb.Append($"Метрика: {ev.Metric}; Значение: {ev.Value}{Environment.NewLine}");
                    }
                }
                tgMessage.text = sb.ToString();

                var res = await $"{_botBaseUrl}/sendMessage".WithClient(_client).PostJsonAsync(tgMessage);
                if (res.IsSuccessStatusCode)
                    return Accepted();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }

            return Accepted();
        }
    }
}