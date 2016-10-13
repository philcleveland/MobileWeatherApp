using System;
using System.Linq;
using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using Android.Locations;
using ReactiveUI;

namespace MobileWeatherApp.Droid
{
    [Activity(Label = "MobileWeatherApp.Droid", MainLauncher = true, Icon = "@drawable/icon")]
    public class HomeScreenActivity : ReactiveActivity<HomeScreenViewModel>
    {
        const string DarkSkyApiKey = "DARK_SKY_API_KEY_HERE";
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.Main);

            // Get our button from the layout resource,
            // and attach an event to it
            var button = FindViewById<Button>(Resource.Id.myButton);
            var txtPlace = FindViewById<TextView>(Resource.Id.txtPlace);
            var txtCurTemp = FindViewById<TextView>(Resource.Id.txtCurrentTemp);
            var txtDate = FindViewById<TextView>(Resource.Id.txtDate);
            var txtTime = FindViewById<TextView>(Resource.Id.txtTime);
            var line = FindViewById<View>(Resource.Id.lineTempDiff);
            var txtMinTemp = FindViewById<TextView>(Resource.Id.txtMinTemp);
            var txtMaxTemp = FindViewById<TextView>(Resource.Id.txtMaxTemp);
            var txtAlerts = FindViewById<TextView>(Resource.Id.txtAlerts);
            var imgIcon = FindViewById<ImageView>(Resource.Id.imgIcon);

            //TODO: Convert to ReactiveUI bindings
            button.Click += async (s, e) =>
            {
                var places = await PlaceService.GetPlaceFromName("Reno, NV");
                if (places.results.Any())
                {
                    txtPlace.Text = places.results[0].formatted_address;
                    var lat = places.results[0].geometry.location.lat;
                    var lng = places.results[0].geometry.location.lng;

                    System.Diagnostics.Debug.WriteLine(string.Format("{0} , {1}", lat, lng));
                    var weather = await DarkSkyService.GetWeatherData(DarkSkyApiKey, lat, lng);
                    var curDateTime = new DateTime(1970, 1, 1).AddSeconds(weather.currently.time).AddHours(weather.offset);

                    txtDate.Text = curDateTime.ToShortDateString();
                    txtTime.Text = curDateTime.ToShortTimeString();
                    txtCurTemp.Text = weather.currently.temperature.ToString() + "°";
                    imgIcon.SetImageResource(Resource.Drawable.partlycloudyday);
                    //weather.currently.icon;
                    //line.Width = weather.daily.data[0].temperatureMax - weather.daily.data[0].temperatureMin;
                    txtMinTemp.Text = weather.daily.data[0].temperatureMin.ToString() + "°";
                    txtMaxTemp.Text = weather.daily.data[0].temperatureMax.ToString() + "°";
                }
                else
                {
                    txtPlace.Text = "No Places Found!";
                }



            };
        }
    }
}


