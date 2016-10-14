using ReactiveUI;
using System.Linq;
using System.Reactive;
using System.Reactive.Linq;
using System;
namespace MobileWeatherApp
{
    public class HomeScreenViewModel : ReactiveObject
    {
        public HomeScreenViewModel(IPlacesRepository placeRepo)
        {
            Places = new ReactiveList<PlaceViewModel>();
            MyPlaces = new ReactiveList<MyPlaceViewModel>();

            Search = ReactiveCommand.CreateAsyncTask(async _ =>
            {
                var places = await PlaceService.GetPlaceFromName(SearchTerm);
                Places.Clear();

                Places.AddRange(
                    places.results.Select(x =>
                    {
                        var p = new PlaceViewModel(x.formatted_address, x.geometry.location.lat, x.geometry.location.lng, placeRepo);
                        p.Add.Subscribe(y =>
                        {
                            var myPlace = new MyPlaceViewModel(p.Name, p.Latitude, p.Longitude);
                            myPlace.Remove.Subscribe(z =>
                            {
                                placeRepo.RemovePlace(myPlace.Name);
                                MyPlaces.Remove(myPlace);
                            });
                            placeRepo.AddPlace(myPlace);
                            MyPlaces.Add(myPlace);
                            Places.Clear();
                        });
                        return p;
                    }));
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
