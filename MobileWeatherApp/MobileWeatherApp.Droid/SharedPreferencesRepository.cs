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
using Akavache;
using System.Reactive.Linq;
using Newtonsoft.Json;

namespace MobileWeatherApp.Droid
{
    public class SharedPreferencesRepository : IPlacesRepository
    {
        readonly ISharedPreferences _sp;
        public SharedPreferencesRepository(ISharedPreferences sp)
        {
            _sp = sp;
            
        }
        public void AddPlace(MyPlaceViewModel place)
        {
            using (var editor = _sp.Edit())
            {
                editor.PutString(place.Name, JsonConvert.SerializeObject(place));
            }
        }

        public IEnumerable<MyPlaceViewModel> GetAllPlaces()
        {
            List<MyPlaceViewModel> places = new List<MyPlaceViewModel>();
            foreach (var p in _sp.All)
            {
                places.Add(JsonConvert.DeserializeObject<MyPlaceViewModel>(p.Value.ToString()));
            }
            return places;
        }

        public void RemovePlace(string placeName)
        {
            using (var editor = _sp.Edit())
            {
                editor.Remove(placeName);
            }
        }
    }

    //public class AkavachePlacesRepository : IPlacesRepository
    //{
        
    //    public AkavachePlacesRepository()
    //    {
    //        BlobCache.ApplicationName = "MobileWeatherApp";
    //    }
    //    public void AddPlace(MyPlaceViewModel place)
    //    {
    //        BlobCache.UserAccount.InsertObject(place.Name, place);
    //    }

    //    public void RemovePlace(string placeName)
    //    {
    //        BlobCache.UserAccount.Invalidate(placeName);
    //    }

    //    public IObservable<IEnumerable<MyPlaceViewModel>> GetAllPlaces()
    //    {
    //        //return new List<MyPlaceViewModel>();
            
    //        return BlobCache.UserAccount.GetAllObjects<MyPlaceViewModel>();
    //    }
    //}
}