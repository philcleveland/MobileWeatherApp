using MobileWeatherApp.Services;
using ReactiveUI;
using Splat;
using System;


namespace MobileWeatherApp
{
    public class DailyForecastViewModel : ReactiveObject
    {
        public DailyForecastViewModel(double latitude, double longitude, IDarkSkyService darkSky)
        {
            var epoch = new DateTime(1970, 1, 1, 0, 0, 0);

            //Date = DateTime.Now.ToLocalTime().ToString();
            //Time = string.Format("{0}:{1}", DateTime.Now.ToLocalTime().Hour, DateTime.Now.ToLocalTime().Minute);
            //CurrentTemperature = "0.0";
            //LowTemp = "0.0";
            //HighTemp = "0.0";
            //SunriseTime = "";
            //SunsetTime = "";
            //Wind = "";
            //Humidity = "";

            this.WhenAnyValue(x => x.Activator)
                .Subscribe(async _ =>
                {
                    var weather = await darkSky.GetWeatherData(latitude, longitude);

                    var d = epoch.AddSeconds(weather.currently.time);
                    Date = d.Date.ToString();
                    Time = d.ToLocalTime().TimeOfDay.ToString();
                    CurrentTemperature = string.Format("{0}°", Math.Round(weather.currently.temperature, 0));
                    LowTemp = string.Format("{0}°", Math.Round(weather.daily.data[0].temperatureMin, 0));
                    HighTemp = string.Format("{0}°", Math.Round(weather.daily.data[0].temperatureMax, 0));
                    SunriseTime = epoch.AddSeconds(weather.daily.data[0].sunriseTime).ToLocalTime().ToString();
                    SunsetTime = epoch.AddSeconds(weather.daily.data[0].sunsetTime).ToLocalTime().ToString();
                    Wind = string.Format("{0} mph", weather.currently.windSpeed);
                    Humidity = string.Format("{0}%", weather.currently.humidity);

                    //POSIBLE ICONS
                    //clear-day 
                    //clear-night 
                    //rain 
                    //snow 
                    //sleet 
                    //wind 
                    //fog 
                    //cloudy 
                    //partly-cloudy-day 
                    //partly-cloudy-night
                    //tornado
                    //thunderstorm
                    //hail
                    var iconName = weather.currently.icon.Replace("-", "") + ".png";
                    Icon = await BitmapLoader.Current.LoadFromResource(iconName, null, null);
                });

        }

        public ViewModelActivator Activator { get; private set; }

        private string _Place;
        public string Place
        {
            get { return _Place; }
            set { this.RaiseAndSetIfChanged(ref _Place, value); }
        }

        private string _Date;
        public string Date
        {
            get { return _Date; }
            set { this.RaiseAndSetIfChanged(ref _Date, value); }
        }

        private string _Time;
        public string Time
        {
            get { return _Time; }
            set { this.RaiseAndSetIfChanged(ref _Time, value); }
        }

        private string _CurrentTemperature;
        public string CurrentTemperature
        {
            get { return _CurrentTemperature; }
            set { this.RaiseAndSetIfChanged(ref _CurrentTemperature, value); }
        }

        private IBitmap _Icon;
        public IBitmap Icon
        {
            get { return _Icon; }
            set { this.RaiseAndSetIfChanged(ref _Icon, value); }
        }

        private string _LowTemp;
        public string LowTemp
        {
            get { return _LowTemp; }
            set { this.RaiseAndSetIfChanged(ref _LowTemp, value); }
        }

        private string _HighTemp;
        public string HighTemp
        {
            get { return _HighTemp; }
            set { this.RaiseAndSetIfChanged(ref _HighTemp, value); }
        }


        public string SunriseTime { get; private set; }
        public string SunsetTime { get; private set; }
        public string Wind { get; private set; }
        public string Humidity { get; private set; }
        public string UVIndex { get; private set; }
        public string Alerts { get; private set; }
    }
}
