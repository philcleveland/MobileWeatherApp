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
    public class MyPlaceView : ReactiveViewHost<MyPlaceViewModel>
    {

        public MyPlaceView(MyPlaceViewModel viewModel, Context ctx, ViewGroup parent)
            : base(ctx, Resource.Layout.Place, parent)
        {
            ViewModel = viewModel;

            this.OneWayBind(ViewModel, vm => vm.Name, v => v.Title.Text);
            this.BindCommand(ViewModel, vm => vm.Remove, v => v.RemoveBtn);
            
        }

        public TextView Title { get; private set; }
        public Button RemoveBtn { get; private set; }
    }
}