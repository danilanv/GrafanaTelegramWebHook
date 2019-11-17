using System.Net;
using System.Net.Http;
using Flurl.Http.Configuration;

namespace GrafanaTelegramWebHook
{
    /// <summary>
    /// Добавляет к Flurl клиенту поддержку прокси и сжатия HTTP GZIP.
    /// </summary>
    public class CustomFlurlHttpClient : DefaultHttpClientFactory
    {
        private readonly string _address;
        private readonly string _login;
        private readonly string _pass;
        private readonly bool _acceptGzip;

        /// <param name="address">Адрес прокси, включая порт</param>
        /// <param name="login">Логин к прокси</param>
        /// <param name="pass">Пароль</param>
        /// <param name="acceptGzip">Использовать gzip сжатие HTTP</param>
        public CustomFlurlHttpClient(string address, string login, string pass, bool acceptGzip = false)
        {
            _address = address;
            _login = login;
            _pass = pass;
            _acceptGzip = acceptGzip;
        }

        public override HttpClient CreateHttpClient(HttpMessageHandler handler)
        {
            return base.CreateHttpClient(CreateProxyHttpClientHandler());
        }

        private HttpClientHandler CreateProxyHttpClientHandler()
        {
            NetworkCredential proxyCreds = new NetworkCredential(
                _login,
                _pass
            );

            WebProxy proxy = new WebProxy(_address.Trim(), false)
            {
                UseDefaultCredentials = false,
                Credentials = proxyCreds,
            };
            
            var httpClientHandler = new HttpClientHandler()
            {
                Proxy = proxy,
                PreAuthenticate = true,
                UseDefaultCredentials = false,
                MaxAutomaticRedirections = 2,
                MaxConnectionsPerServer = 1
            };
            
            if (_acceptGzip)
                httpClientHandler.AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate;
            
            
            return httpClientHandler;
        }
    }
}