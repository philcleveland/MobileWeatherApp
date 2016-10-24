using ReactiveUI;
using System;
using Splat;

namespace MobileWeatherApp
{
    public class WeeklyForecastItemViewModel : ReactiveObject
    {
        public WeeklyForecastItemViewModel(int unixTime, double min, double max, string icon, string summary)
        {
            var date = new DateTime(1970, 1, 1, 0, 0, 0).AddSeconds(unixTime);
            
            Day = date.DayOfWeek.ToString().Substring(0, 3);
            TemperatureMin = string.Format("{0}°", Math.Round(min, 0));
            TemperatureMax = string.Format("{0}°", Math.Round(max, 0));
            Summary = summary;

            this.WhenAnyValue(x => x.Activator)
                .Subscribe(async _ =>
                {
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
                    var iconName = icon.Replace("-", "") + ".png";
                    System.Diagnostics.Debug.WriteLine("Icon for {0} is {1}", Day, iconName); 
                    Icon = await BitmapLoader.Current.LoadFromResource(iconName, null, null);
                });
        }
        public ViewModelActivator Activator { get; private set; }
        public string Day { get; private set; }
        public string TemperatureMin { get; private set; }
        public string TemperatureMax { get; private set; }
        public IBitmap Icon { get; private set; }
        public string Summary { get; private set; }
        
    }
}
