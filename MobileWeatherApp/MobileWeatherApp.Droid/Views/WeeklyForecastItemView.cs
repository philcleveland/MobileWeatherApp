using Android.Content;
using Android.Views;
using Android.Widget;
using ReactiveUI;
using Splat;
using System.Reactive.Linq;
using System;

namespace MobileWeatherApp.Droid.Views
{
    public class WeeklyForecastItemView : ReactiveViewHost<WeeklyForecastItemViewModel>
    {
        public WeeklyForecastItemView(Context ctx, WeeklyForecastItemViewModel itemVM, ViewGroup parent)
            : base(ctx, Resource.Layout.WeeklyForecastItem, parent)
        {
            ViewModel = itemVM;

            this.WhenAnyValue(x => x.ViewModel.Icon)
                .Where(img => img != null)
                .Select(img => img.ToNative())
                .Subscribe(img => IconImage.SetImageDrawable(img));

            this.OneWayBind(ViewModel, vm => vm.Day, v => v.TextDay.Text);
            this.OneWayBind(ViewModel, vm => vm.TemperatureMin, v => v.TextLowTemp.Text);
            this.OneWayBind(ViewModel, vm => vm.TemperatureMax, v => v.TextHighTemp.Text);
            this.OneWayBind(ViewModel, vm => vm.Summary, v => v.TextSummary.Text);
        }
        public ImageView IconImage { get; set; }
        public TextView TextDay { get; private set; }
        public TextView TextLowTemp { get; private set; }
        public TextView TextHighTemp { get; private set; }
        public TextView TextSummary { get; private set; }
    }
}