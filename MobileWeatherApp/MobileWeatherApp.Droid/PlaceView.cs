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
    public class PlaceView : ReactiveViewHost<PlaceViewModel>
    {
        
        public PlaceView(PlaceViewModel viewModel, Context ctx, ViewGroup parent)
            :base(ctx, Resource.Layout.Place, parent)
        {
            ViewModel = viewModel;

            this.OneWayBind(ViewModel, vm => vm.Name, v => v.Title.Text);
        }

        public TextView Title { get; private set; }
        public Button AddPlaceBtn { get; private set; }
    }
}