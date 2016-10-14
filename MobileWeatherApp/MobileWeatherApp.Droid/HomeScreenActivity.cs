﻿using System;
using System.Linq;
using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using Android.Locations;
using ReactiveUI;
using System.Reactive.Linq;
using Splat;

namespace MobileWeatherApp.Droid
{
    [Activity(Label = "MobileWeatherApp.Droid", MainLauncher = true, Icon = "@drawable/icon")]
    public class HomeScreenActivity :  ReactiveActivity<HomeScreenViewModel>
    {
        const string DarkSkyApiKey = "API_KEY_HERE";

        public ImageButton SearchButton { get; private set; }
        public EditText SearchText { get; private set; }
        
        public ListView MyPlaces { get; private set; }

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.HomeScreen);

            //SearchButton = FindViewById<ImageButton>(Resource.Id.btnSearch);
            //SearchText = FindViewById<EditText>(Resource.Id.editPlace);
            
            MyPlaces = FindViewById<ListView>(Resource.Id.myPlaces);

            ISharedPreferences sp = GetSharedPreferences("MyPlacesRepo", FileCreationMode.Append);
            //_sp.GetString("")

            var placeRepo = new SharedPreferencesRepository(sp);
            ViewModel = new HomeScreenViewModel(placeRepo);
            
            Locator.CurrentMutable.RegisterConstant(new BoolToVisibilityConverter(), typeof(IBindingTypeConverter));

            

            //Observable
            //        .FromEventPattern<EventHandler<AdapterView.ItemSelectedEventArgs>, AdapterView.ItemSelectedEventArgs>(h => Places.ItemSelected += h,
            //                                                                                                              h => Places.ItemSelected -= h)
            //        .Where(x=> ViewModel.Places.Count > 0)
            //        .Select(x=> x.EventArgs)
            //        .Subscribe(async e =>
            //        {
            //            if (ViewModel.Places.Count > e.Position)
            //            {
            //                await ViewModel.Places[e.Position].Add.ExecuteAsync(null);
            //            }
            //        });

            

            

            var myPlacesAdapter = new ReactiveListAdapter<MyPlaceViewModel>(
                ViewModel.MyPlaces,
                (vm, parent) => new MyPlaceView(vm, this, parent));
            MyPlaces.Adapter = myPlacesAdapter;

            MyPlaces.ItemSelected += (s, e) =>
            {
                var intent = new Intent(this, typeof(WeeklyForecastActivity));
                intent.PutExtra("name", ViewModel.MyPlaces[e.Position].Name);
                intent.PutExtra("latitude", ViewModel.MyPlaces[e.Position].Latitude);
                intent.PutExtra("longitude", ViewModel.MyPlaces[e.Position].Longitude);
                StartActivity(intent);
            };

            //TODO: Convert to ReactiveUI bindings
            //button.Click += async (s, e) =>
            //{
            //    var places = await PlaceService.GetPlaceFromName(editPlace.Text);
            //    if (places.results.Any())
            //    {
            //        //editPlace.Text = places.results[0].formatted_address;
            //        //var lat = places.results[0].geometry.location.lat;
            //        //var lng = places.results[0].geometry.location.lng;

            //        //System.Diagnostics.Debug.WriteLine(string.Format("{0} , {1}", lat, lng));
            //        //var weather = await DarkSkyService.GetWeatherData(DarkSkyApiKey, lat, lng);
            //        //var curDateTime = new DateTime(1970, 1, 1).AddSeconds(weather.currently.time).AddHours(weather.offset);

            //        //txtDate.Text = curDateTime.ToShortDateString();
            //        //txtTime.Text = curDateTime.ToShortTimeString();
            //        //txtCurTemp.Text = weather.currently.temperature.ToString() + "°";
            //        //imgIcon.SetImageResource(Resource.Drawable.partlycloudyday);
            //        ////weather.currently.icon;
            //        ////line.Width = weather.daily.data[0].temperatureMax - weather.daily.data[0].temperatureMin;
            //        //txtMinTemp.Text = weather.daily.data[0].temperatureMin.ToString() + "°";
            //        //txtMaxTemp.Text = weather.daily.data[0].temperatureMax.ToString() + "°";
            //    }




            //};
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


