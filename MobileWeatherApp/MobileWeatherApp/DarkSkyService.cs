using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ModernHttpClient;
using System.Net.Http;
using System.Net.Http.Headers;
using Polly;
using Polly.Retry;
using Newtonsoft.Json;

namespace MobileWeatherApp
{
    public static class Languages
    {
        public static string Arabic { get { return "ar"; } }
        public static string Azerbaijani { get { return "az"; } }
        public static string Belarusian { get { return "be"; } }
        public static string Bosnian { get { return "bs"; } }
        public static string Czech { get { return "cs"; } }
        public static string German { get { return "de"; } }
        public static string Greek { get { return "el"; } }
        public static string English { get { return "en"; } } //default
        public static string Spanish { get { return "es"; } }
        public static string French { get { return "fr"; } }
        public static string Croatian { get { return "hr"; } }
        public static string Hungarian { get { return "hu"; } }
        public static string Indonesian { get { return "id"; } }
        public static string Italian { get { return "it"; } }
        public static string Icelandic { get { return "is"; } }
        public static string Cornish { get { return "kw"; } }
        public static string NorwegianBokmal { get { return "nb"; } }
        public static string Dutch { get { return "nl"; } }
        public static string Polish { get { return "pl"; } }
        public static string Portuguese { get { return "pt"; } }
        public static string Russian { get { return "ru"; } }
        public static string Slovak { get { return "sk"; } }
        public static string Serbian { get { return "sr"; } }
        public static string Swedish { get { return "sv"; } }
        public static string Tetum { get { return "tet"; } }
        public static string Turkish { get { return "tr"; } }
        public static string Ukrainian { get { return "uk"; } }
        public static string SimplifiedChinese { get { return "zh"; } }
        public static string TraditionalChinese { get { return "zh-tw"; } }
    }

    public static class Units
    {
        /// <summary>
        /// automatically select units based on geographic location
        /// </summary>
        public static string Auto { get { return "auto"; } }
        /// <summary>
        /// same as si, except that windSpeed is in kilometers per hour
        /// </summary>
        public static string CA { get { return "CA"; } }
        /// <summary>
        /// same as si, except that nearestStormDistance and visibility are in miles and
        /// </summary>
        public static string UK2 { get { return "uk2"; } }
        /// <summary>
        ///  Imperial units (the default)
        /// </summary>
        public static string US { get { return "us"; } }
        /// <summary>
        /// SI units
        /// 
        /// summary: Any summaries containing temperature or snow accumulation units will  
        ///          have their values in degrees Celsius or in centimeters (respectively).
        /// nearestStormDistance: Kilometers.
        /// precipIntensity: Millimeters per hour.
        /// precipIntensityMax: Millimeters per hour.
        /// precipAccumulation: Centimeters.
        /// temperature: Degrees Celsius.
        /// temperatureMin: Degrees Celsius.
        /// temperatureMax: Degrees Celsius.
        /// apparentTemperature: Degrees Celsius.
        /// dewPoint: Degrees Celsius.
        /// windSpeed: Meters per second.
        /// pressure: Hectopascals.
        /// visibility: Kilometers.
        /// </summary>
        public static string SI { get { return "si"; } }
    }

    //public interface IDarkSkyApi
    //{
    //    [Get("")]
    //}

    public class DarkSkyService
    {
        static HttpClient httpClient = new HttpClient(new NativeMessageHandler());
        static RetryPolicy retryPolicy;

        static DarkSkyService()
        {
            httpClient.BaseAddress = new Uri("https://api.darksky.net/forecast/");
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

        public static async Task<Weather> GetWeatherData(string apiKey, double latitude, double longitude)
        {
            var url = string.Format(@"{0}/{1},{2}?exclude=flags,minutely", apiKey, latitude, longitude);
            System.Diagnostics.Debug.WriteLine(url);

            return await retryPolicy.ExecuteAsync(async () =>
            {
                try
                {
                    var response = await httpClient.GetAsync(url);
                    response.EnsureSuccessStatusCode();
                    var json = await response.Content.ReadAsStringAsync();
                    return JsonConvert.DeserializeObject<Weather>(json);
                }
                catch (HttpRequestException ex)
                {
                    System.Diagnostics.Debug.WriteLine(ex.Message);
                    throw;
                }
            });
        }

        //https://api.darksky.net/forecast/0123456789abcdef9876543210fedcba/42.3601,-71.0589,409467600?exclude=currently,flags
    }
}
