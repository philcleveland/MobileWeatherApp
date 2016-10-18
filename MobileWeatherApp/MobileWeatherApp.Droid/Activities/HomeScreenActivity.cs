using Akavache;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Views;
using Android.Widget;
using MobileWeatherApp.Droid.Views;
using MobileWeatherApp.Models;
using ReactiveUI;
using Splat;
using System;
using System.Linq;
using System.Reactive.Disposables;
using System.Reactive.Linq;

namespace MobileWeatherApp.Droid.Activities
{
    [Activity(Label = "MobileWeatherApp.Droid", MainLauncher = true, Icon = "@drawable/icon")]
    public class HomeScreenActivity : ReactiveActivity<HomeScreenViewModel>
    {
        const string DarkSkyApiKey = "API_KEY_HERE";

        public ImageButton AddPlaceBtn { get; private set; }
        public ListView MyPlacesList { get; private set; }

        public HomeScreenActivity()
        {
            BlobCache.ApplicationName = "MobileWeatherApp";
            

            this.WhenActivated(() =>
            {
                var disp = new CompositeDisposable();
                return disp;
            });
            
        }

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            SetContentView(Resource.Layout.HomeScreen);
            this.WireUpControls();
            ViewModel = new HomeScreenViewModel();

            Locator.CurrentMutable.RegisterConstant(new BoolToVisibilityConverter(), typeof(IBindingTypeConverter));

            var myPlacesAdapter = new ReactiveListAdapter<PlaceViewModel>(
                ViewModel.MyPlaces,
                (vm, parent) => new PlaceView(this, vm, parent));
            MyPlacesList.Adapter = myPlacesAdapter;

            MyPlacesList.Events().ItemClick.Select(i => i.Position)
                .Subscribe(pos =>
                {
                    var intent = new Intent(this, typeof(WeeklyForecastActivity));
                    intent.PutExtra("place", (string)ViewModel.MyPlaces[pos].Name);
                    StartActivity(intent);
                });

            AddPlaceBtn.Events().Click.Subscribe(_ =>
            {
                var intent = new Intent(this, typeof(SearchActivity));
                StartActivity(intent);
            });
            
        }

        protected override void OnResume()
        {
            base.OnResume();
            
        }

        protected override void OnRestart()
        {
            base.OnRestart();
        }

    }
}


