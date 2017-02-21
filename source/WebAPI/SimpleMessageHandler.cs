using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using Cmc.Core.ComponentModel;
using Cmc.Core.Diagnostics;

namespace SimpleODataApiWithEf
{
    public class SimpleMessageHandler : DelegatingHandler
    {
        private readonly ILogger _logger;

        public SimpleMessageHandler()
        {
            _logger = ServiceLocator.Default.GetInstance<ILoggerFactory>().GetLogger(this);
        }

        protected override Task<HttpResponseMessage> SendAsync(
        HttpRequestMessage request, CancellationToken cancellationToken)
        {
            using (new LogScope(_logger))
            {
                // Create the response.
                var response = new HttpResponseMessage(HttpStatusCode.OK)
                {
                    Content = new StringContent("Hello!")
                };

                // Note: TaskCompletionSource creates a task that does not contain a delegate.
                var tsc = new TaskCompletionSource<HttpResponseMessage>();
                tsc.SetResult(response);   // Also sets the task state to "RanToCompletion"
                return tsc.Task;
            }
        }
    }
}