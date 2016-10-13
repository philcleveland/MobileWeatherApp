using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using ModernHttpClient;
using Polly;
using Polly.Retry;
using System.Net.Http.Headers;
using Newtonsoft.Json;

namespace MobileWeatherApp
{
    public static class PlaceService
    {
        static HttpClient httpClient = new HttpClient(new NativeMessageHandler());
        static RetryPolicy retryPolicy;

        static PlaceService()
        {
            httpClient.BaseAddress = new Uri("https://maps.google.com/maps/api/geocode/");
            httpClient.DefaultRequestHeaders.Accept.Clear();
            httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            retryPolicy =
                Policy.Handle<HttpRequestException>()
                .WaitAndRetryAsync(retryCount: 3,
                                   sleepDurationProvider: attempt => TimeSpan.FromMilliseconds(250),
                                   onRetry: (ex, calcWaitDuration) =>
                                   {
                                       //log.Information(ex, ex.Message);
                                   });
        }

        public static async Task<Places> GetPlaceFromName(string placeName)
        {
            //json?address=Reno,NV
            var url = string.Format(@"json?address={0}", placeName);
            System.Diagnostics.Debug.WriteLine(url);

            return await retryPolicy.ExecuteAsync(async () =>
            {
                try
                {
                    var response = await httpClient.GetAsync(url);
                    response.EnsureSuccessStatusCode();
                    var json = await response.Content.ReadAsStringAsync();
                    return JsonConvert.DeserializeObject<Places>(json);
                }
                catch (HttpRequestException ex)
                {
                    System.Diagnostics.Debug.WriteLine(ex.Message);
                    throw;
                }
            });
        }
    }
}
