using FluentAssertions;
using InterparkingTest.Application.Config;
using InterparkingTest.Application.Domain;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web;

namespace InterparkingTest.Application.Infrastructure
{
    [TestClass]
    public class AzureRoutingServiceTest
    {
        [TestMethod]
        public async Task GetDistance()
        {
            //Arrange
            var cancellation = new CancellationToken();
            var httpClient = new Moq.Mock<IHttpClient>(Moq.MockBehavior.Strict);
            string response =
                new StreamReader(
                        Assembly.GetExecutingAssembly().GetManifestResourceStream(this.GetType().Namespace + ".routingResponse.xml")
                    ).ReadToEnd();
            Uri calledUri = null;
            httpClient
                .Setup(c => c.GetStringAsync(It.IsAny<Uri>(), cancellation, It.IsAny<HttpStatusCode[]>()))
                .Callback<Uri, CancellationToken, HttpStatusCode[]>((uri, _, codes) => calledUri = uri)
                .ReturnsAsync((HttpStatusCode.OK, response))
            ;

            var options = new AzureMapsOptions()
            {
                ApiKey = "some-key"
            };

            var routingService = new AzureMapsRoutingService(httpClient.Object, Options.Create(options), TestData.GetLogger<AzureMapsRoutingService>());

            var departure = new Coordinates()
            {
                Latitude = 1.2,
                Longitude = 3.4,
            };
            var arrival = new Coordinates()
            {
                Latitude = 5.6,
                Longitude = 7.8,
            };


            //Act
            var distance = await routingService.CalculateDistanceAsync(departure, arrival, cancellation);


            //Assert
            distance.Should().Be(1.146);
            calledUri.AbsolutePath.Should().Be("/route/directions/xml");
            var query = HttpUtility.ParseQueryString(calledUri.Query);
            query["subscription-key"].Should().Be(options.ApiKey);
            query["query"].Should().Be("1.2,3.4:5.6,7.8");
        }

        [TestMethod]
        public async Task GetDistanceForReal()
        {
            var apiKey = Environment.GetEnvironmentVariable("AZURE_MAPS_KEY");
            if (String.IsNullOrEmpty(apiKey))
            {
                Assert.Inconclusive("The api key is not configured");
                //See tests.runsettings for more info
            }
            var services = new ServiceCollection();
            services.AddSingleton<AzureMapsRoutingService>();
            services.AddLogging(logging =>
            {
                logging.AddConsole().SetMinimumLevel(LogLevel.Trace);
            });
            services.AddHttpClient();
            services.AddSingleton<IHttpClient, HttpClient>();

            services.Configure<AzureMapsOptions>(options =>
            {
                options.ApiKey = apiKey;
            });

            var routing = services.BuildServiceProvider().GetRequiredService<AzureMapsRoutingService>();

            var start = new Coordinates()
            {
                Latitude = 52.50931,
                Longitude = 1.42936,
            };
            var end = new Coordinates()
            {
                Latitude = 52.50274,
                Longitude = 4.33872,
            };
            var distance = await routing.CalculateDistanceAsync(start, end, new CancellationToken());
        }
    }
}
