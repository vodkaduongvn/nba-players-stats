using System.Net;
using Polly;
using Polly.Extensions.Http;

namespace NBA.Players.Charts.Extensions
{
    public static class ServiceExtension
    {
        public static IAsyncPolicy<HttpResponseMessage> GetRetryPolicy(int retryCount = 3)
        {
            return HttpPolicyExtensions
                .HandleTransientHttpError() // Handles 5xx, 408, and network failures
                .OrResult(msg => msg.StatusCode == HttpStatusCode.NotFound) // Retry on 404
                .WaitAndRetryAsync(retryCount, retryAttempt =>
                    TimeSpan.FromSeconds(Math.Pow(2, retryAttempt))); // Exponential backoff
        }

        // Define the circuit breaker policy
        public static IAsyncPolicy<HttpResponseMessage> GetCircuitBreakerPolicy(int handledEventsAllowedBeforeBreaking = 2,
            int durationOfBreakInSeconds = 30)
        {
            return HttpPolicyExtensions
                .HandleTransientHttpError()
                .CircuitBreakerAsync(
                    handledEventsAllowedBeforeBreaking,
                    TimeSpan.FromSeconds(durationOfBreakInSeconds)
                );
        }
    }
}
