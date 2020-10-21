using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace InterparkingTest.Application.Infrastructure
{
    class HttpClient : IHttpClient
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly ILogger<HttpClient> _logger;

        public HttpClient(IHttpClientFactory httpClientFactory, ILogger<HttpClient> logger)
        {
            _httpClientFactory = httpClientFactory;
            _logger = logger;
        }

        public async Task<string> GetStringAsync(Uri uri, CancellationToken cancellation)
        {
            var client = _httpClientFactory.CreateClient();
            using (var response = await client.GetAsync(uri))
            {
                var content = await response.Content.ReadAsStringAsync();
                if (!response.IsSuccessStatusCode)
                {
                    _logger.LogWarning("Error while fetching {0} : status {1}\r\n{2}", uri, response.StatusCode, content);
                    response.EnsureSuccessStatusCode();
                }
                return content;
            }
        }
    }
}
