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

namespace MobileWeatherApp.Droid.Activities
{
    [Activity(Label = "DailyForecastActivity")]
    public class DailyForecastActivity : Activity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your application here
            var txtPlace = FindViewById<TextView>(Resource.Id.txtPlace);
            var txtCurTemp = FindViewById<TextView>(Resource.Id.txtCurrentTemp);
            var txtDate = FindViewById<TextView>(Resource.Id.txtDate);
            var txtTime = FindViewById<TextView>(Resource.Id.txtTime);
            var line = FindViewById<View>(Resource.Id.lineTempDiff);
            var txtMinTemp = FindViewById<TextView>(Resource.Id.txtMinTemp);
            var txtMaxTemp = FindViewById<TextView>(Resource.Id.txtMaxTemp);
            var txtAlerts = FindViewById<TextView>(Resource.Id.txtAlerts);
            var imgIcon = FindViewById<ImageView>(Resource.Id.imgIcon);
        }
    }
}