using Android.Views;
using ReactiveUI;
using System;

namespace MobileWeatherApp.Droid
{
    public class BoolToVisibilityConverter : IBindingTypeConverter
    {
        public int GetAffinityForObjects(Type fromType, Type toType)
        {
            if (fromType == typeof(bool) && toType == typeof(ViewStates))
                return 10;
            else if (fromType == typeof(ViewStates) && toType == typeof(bool))
                return 10;
            return 0;
        }

        public bool TryConvert(object from, Type toType, object conversionHint, out object result)
        {
            if (toType == typeof(bool))
            {
                result = (ViewStates)from == ViewStates.Visible ? true : false;
                return true;
            }
            else if (toType == typeof(ViewStates))
            {
                result = (bool)from ? ViewStates.Visible : ViewStates.Invisible;
                return true;
            }

            result = null;
            return false;
        }
    }
}