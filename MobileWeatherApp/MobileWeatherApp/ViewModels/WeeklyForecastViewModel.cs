using Akavache;
using MobileWeatherApp.Models;
using MobileWeatherApp.Services;
using ReactiveUI;
using System;
using System.Linq;
using System.Threading.Tasks;
using System.Reactive.Linq;

namespace MobileWeatherApp
{
    public class WeeklyForecastViewModel : ReactiveObject
    {
        const int DaysPerWeek = 7;
        public WeeklyForecastViewModel(Place place, IDarkSkyService darkSky)
        {
            Place = place;
            Days = new ReactiveList<WeeklyForecastItemViewModel>();

            this.WhenAnyValue(x => x.Activator)
                .Subscribe(async _ =>
                {
                    var weather = await GetWeatherAsync(Place, darkSky);
                    
                    Days.AddRange(
                        weather
                            .daily
                            .data
                            .Take(DaysPerWeek)
                            .Select(d => new WeeklyForecastItemViewModel(d.time, d.temperatureMin, d.temperatureMax, d.icon, d.summary))
                    );
                });


        }

        public ViewModelActivator Activator { get; private set; }

        public Place Place { get; private set; }

        private ReactiveList<WeeklyForecastItemViewModel> _Days;
        public ReactiveList<WeeklyForecastItemViewModel> Days
        {
            get { return _Days; }
            set { this.RaiseAndSetIfChanged(ref _Days, value); }
        }

        private async Task<Weather> GetWeatherAsync(Place place, IDarkSkyService darkSky)
        {
            return await darkSky.GetWeatherData(Place.Latitude, Place.Longitude);

            //var offset = new DateTimeOffset(DateTime.UtcNow);
            //offset.AddHours(1);
            //return await BlobCache.UserAccount.GetOrCreateObject("weather_" + Place.Name,
            //                 () => darkSky.GetWeatherData(Place.Latitude, Place.Longitude).Result, 
            //                 offset);
        }
    }
}
