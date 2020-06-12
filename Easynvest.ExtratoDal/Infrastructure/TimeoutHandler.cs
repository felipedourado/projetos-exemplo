using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace Easynvest.ExtratoDal.Infrastructure
{
    public class TimeoutHandler : DelegatingHandler
    {
        public TimeSpan DefaultTimeout { get; set; } = TimeSpan.FromSeconds(10);

        protected async override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request,
            CancellationToken cancellationToken)
        {
            using (var cts = GetCancellationTokenSource(request, cancellationToken))
            {
                try
                {
                    return await base.SendAsync(request, cts?.Token ?? cancellationToken);
                }
                catch (OperationCanceledException)
                {
                    if (cancellationToken.IsCancellationRequested)
                        throw new TaskCanceledException();

                    throw new TimeoutException();
                }
            }
        }

        private CancellationTokenSource GetCancellationTokenSource(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            if (DefaultTimeout != Timeout.InfiniteTimeSpan)
            {
                var cts = CancellationTokenSource.CreateLinkedTokenSource(cancellationToken);
                cts.CancelAfter(DefaultTimeout);
                return cts;
            }

            return null;
        }

    }
}
