using Akavache;
using Android.App;
using Android.OS;
using Android.Widget;
using MobileWeatherApp.Services;
using ReactiveUI;
using Splat;
using System;
using System.Linq;
using System.Reactive.Disposables;
using System.Reactive.Linq;

namespace MobileWeatherApp.Droid.Activities
{
    [Activity(Label = "DailyForecastActivity")]
    public class DailyForecastActivity : ReactiveActivity<DailyForecastViewModel>
    {
        
        //public TextView TextDate { get; private set; }
        //public TextView TextTime { get; private set; }
        public TextView TextLowTemp { get; private set; }
        public TextView TextCurrentTemp { get; private set; }
        public TextView TextHighTemp { get; private set; }
        public TextView TextAlerts { get; private set; }
        public ImageView ImageIcon { get; private set; }

        public DailyForecastActivity()
        {
            this.WhenActivated(() =>
            {
                var disposable = new CompositeDisposable();

                //this.OneWayBind(ViewModel, vm => vm.Date, v => v.TextDate.Text);
                //this.OneWayBind(ViewModel, vm => vm.Time, v => v.TextTime.Text);
                this.OneWayBind(ViewModel, vm => vm.LowTemp, v => v.TextLowTemp.Text);
                this.OneWayBind(ViewModel, vm => vm.CurrentTemperature, v => v.TextCurrentTemp.Text);
                this.OneWayBind(ViewModel, vm => vm.HighTemp, v => v.TextHighTemp.Text);
                this.OneWayBind(ViewModel, vm => vm.Alerts, v => v.TextAlerts.Text);

                this.WhenAnyValue(x => x.ViewModel.Icon)
                    .Where(img => img != null)
                    .Select(img => img.ToNative())
                    .Subscribe(img => ImageIcon.SetImageDrawable(img));

                return disposable;
            });
        }
        protected override async void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.DailyForecast);
            this.WireUpControls();

            var lat = Intent.GetDoubleExtra("lat", 0.0);
            var lng = Intent.GetDoubleExtra("lng", 0.0);
            var darkSkyKey = await BlobCache.UserAccount.GetObject<string>("darksky");
            ViewModel = new DailyForecastViewModel(lat, lng, new DarkSkyService(darkSkyKey));


            
        }
    }
}