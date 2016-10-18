using ReactiveUI;
using System.Reactive;
using System.Threading.Tasks;

namespace MobileWeatherApp
{
    public class MyPlaceViewModel : ReactiveObject
    {
        public MyPlaceViewModel(string name, double latitude, double longitude)
        {
            Name = name;
            Latitude = latitude;
            Longitude = longitude;

            Remove = ReactiveCommand.Create();
        }
        public ReactiveCommand<object> Remove { get; private set; }

        public string Name { get; private set; }
        public double Latitude { get; private set; }
        public double Longitude { get; private set; }
    }
}
