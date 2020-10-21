using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
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

        public async Task<(HttpStatusCode, string)> GetStringAsync(Uri uri, CancellationToken cancellation, params HttpStatusCode[] acceptedErrorCodes)
        {
            var client = _httpClientFactory.CreateClient();
            using (var response = await client.GetAsync(uri))
            {
                var content = await response.Content.ReadAsStringAsync();
                if (!response.IsSuccessStatusCode && !acceptedErrorCodes.Contains(response.StatusCode))
                {
                    _logger.LogWarning("Error while fetching {0} : status {1}\r\n{2}", uri, response.StatusCode, content);
                    response.EnsureSuccessStatusCode();
                }
                return (response.StatusCode, content);
            }
        }
    }
}
