using InterparkingTest.Application.Config;
using InterparkingTest.Application.Domain;
using InterparkingTest.Application.Services;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Xml.Linq;
using System.Xml.XPath;

namespace InterparkingTest.Application.Infrastructure
{
    class AzureMapsRoutingService : IRoutingService
    {
        private readonly IHttpClient _httpClient;
        private readonly ILogger<AzureMapsRoutingService> _logger;
        private readonly AzureMapsOptions _options;

        public AzureMapsRoutingService(IHttpClient httpClient, IOptions<AzureMapsOptions> options, ILogger<AzureMapsRoutingService> logger)
        {
            _httpClient = httpClient;
            _logger = logger;
            _options = options.Value;
        }

        public async Task<double?> CalculateDistanceAsync(Coordinates startPoint, Coordinates endPoint, CancellationToken cancellation)
        {
            _logger.LogDebug("Calling azure routing {0},{1} - {2},{3}", startPoint.Latitude, startPoint.Longitude, endPoint.Latitude, endPoint.Longitude);
            if (String.IsNullOrEmpty(_options.ApiKey)) throw new InvalidOperationException("Api key is not configured");

            var uri = new UriBuilder("https://atlas.microsoft.com/route/directions/xml");
            var queryParameters = HttpUtility.ParseQueryString("");
            queryParameters["subscription-key"] = _options.ApiKey;
            queryParameters["query"] = GetQuery(startPoint) + ":" + GetQuery(endPoint);
            queryParameters["api-version"] = "1.0";
            uri.Query = queryParameters.ToString();

            var content = await _httpClient.GetStringAsync(uri.Uri, cancellation);

            XDocument document = XDocument.Parse(content);
            XNamespace ns = document.Root.Name.Namespace;
            var distance =
                document
                    .Element(ns + "calculateRouteResponse")
                    .Element(ns + "route")
                    .Element(ns + "summary")
                    .Element(ns + "lengthInMeters")
                    .Value
            ;
            return Convert.ToDouble(distance) / 1000;
        }

        private string GetQuery(Coordinates point)
        {
            return point.Latitude.ToString(CultureInfo.InvariantCulture) + "," + point.Longitude.ToString(CultureInfo.InvariantCulture);
        }
    }
}
