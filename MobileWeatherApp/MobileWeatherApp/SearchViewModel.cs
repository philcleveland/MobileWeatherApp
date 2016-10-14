using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive;
using System.Reactive.Linq;
using System.Text;
using System.Threading.Tasks;

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
                SearchResults.AddRange(
                    places
                        .results
                        .Select(r => CreateSearchResult(r)));
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

        private ObservableAsPropertyHelper<bool> _IsSearchResults;
        public bool IsSearchResults
        {
            get { return _IsSearchResults.Value; }
        }

        private PlaceViewModel CreateSearchResult(Result r)
        {
            return new PlaceViewModel(r.formatted_address, r.geometry.location.lat, r.geometry.location.lng);
        }
    }
}
