using ReactiveUI;
using System.Reactive;
using System.Threading.Tasks;

namespace MobileWeatherApp
{
    public class MyPlaceViewModel : ReactiveObject
    {
        public MyPlaceViewModel(string name, double latitude, double longitude, IPlacesRepository placeRepo)
        {
            Name = name;
            Latitude = latitude;
            Longitude = longitude;

            Remove = ReactiveCommand.CreateAsyncTask(async _ =>
            {
                await Task.Run(()=> placeRepo.RemovePlace(Name));
            });
        }
        public ReactiveCommand<Unit> Remove { get; private set; }

        public string Name { get; private set; }
        public double Latitude { get; private set; }
        public double Longitude { get; private set; }
    }
}
