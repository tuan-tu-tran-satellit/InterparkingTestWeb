using System;
using System.Threading;
using System.Threading.Tasks;

namespace InterparkingTest.Application.Infrastructure
{
    public interface IHttpClient
    {
        Task<string> GetStringAsync(Uri uri, CancellationToken cancellation);
    }
}