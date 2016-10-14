using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MobileWeatherApp
{
    public class InMemPlacesRepository : IPlacesRepository
    {
        Dictionary<string, MyPlaceViewModel> _db = new Dictionary<string, MyPlaceViewModel>();

        public void AddPlace(MyPlaceViewModel place)
        {
            if(!_db.ContainsKey(place.Name))
            {
                _db.Add(place.Name, place);
            }
            
        }

        public IEnumerable<MyPlaceViewModel> GetAllPlaces()
        {
            return _db.Values.ToList();
        }

        public void RemovePlace(string placeName)
        {
            if (_db.ContainsKey(placeName))
            {
                _db.Remove(placeName);
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
    //        return BlobCache.UserAccount.GetAllObjects<MyPlaceViewModel>();
    //    }
    //}
}
