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
using System.Reactive.Disposables;
using Akavache;
using MobileWeatherApp.Models;
using System.Reactive.Linq;
using MobileWeatherApp.Services;
using MobileWeatherApp.Droid.Views;

namespace MobileWeatherApp.Droid.Activities
{
    [Activity(Label = "WeeklyForecastActivity")]
    public class WeeklyForecastActivity : ReactiveActivity<WeeklyForecastViewModel>
    {
        const string DarkSkyApiKey = "DARK_SKY_API_KEY_HERE";
        public TextView TextPlace { get; private set; }
        public ListView DaysOfTheWeekList { get; private set; }

        public WeeklyForecastActivity()
        {
            this.WhenActivated(() =>
            {
                var disposable = new CompositeDisposable();

                disposable.Add(
                    DaysOfTheWeekList.Events().ItemClick.Select(i => i.Position)
                        .Subscribe(pos =>
                        {
                            var intent = new Intent(this, typeof(DailyForecastActivity));
                            StartActivity(intent);
                        })
                );

                return disposable;
            });
        }

        protected override async void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.WeeklyForecast);
            this.WireUpControls();

            var place = await BlobCache.UserAccount.GetObject<Place>(Intent.GetStringExtra("place"));
            
            ViewModel = new WeeklyForecastViewModel(place, new DarkSkyService(DarkSkyApiKey));
            this.OneWayBind(ViewModel, vm => vm.Place.Name, v => v.TextPlace.Text);

            var myPlacesAdapter = new ReactiveListAdapter<WeeklyForecastItemViewModel>(
            ViewModel.Days,
            (vm, parent) => new WeeklyForecastItemView(this, vm, parent));
            DaysOfTheWeekList.Adapter = myPlacesAdapter;
        }
    }
}