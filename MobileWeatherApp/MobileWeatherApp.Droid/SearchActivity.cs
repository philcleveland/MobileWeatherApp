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

namespace MobileWeatherApp.Droid
{
    [Activity(Label = "SearchActivity")]
    public class SearchActivity : ReactiveActivity<SearchViewModel>
    {
        public ImageButton SearchButton { get; private set; }
        public EditText SearchText { get; private set; }
        public ListView SearchResults { get; private set; }
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.Search);

            // Create your application here
            SearchButton = this.FindViewById<ImageButton>(Resource.Id.searchButton);
            SearchText = this.FindViewById<EditText>(Resource.Id.searchText);
            SearchResults = this.FindViewById<ListView>(Resource.Id.searchResults);

            ViewModel = new SearchViewModel();
            this.Bind(ViewModel, vm => vm.SearchTerm, v => v.SearchText.Text);
            //this.OneWayBind(ViewModel, vm => vm.IsPlacesVisible, v => v.Places.Visibility);
            this.BindCommand(ViewModel, vm => vm.Search, v => v.SearchButton);

            var adapter = new ReactiveListAdapter<PlaceViewModel>(
                ViewModel.SearchResults,
                (vm, parent) => new PlaceView(vm, this, parent));

            SearchResults.ItemSelected += (s, e) =>
            {
                if (e.Position < 0) return;

                var intent = new Intent();
                //if (ViewModel.Places.Count > e.Position)
                //{
                //    await ViewModel.Places[e.Position].Add.ExecuteAsync(null);
                //}
            };

            SearchResults.Adapter = adapter;
        }
    }
}