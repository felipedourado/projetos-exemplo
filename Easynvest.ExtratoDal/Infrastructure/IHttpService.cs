using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace Easynvest.ExtratoDal.Infrastructure
{
    public interface IHttpService
    {
        HttpContent Get(Uri uri, string apikey, string token);
        Task<T> Get<T>(Uri uri, string subscriptionKey, string token);
    }
}
