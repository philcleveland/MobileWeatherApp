using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MobileWeatherApp
{
    public class DailyForecastViewModel : ReactiveObject
    {
        public DailyForecastViewModel()
        {

        }

        public string Place { get; private set; }
        public string CurrentTemperature { get; private set; }
    }
}
