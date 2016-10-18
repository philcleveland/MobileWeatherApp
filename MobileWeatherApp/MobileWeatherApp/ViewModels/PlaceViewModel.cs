using ReactiveUI;
using System.Threading.Tasks;

namespace MobileWeatherApp
{
    public class PlaceViewModel : ReactiveObject
    {
        public PlaceViewModel()
        {

        }
        public PlaceViewModel(string name, double latitude, double longitude)
        {
            Name = name;
            Latitude = latitude;
            Longitude = longitude;
        }

        public string Name { get;  set; }
        public double Latitude { get;  set; }
        public double Longitude { get;  set; }
    }
}
