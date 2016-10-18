using Akavache;
using MobileWeatherApp.Models;
using MobileWeatherApp.Services;
using ReactiveUI;
using System;
using System.Linq;
using System.Reactive;
using System.Reactive.Linq;

namespace MobileWeatherApp
{
    public class SearchViewModel : ReactiveObject
    {
        public SearchViewModel()
        {
            SearchResults = new ReactiveList<PlaceViewModel>();

            this.WhenAnyValue(x => x.SearchResults)
                .Select(_ => SearchResults.Count > 0)
                .ToProperty(this, x => x.IsSearchResults, out _IsSearchResults);

            Search = ReactiveCommand.CreateAsyncTask(async _ =>
            {
                var places = await PlaceService.GetPlaceFromName(SearchTerm);
                SearchResults.Clear();
                foreach (var item in places.results)
                {
                    SearchResults.Add(new PlaceViewModel(item.formatted_address, item.geometry.location.lat, item.geometry.location.lng));
                }
            });

            this.WhenAnyValue(x => x.SelectedPlace)
                .Where(p => p != null)
                .Subscribe(async pvm =>
                {
                    await BlobCache.UserAccount.InsertObject(pvm.Name, new Place() { Name = pvm.Name, Latitude = pvm.Latitude, Longitude = pvm.Longitude});
                });
        }

        public ReactiveCommand<Unit> Search { get; private set; }

        private string _SearchTerm;
        public string SearchTerm
        {
            get { return _SearchTerm; }
            set { this.RaiseAndSetIfChanged(ref _SearchTerm, value); }
        }

        private ReactiveList<PlaceViewModel> _SearchResults;
        public ReactiveList<PlaceViewModel> SearchResults
        {
            get { return _SearchResults; }
            set { this.RaiseAndSetIfChanged(ref _SearchResults, value); }
        }

        private PlaceViewModel _SelectedPlace;
        public PlaceViewModel SelectedPlace
        {
            get { return _SelectedPlace; }
            set { this.RaiseAndSetIfChanged(ref _SelectedPlace, value); }
        }

        private ObservableAsPropertyHelper<bool> _IsSearchResults;
        public bool IsSearchResults
        {
            get { return _IsSearchResults.Value; }
        }
    }
}
