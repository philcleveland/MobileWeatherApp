using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MobileWeatherApp
{
    public class WeeklyForecastItemViewModel : ReactiveObject
    {
        public WeeklyForecastItemViewModel()
        {

        }

        public string Day { get; private set; }
        public double TemperatureMin { get; private set; }
        public double TemperatureMax { get; private set; }
        public string Icon { get; private set; }
        public string Summary { get; private set; }
        
    }
}
