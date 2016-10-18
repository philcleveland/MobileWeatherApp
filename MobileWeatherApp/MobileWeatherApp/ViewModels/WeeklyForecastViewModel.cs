using MobileWeatherApp.Models;
using MobileWeatherApp.Services;
using ReactiveUI;
using System;
using System.Linq;

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
                    var weather = await darkSky.GetWeatherData(Place.Latitude, Place.Longitude);
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
    }
}
