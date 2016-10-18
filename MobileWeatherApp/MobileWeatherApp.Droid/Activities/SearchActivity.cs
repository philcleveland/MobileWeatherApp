using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using ReactiveUI;
using System.Reactive.Linq;
using System.Reactive.Disposables;
using Akavache;
using MobileWeatherApp.Droid.Views;

namespace MobileWeatherApp.Droid.Activities
{
    [Activity(Label = "SearchActivity")]
    public class SearchActivity : ReactiveActivity<SearchViewModel>
    {
        public ImageButton SearchButton { get; private set; }
        public EditText SearchText { get; private set; }
        public ListView SearchResults { get; private set; }

        public SearchActivity()
        {
            this.WhenActivated(() =>
            {
                var disposable = new CompositeDisposable();
                var adapter = new ReactiveListAdapter<PlaceViewModel>(ViewModel.SearchResults,
                    (vm, parent) => new PlaceView(this, vm, parent));
                SearchResults.Adapter = adapter;

                disposable.Add(
                    this.SearchResults.Events().ItemClick.Select(x => x.Position).Subscribe(pos =>
                  {
                      this.ViewModel.SelectedPlace = this.ViewModel.SearchResults[pos];

                      var intent = new Intent(this, typeof(HomeScreenActivity));
                      StartActivity(intent);
                  }));

                return disposable;
            });
        }

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.Search);
            this.WireUpControls();

            ViewModel = new SearchViewModel();
            this.Bind(ViewModel, vm => vm.SearchTerm, v => v.SearchText.Text);
            ////this.OneWayBind(ViewModel, vm => vm.IsPlacesVisible, v => v.Places.Visibility);
            this.BindCommand(ViewModel, vm => vm.Search, v => v.SearchButton);
        }
    }
}