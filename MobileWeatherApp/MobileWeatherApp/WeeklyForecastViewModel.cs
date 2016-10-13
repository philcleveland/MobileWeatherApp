using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MobileWeatherApp
{
    public class WeeklyForecastViewModel : ReactiveObject
    {
        public WeeklyForecastViewModel()
        {

        }

        public string Place { get; private set; }
        private ReactiveList<WeeklyForecastItemViewModel> _Days;
        public ReactiveList<WeeklyForecastItemViewModel> Days
        {
            get { return _Days; }
            set { this.RaiseAndSetIfChanged(ref _Days, value); }
        }
    }
}
