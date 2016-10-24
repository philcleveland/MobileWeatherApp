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
                            intent.PutExtra("lat", ViewModel.Place.Latitude);
                            intent.PutExtra("lng", ViewModel.Place.Longitude);
                            StartActivity(intent);
                        })
                );

                this.OneWayBind(ViewModel, vm => vm.Place.Name, v => v.TextPlace.Text);

                return disposable;
            });
        }

        protected override async void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.WeeklyForecast);
            this.WireUpControls();

            var place = await BlobCache.UserAccount.GetObject<Place>(Intent.GetStringExtra("place"));
            var darkSkyKey = await BlobCache.UserAccount.GetObject<string>("darksky");
            ViewModel = new WeeklyForecastViewModel(place, new DarkSkyService(darkSkyKey));
            
            var myPlacesAdapter = new ReactiveListAdapter<WeeklyForecastItemViewModel>(
                ViewModel.Days,
                (vm, parent) => new WeeklyForecastItemView(this, vm, parent));
            DaysOfTheWeekList.Adapter = myPlacesAdapter;
        }
    }
}