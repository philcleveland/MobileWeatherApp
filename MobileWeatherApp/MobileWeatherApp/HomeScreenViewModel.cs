using Akavache;
using ReactiveUI;
using System.Linq;
using System.Reactive;
using System.Reactive.Linq;

namespace MobileWeatherApp
{
    public class HomeScreenViewModel : ReactiveObject
    {
        public HomeScreenViewModel(IPlacesRepository placeRepo)
        {
            BlobCache.ApplicationName = "MobileWeatherApp";

            Places = new ReactiveList<PlaceViewModel>();
            Search = ReactiveCommand.CreateAsyncTask(async _ =>
            {
                var places = await PlaceService.GetPlaceFromName(SearchTerm);
                Places.Clear();
                Places.AddRange(
                    places.results.Select(x => new PlaceViewModel(x.formatted_address, x.geometry.location.lat, x.geometry.location.lng, placeRepo)));
            });
        }

        private string _SearchTerm;
        public string SearchTerm
        {
            get { return _SearchTerm; }
            set { this.RaiseAndSetIfChanged(ref _SearchTerm, value); }
        }

        public ReactiveCommand<Unit> Search { get; private set; }

        private ReactiveList<PlaceViewModel> _Places;
        public ReactiveList<PlaceViewModel> Places
        {
            get { return _Places; }
            set { this.RaiseAndSetIfChanged(ref _Places, value); }
        }

        private ReactiveList<MyPlaceViewModel> _MyPlaces;
        public ReactiveList<MyPlaceViewModel> MyPlaces
        {
            get { return _MyPlaces; }
            set { this.RaiseAndSetIfChanged(ref _MyPlaces, value); }
        }
    }
}
