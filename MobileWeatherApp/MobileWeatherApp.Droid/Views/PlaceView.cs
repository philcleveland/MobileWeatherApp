using Android.Content;
using Android.Views;
using Android.Widget;
using ReactiveUI;

namespace MobileWeatherApp.Droid.Views
{
    public class PlaceView : ReactiveViewHost<PlaceViewModel>
    {
        public PlaceView(Context ctx, PlaceViewModel place, ViewGroup parent)
            : base(ctx, Resource.Layout.Place, parent)
        {
            ViewModel = place;
            this.OneWayBind(ViewModel, vm => vm.Name, v => v.Title.Text);
        }

        public TextView Title { get; private set; }
    }

    
}