using ReactiveUI;
using System.Reactive;
using System.Threading.Tasks;

namespace MobileWeatherApp
{
    public class PlaceViewModel : ReactiveObject
    {
        public PlaceViewModel(string name, double latitude, double longitude, IPlacesRepository placeRepo)
        {
            Name = name;
            Latitude = latitude;
            Longitude = longitude;

            Add = ReactiveCommand.Create();
        }

        public ReactiveCommand<object> Add { get; private set; }

        public string Name { get; private set; }
        public double Latitude { get; private set; }
        public double Longitude { get; private set; }
    }
}
