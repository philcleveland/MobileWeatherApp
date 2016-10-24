using Akavache;
using MobileWeatherApp.Models;
using ReactiveUI;
using System;
using System.Reactive.Linq;

namespace MobileWeatherApp
{
    public class HomeScreenViewModel : ReactiveObject
    {
        public HomeScreenViewModel()
        {
            MyPlaces = new ReactiveList<PlaceViewModel>();

            this.WhenAnyValue(x => x.Activator)
                .Subscribe(async _ =>
                {
                    var places = await BlobCache.UserAccount.GetAllObjects<Place>();

                    MyPlaces.Clear();
                    foreach (var p in places)
                    {
                        MyPlaces.Add(new PlaceViewModel(p.Name, p.Latitude, p.Longitude));
                    }
                });
        }

        public ViewModelActivator Activator { get; private set; }

        private ReactiveList<PlaceViewModel> _MyPlaces;
        public ReactiveList<PlaceViewModel> MyPlaces
        {
            get { return _MyPlaces; }
            set { this.RaiseAndSetIfChanged(ref _MyPlaces, value); }
        }
    }
}
