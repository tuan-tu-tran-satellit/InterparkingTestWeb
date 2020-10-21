using System;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace InterparkingTest.Application.Infrastructure
{
    public interface IHttpClient
    {
        Task<(HttpStatusCode, string)> GetStringAsync(Uri uri, CancellationToken cancellation, params HttpStatusCode[] validCodes);
    }
}