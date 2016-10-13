using System;
using System.Collections.Generic;

namespace MobileWeatherApp
{
    public interface IPlacesRepository
    {
        void AddPlace(MyPlaceViewModel place);
        IObservable<IEnumerable<MyPlaceViewModel>> GetAllPlaces();
        void RemovePlace(string placeName);
    }
}