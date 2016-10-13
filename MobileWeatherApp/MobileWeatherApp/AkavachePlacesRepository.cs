using Akavache;
using System;
using System.Collections.Generic;

namespace MobileWeatherApp
{
    public class AkavachePlacesRepository : IPlacesRepository
    {
        public AkavachePlacesRepository()
        {
            BlobCache.ApplicationName = "MobileWeatherApp";
        }
        public void AddPlace(MyPlaceViewModel place)
        {
            BlobCache.UserAccount.InsertObject(place.Name, place);
        }

        public void RemovePlace(string placeName)
        {
            BlobCache.UserAccount.Invalidate(placeName);
        }

        public IObservable<IEnumerable<MyPlaceViewModel>> GetAllPlaces()
        {
            return BlobCache.UserAccount.GetAllObjects<MyPlaceViewModel>();
        }
    }
}
