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
            MyPlaces = new ReactiveList<MyPlaceViewModel>();
        }

        private ReactiveList<MyPlaceViewModel> _MyPlaces;
        public ReactiveList<MyPlaceViewModel> MyPlaces
        {
            get { return _MyPlaces; }
            set { this.RaiseAndSetIfChanged(ref _MyPlaces, value); }
        }

        private MyPlaceViewModel Create(string name, double latitude, double longitude)
        {
            var myPlace = new MyPlaceViewModel(name, latitude, longitude);
            myPlace.Remove.Subscribe(z =>
            {
                MyPlaces.Remove(myPlace);
            });
            return myPlace;
        }
    }
}
