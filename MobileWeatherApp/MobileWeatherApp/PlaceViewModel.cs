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

            Add = ReactiveCommand.CreateAsyncTask(async _ =>
            {
                await Task.Run(() => placeRepo.AddPlace(new MyPlaceViewModel(Name, Latitude, Longitude, placeRepo)));
            });
        }

        public ReactiveCommand<Unit> Add { get; private set; }

        public string Name { get; private set; }
        public double Latitude { get; private set; }
        public double Longitude { get; private set; }
    }
}
